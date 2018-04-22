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
        public ActionResult Save(PersonModelEdit model)
        {
            var PersonCaptured = new Person() { Name = model.Name, Surname = model.Surname, GenderNo = model.Gender,DOB = model.DOB,
                IdNo = model.ID, IdTypeNo = model.IDType, PersonTypeNo = model.PersonType };
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
        private IEnumerable<PersonModelEdit> ListOfPersons()
        {
            List<PersonModelEdit> list = new List<PersonModelEdit>();
            foreach (var person in unitOfWork.PersonRepository.Find(x => x.Name != "456"))
            { list.Add(new PersonModelEdit() { PersonNo = person.PersonNo, Name = person.Name, Surname = person.Surname }); }
            IEnumerable<PersonModelEdit> en = list;
            return en;
        }

        public ActionResult ViewUpdate(PersonModelEdit person)
        {
            if (person != null)
            {
                Person PersonToUpdate = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == person.PersonNo);
                IEnumerable<StudentData.IDType> IDtypes = unitOfWork.IdTypeRepository.Find(x => x.IDTypeDesc != "456");
                IEnumerable<StudentData.PersonType> PersonTypes = unitOfWork.PersonTypeRepository.Find(x => x.PersonTypeDesc != "456");

                List<Models.PersonType> ps = new List<Models.PersonType>();
                foreach (var PersonType in PersonTypes)
                {
                    ps.Add(new Models.PersonType() { PersonTypeNo = PersonType.PersonTypeNo,PersonTypeDesc = PersonType.PersonTypeDesc } );
                }

                List<Models.IDType> idtps = new List<Models.IDType>();
                foreach (var idtype in IDtypes)
                {
                    idtps.Add(new Models.IDType() { IDTypeNo = idtype.IDTypeNo, IDTypeDesc = idtype.IDTypeDesc });
                }

                PersonModelEdit personmodel = new PersonModelEdit()
                { PersonNo = PersonToUpdate.PersonNo,
                    Name = PersonToUpdate.Name,
                    Surname = PersonToUpdate.Surname,
                    Gender = PersonToUpdate.GenderNo,
                    ID = PersonToUpdate.IdNo,
                    IDType = PersonToUpdate.IdTypeNo,
                    PersonType = PersonToUpdate.PersonTypeNo,
                    DOB = PersonToUpdate.DOB,
                    PersonTypes = ps,
                    IDTypes = idtps
                };
                return View("ViewUpdate", personmodel);
            }
            PersonModelEdit Dummypersonmodel = new PersonModelEdit() { PersonNo = 0, Name = "", Surname = "" };
            return View("ViewUpdate", Dummypersonmodel);
        }

        public ActionResult Update(PersonModelEdit person)
        {
            Person PersonToUpdate = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == person.PersonNo);
            PersonToUpdate.Name = person.Name;
            PersonToUpdate.Surname = person.Surname;
            PersonToUpdate.PersonTypeNo = person.PersonType;
            PersonToUpdate.IdTypeNo = person.IDType;
            PersonToUpdate.IdNo = person.ID;
            PersonToUpdate.GenderNo = person.Gender;
            PersonToUpdate.DOB = person.DOB;
            unitOfWork.Save();

            return View("Index", ListOfPersons());
        }
    }
}