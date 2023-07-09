using Basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index1()
        {
            string message = $"Hello World. {DateTime.Now.ToString()}";
            return View("Index1", message);
        }

        public ViewResult Index2()
        {
            var names = new String[]
            {
                "Ahmet",
                "Mehmet",
                "Can"
            };
            return View(names);
        }

        public IActionResult Index3()
        {
            var list= new List<Employee>
            {
                new Employee(){Id=1, FirstName="Nazli", LastName="Esmeray", Age=21},
                new Employee(){Id=2, FirstName="Nur", LastName="Yakacik", Age=35},
                new Employee(){Id=3, FirstName="Sanem", LastName="Yilmaz", Age=41},

            };

            return View(list);
        }
    }
}