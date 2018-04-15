using StudentData;
using StudentData.Custom;
using System.Collections.Generic;
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
            return View(ListOfPersons());
        }
        [HttpPost]
        public ActionResult Save(PersonModel model)
        {
            var PersonCaptured = new Person() { Name = model.Name, Surname = model.Surname };
            unitOfWork.PersonRepository.Add(PersonCaptured);
            unitOfWork.Save();

            return View("Index", ListOfPersons());
        }
        public ActionResult Delete(int PersonNo)
        {
            Person PersonToDelete = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == PersonNo);

            unitOfWork.PersonRepository.Remove(PersonToDelete);
            unitOfWork.Save();
            return View("Index", ListOfPersons());
        }
        private IEnumerable<Person> ListOfPersons()
        {
            // Get everyone ....used not 456
            return unitOfWork.PersonRepository.Find(x => x.Name != "456");
        }

        public ActionResult Update(PersonModel person)
        {
            Person PersonToUpdate = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == person.PersonNo);
            PersonToUpdate.Name = person.Name + " get from form";
            PersonToUpdate.Surname = person.Name + " get from form";
            unitOfWork.Save();
            return View("Index", ListOfPersons());
        }
    }
}