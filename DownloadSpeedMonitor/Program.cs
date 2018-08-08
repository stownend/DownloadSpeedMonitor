using System;
using System.Diagnostics;
using System.Threading;

namespace DownloadSpeedMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Tested at,Mbps");


            for (int i = 0; i < 50000; i++)
            {
                var watch = new Stopwatch();
                long bitsOfData = 0;

                byte[] data;
                using (var client = new System.Net.WebClient())
                {
                    watch.Start();
                    try
                    {
                        data = client.DownloadData("http://dl.google.com/googletalk/googletalk-setup.exe?t=" + DateTime.Now.Ticks);
                        bitsOfData = data.LongLength;
                    }
                    catch (Exception)
                    {
                        bitsOfData = 0;
                        // Continue
                    }
                    watch.Stop();
                }

                // Convert bytes to bits then to Mb
                var speed = bitsOfData > 0 ? (bitsOfData * 8 / 1024 / 1024 / watch.Elapsed.TotalSeconds) : 0; // instead of [Seconds] property

                //Console.WriteLine("Download duration: {0}", watch.Elapsed);
                //Console.WriteLine("File size: {0}", data.Length.ToString("N"));
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},{speed.ToString("N2")}");

                Thread.Sleep(30000);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
