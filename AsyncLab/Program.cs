using System;
using System.Collections.Generic;
using System.Linq;
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
        static void Main(string[] args)
        {
            Console.WriteLine("Main-Thread start");
            Test1();
            Console.ReadLine();
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
