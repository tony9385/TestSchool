using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using testschool.DB;
using testschool.DLL;
using testschool.Models;

namespace testschool.Controllers
{
    public class HomeController : Controller
    {
        private AppSettings AppSettings { get; set; }
        private ConnectionStrings ConnectionStrings { get; set; }
        public HomeController(IOptions<AppSettings> settings,IOptions<ConnectionStrings> connSettings)
        {
            AppSettings = settings.Value;
            ConnectionStrings = connSettings.Value;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Hello Docker azure second";
            Login login = new Login();
            string ss = AppSettings.AzureStorageAccountContainer;
           // login.TestOpen();
            //using (var db = new AppDb("server=localhost;port=3306;database=school;uid=root;password=123456"))
            //{
            //    // await db.Connection.OpenAsync();
            //    try
            //    {
            //        db.Connection.Open();
            //        // task.Wait();
            //        var query = new BlogPostQuery(db);
            //        //var result = await query.LatestPostsAsync();
            //        Task<List<SchoolInfo>> result = query.LatestPostsAsync();
            //        result.Wait();
            //    }
            //    catch(Exception e)
            //    {

            //    }
            //    finally
            //    {
            //        //if(db.Connection.State!=C)
            //    }
            //   // ViewData["SchoolName"]=result/
            //}

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
