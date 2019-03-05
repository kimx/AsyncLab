using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncLab.WebApp.Controllers
{
    public class HomeController : Controller
    {
        static int _DelayAsyncCount = 0;
        static int _DelayCount = 0;
        public ActionResult Index()
        {
            ViewBag.Message = $"_DelayAsyncCount : {_DelayAsyncCount}<br>_DelayCount : {_DelayCount}";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            _DelayAsyncCount = 0;
            _DelayCount = 0;
            return View();
        }

        int _count = 100;
        public async Task<ActionResult> DelayAsync()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
         //   await Task.Delay(_delay);
            using (var db = new Models.UnderBingoEntities())
            {
                var list = await db.BINSIGNDs.Take(_count).ToListAsync();
                stopwatch.Stop();
                var v = stopwatch.ElapsedMilliseconds;
                _DelayAsyncCount += 1;
                return Content("DelayAsync:" + v);
            }
            //var v = await GetRemoteData();
            //return Content("DelayAsync:" + v);
        }

        int _delay = 2000;
        public ActionResult Delay()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            using (var db = new Models.UnderBingoEntities())
            {
           //     System.Threading.Thread.Sleep(_delay);
                var list =  db.BINSIGNDs.Take(_count).ToList();
                stopwatch.Stop();
                var v = stopwatch.ElapsedMilliseconds;
                _DelayCount += 1;
                return Content("Delay:" + v);
            }

            //System.Threading.Thread.Sleep(_delay);
            //var v = 1;
            // return Content("Delay:" + v);
        }

        async Task<int> GetRemoteData()
        {
            int res = 0;
            await Task.Delay(_delay);
            res = 32767;

            return res;
        }
    }
}