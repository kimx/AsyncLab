using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLab
{
    /// <summary>
    /// v3
    ///    Stopwatch sw = new Stopwatch();
    ///        sw.Start();
    ///        using (var db = new CorexERP_DEVEntities())
    ///        {
    ///            var systems = await db.SYSPACK.ToListAsync();
    ///            var programs = await db.SYSPRG.ToListAsync();
    ///            var roles = await db.SYSROLE.ToListAsync();
    ///            var roleFuns = await db.SYSROLE_FUN.ToListAsync();
    ///            var dirs = await db.SYSDIR.ToListAsync();
    ///            var dirFuns = await db.SYSDIR_FUN.ToListAsync();
    ///        }
    /// 
    ///        sw.Stop();
    ///        Console.WriteLine($"Async:{sw.Elapsed}");
    /// </summary>
    class Program
    {
        static string _PathS = @"D:\GitHub\AsyncLab\AsyncLab\bin\Debug\s\";
        static string _PathF = @"D:\GitHub\AsyncLab\AsyncLab\bin\Debug\f\";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main-Thread start");
            if (args.Length == 0)
            {
                Directory.Delete(_PathF,true);
                Directory.CreateDirectory(_PathF);
                Directory.Delete(_PathS, true);
                Directory.CreateDirectory(_PathS);
                for (int i = 0; i < 10; i++)
                {
                    Process.Start(@"D:\GitHub\AsyncLab\AsyncLab\bin\Debug\test.bat");
                }
                Console.ReadLine();
                var sSum = Directory.GetFiles(_PathS).Sum(s => Convert.ToInt32(Path.GetExtension(s).Replace(".", "")));
                var fSum = Directory.GetFiles(_PathF).Sum(s => Convert.ToInt32(Path.GetExtension(s).Replace(".", "")));
                Console.WriteLine($"sSum:{sSum},fSum:{fSum}");
                return;
            }
            //await TestWeb(false);
            //await TestWeb(true);

            //asyncEla = 0;
            //syncEla = 0;
            //warm
            max = 2;
            await TestWeb(false);
            asyncEla = 0;
            syncEla = 0;
            Console.WriteLine();

            //begin
            max = 10000;
            bool isAsync = false;
            if (args.Length == 2)
            {
                isAsync = Convert.ToBoolean(args[0]);
                max = Convert.ToInt32(args[1]);
            }
            Console.WriteLine($"isAsync:{isAsync},max:{max}");
            await TestWeb(false);
            Console.WriteLine("asyncEla  :" + asyncEla);
            Console.WriteLine("syncEla  :" + syncEla);
            Console.WriteLine("s-count:" + s);
            Console.WriteLine("f-count:" + f);

            Console.WriteLine("Main-Thread end");
            File.Create(_PathS + $"{DateTime.Now.ToString("ddHHmmss")}.{s}");
            File.Create(_PathF + $"{DateTime.Now.ToString("ddHHmmss")}.{f}");
            //  Console.ReadLine();
        }

        static string url = "http://localhost:8073/Home/Delay";
        static int max = 10000;
        static int s = 0;
        static int f = 0;
        static int currentBeginCount = 0;
        static object _lockS = new object();
        static object _lockF = new object();
        static object _lockB = new object();
        static long asyncEla = 0;
        static long syncEla = 0;
        static Stopwatch stopwatch;
        static async Task TestWeb(bool isAsync)
        {
            if (isAsync)
                url = "http://localhost:8073/Home/DelayAsync";
            else
                url = "http://localhost:8073/Home/Delay";

            stopwatch = new System.Diagnostics.Stopwatch();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < max; i++)
            {
                object arg = i;
                Task task = new Task(new Action<object>((p) =>
                {
                    SendRequest(Convert.ToInt32(p));
                }), arg);

                tasks.Add(task);

            }

            stopwatch.Start();
            Parallel.ForEach(tasks, (t) =>
            {
                t.Start();
            });
            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            Console.WriteLine(url + ":" + stopwatch.ElapsedMilliseconds);
            if (isAsync)
                asyncEla += stopwatch.ElapsedMilliseconds;
            else
                syncEla += stopwatch.ElapsedMilliseconds;
        }

        static void SendRequest(int i)
        {
            using (WebClient web = new WebClient())
            {
                try
                {
                    string message = web.DownloadString(url);
                    lock (_lockS)
                    {
                        s++;
                    }

                }
                catch (Exception ex)
                {
                    lock (_lockF)
                    {
                        f++;
                    }
                    //  Console.WriteLine($"{i}-ex:" + ex.Message);

                }
            }
        }

        #region Test1
        async static void Test1()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var a = Task.Run(() => LongTask("1", 3000));
            var b = Task.Run(() => LongTask("2", 3000));
            Task.WaitAll(a, b);
            sw.Stop();
            Console.WriteLine($"Test1-End:{sw.ElapsedMilliseconds}");
        }

        static void LongTask(string name, int wait)
        {
            Thread.Sleep(wait);
            Console.WriteLine($"Finished Long Task : {name}");
        }
        #endregion
    }
}
