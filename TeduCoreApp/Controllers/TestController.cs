using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Services;

namespace TeduCoreApp.Controllers
{
    public class TestController : Controller
    {
        private IConfiguration _configuration;
        private IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public TestController(IConfiguration configuration, IEmailSender emailSender,
            UserManager<AppUser> userManager, AppDbContext context)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            //var teststring = @"\uploaded\images\20191026\bo-ngu-cho-be-cotton-100-kkct01.jpg";
            //ViewBag.Test = teststring.Replace('\\', '/');
            //string text = System.IO.File.ReadAllText(@".\wwwroot\files\NewFolder\WriteText.txt");
            //ViewBag.Test = text;

            //int counter = 0;
            //string line;

            //// Read the file and display it line by line.
            //System.IO.StreamReader file =
            //    new System.IO.StreamReader(@".\wwwroot\files\NewFolder\WriteText.txt");
            //while ((line = file.ReadLine()) != null)
            //{
            //    System.Console.WriteLine(line);
            //    counter++;
            //}

            //file.Close();
            //System.Console.WriteLine("There were {0} lines.", counter);
            //// Suspend the screen.
            //System.Console.ReadLine();

            //var random = new Random();
            /*ViewBag.Test =random.Next().ToString();*/
            string Key = "Chí Minh";
            var province = _context.Provinces.Where(x => x.Name.Contains(Key)).ToListAsync();
            if (province != null)
            {
                ViewBag.Test = "Có Key";
            }
            else
            {
                ViewBag.Test = "Không Có Key";
            }

            //ViewBag.Test = _context.Provinces.SingleOrDefault(x => x.Name == "Thành phố Hà Nội").Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> sendmail()
        {
            //List<string> images = new List<string>();
            //images.Add(@"\uploaded\images\20191026\bo-ngu-cho-be-cotton-100-kkct01.jpg");
            //images.Add(@"\uploaded\images\20191202\LT4704G17.jpg");
            //images.Add(@"\uploaded\images\20191026\bo-ngu-cho-be-cotton-100-kkct20.jpg");

            //await _emailSender.SendEmailBillMailAsync("nicolas.kenry@gmail.com", "Test Send Files", "Test Send Files", images);

            return View("Index");
        }

        [HttpPost]
        public IActionResult File()
        {
            //string[] lines = { "First line", "Second line", "Third line" };
            //string text = "A class is the most powerful data type in C#. Like a structure, " + "a class defines the data and behavior of the data type. ";
            //List<string> images = new List<string>();

            //images.Add(@"\uploaded\images\20191026\bo-ngu-cho-be-cotton-100-kkct01.jpg");
            //images.Add(@"\uploaded\images\20191202\LT4704G17.jpg");
            //images.Add(@"\uploaded\images\20191026\bo-ngu-cho-be-cotton-100-kkct20.jpg");

            //foreach (var item in images)
            //{
            //    System.IO.File.WriteAllText(@".\wwwroot\files\NewFolder\WriteText.txt", item.ToString());
            //}

            var listStreet = _context.Streets.ToList();
            List<string> List = new List<string>();
            foreach (var item in listStreet)
            {
                List.Add(item.Name.ToString());
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@".\wwwroot\files\NewFolder\WriteText.txt"))
            {
                var item = 0;
                foreach (var line in List)
                {
                    if (item == 10)
                    {
                        file.WriteLine(line + "---");
                        item = 0;
                    }
                    item++;
                    file.WriteLine(line);
                }
            }

            return View("Index");
        }
    }
}