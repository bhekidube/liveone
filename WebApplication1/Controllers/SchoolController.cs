using StudentData;
using StudentData.Custom;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SchoolController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: School
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(PersonModel model)
        {
            var person = new Person() { Name = model.Name, Surname = model.Surname };
            unitOfWork.PersonRepository.Add(person);
            unitOfWork.Save();

            return View("Index");
        }
    }
}