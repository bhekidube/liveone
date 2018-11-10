using StudentData;
using StudentData.Custom;
using System;
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


        //public ActionResult CreateGroup(GroupEdit model)
        //{
        //    var grp = new Group()
        //    {
        //        GroupTypeNo = model.GroupType,
        //        GroupDesc = model.GroupDesc,
        //        StartDate = model.StartDate,
        //        EndDate = model.EndDate,
        //        SystemDate = DateTime.Now,
        //        GroupPersonNo = model.GroupPersonNo
        //    };
        //    unitOfWork.GroupRepository.Add(grp);
        //    unitOfWork.Save();
        //    ViewBag.Groups = ListOfGroups();
        //    return View("CreateGroup");
        //}

        public ActionResult AddGroupMember(PersonModelEdit person)
        {
            var grpMember = new GroupMember()
            {
                GroupMemberPersonNo = person.PersonNo,
                GroupNo = person.Group,
                SystemDate = DateTime.Now
            };
            unitOfWork.GroupMemberRepository.Add(grpMember);
            unitOfWork.Save();
            return View("");
        }

        public ActionResult ViewCreateGroup(GroupEdit model)
        {
            ViewBag.Groups = ListOfGroups();
            return View("CreateGroup", ListOfGroups());
        }
        public ActionResult Delete(int PersonNo)
        {
            Person PersonToDelete = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == PersonNo);

            unitOfWork.PersonRepository.Remove(PersonToDelete);
            unitOfWork.Save();
            return View("Index", ListOfPersons());
        }
        public ActionResult DeleteGroup(int GroupNo)
        {
            Group GroupToDelete = unitOfWork.GroupRepository.FindBy(x => x.GroupNo == GroupNo);

            unitOfWork.GroupRepository.Remove(GroupToDelete);
            unitOfWork.Save();
            return View("CreateGroup", ViewBag.Groups = ListOfGroups());
        }
        private IEnumerable<PersonModelEdit> ListOfPersons()
        {
            List<PersonModelEdit> list = new List<PersonModelEdit>();
            foreach (var person in unitOfWork.PersonRepository.Find(x => x.Name != "456"))
            { list.Add(new PersonModelEdit() { PersonNo = person.PersonNo, Name = person.Name, Surname = person.Surname }); }
            IEnumerable<PersonModelEdit> en = list;
            return en;
        }

        private IEnumerable<GroupEdit> ListOfGroups()
        {
            List<GroupEdit> list = new List<GroupEdit>();
            foreach (var group in unitOfWork.GroupRepository.Find(x => x.GroupDesc != "456"))
            {
                list.Add(new GroupEdit()
                {
                    GroupNo = group.GroupNo,
                    GroupType = group.GroupTypeNo,
                    GroupPersonNo = group.GroupPersonNo,
                    GroupDesc = group.GroupDesc,
                    StartDate = group.StartDate,
                    EndDate = group.EndDate,
                });
            }
            IEnumerable<GroupEdit> en = list;
            return en;
        }

        private IEnumerable<WorkEdit> ListOfWork()
        {
            List<WorkEdit> list = new List<WorkEdit>();
            foreach (var work in unitOfWork.WorkRepository.Find(x => x.WorkDesc != "456"))
            {
                list.Add(new WorkEdit()
                {
                    GroupNo = work.GroupNo,
                    WorkDesc = work.WorkDesc,
                    WorkTypeNo = work.WorkTypeNo,
                    PersonNo = work.PersonNo,
                    StartDate = work.StartDate,
                    EndDate = work.EndDate
                });
            }
            IEnumerable<WorkEdit> en = list;
            return en;
        }

        private IEnumerable<WorkEdit> ListOfWorkByGroup(int GroupNo)
        {
            List<WorkEdit> list = new List<WorkEdit>();
            foreach (var work in unitOfWork.WorkRepository.Find(x => x.GroupNo == GroupNo))
            {
                list.Add(new WorkEdit()
                {
                    WorkNo = work.WorkNo,
                    GroupNo = work.GroupNo,
                    WorkDesc = work.WorkDesc,
                    WorkTypeNo = work.WorkTypeNo,
                    PersonNo = work.PersonNo,
                    StartDate = work.StartDate,
                    EndDate = work.EndDate
                });
            }
            IEnumerable<WorkEdit> en = list;
            return en;
        }

        public ActionResult ViewUpdate(PersonModelEdit person)
        {
            if (person != null)
            {
                Person PersonToUpdate = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == person.PersonNo);
                IEnumerable<StudentData.IDType> IDtypes = unitOfWork.IdTypeRepository.Find(x => x.IDTypeDesc != "456");
                IEnumerable<StudentData.PersonType> PersonTypes = unitOfWork.PersonTypeRepository.Find(x => x.PersonTypeDesc != "456");
                IEnumerable<StudentData.GroupType> GroupTypes = unitOfWork.GroupTypeRepository.Find(x => x.GroupTypeDesc != "456");
                IEnumerable<StudentData.Group> Group = unitOfWork.GroupRepository.Find(x => x.GroupDesc != "456");

                List<Models.PersonType> ps = new List<Models.PersonType>();
                foreach (var PersonType in PersonTypes)
                {
                    ps.Add(new Models.PersonType() { PersonTypeNo = PersonType.PersonTypeNo, PersonTypeDesc = PersonType.PersonTypeDesc });
                }

                List<Models.IDType> idtps = new List<Models.IDType>();
                foreach (var idtype in IDtypes)
                {
                    idtps.Add(new Models.IDType() { IDTypeNo = idtype.IDTypeNo, IDTypeDesc = idtype.IDTypeDesc });
                }

                List<Models.GroupType> groupTypes = new List<Models.GroupType>();
                foreach (var gtype in GroupTypes)
                {
                    groupTypes.Add(new Models.GroupType() { GroupTypeNo = gtype.GroupTypeNo, GroupTypeDesc = gtype.GroupTypeDesc });
                }

                List<GroupEdit> groups = new List<GroupEdit>();
                foreach (var g in Group)
                {
                    groups.Add(new GroupEdit() { GroupNo = g.GroupNo, GroupDesc = g.GroupDesc });
                }

                PersonModelEdit personmodel = new PersonModelEdit()
                {
                    PersonNo = PersonToUpdate.PersonNo,
                    Name = PersonToUpdate.Name,
                    Surname = PersonToUpdate.Surname,
                    Gender = PersonToUpdate.GenderNo,
                    ID = PersonToUpdate.IdNo,
                    IDType = PersonToUpdate.IdTypeNo,
                    PersonType = PersonToUpdate.PersonTypeNo,
                    DOB = PersonToUpdate.DOB,
                    PersonTypes = ps,
                    IDTypes = idtps,
                    GroupTypes = groupTypes,
                    Groups = groups
                };
                return View("ViewUpdate", personmodel);
            }
            PersonModelEdit Dummypersonmodel = new PersonModelEdit() { PersonNo = 0, Name = "", Surname = "" };
            return View("ViewUpdate", Dummypersonmodel);
        }

        public ActionResult ViewEditGroup(int id, GroupEdit group)
        {
            if (group != null)
            {
                Group GroupToUpdate = unitOfWork.GroupRepository.FindBy(x => x.GroupNo == id);
                IEnumerable<StudentData.GroupType> GroupTypes = unitOfWork.GroupTypeRepository.Find(x => x.GroupTypeDesc != "456");

                List<Models.GroupType> groupTypes = new List<Models.GroupType>();
                foreach (var gtype in GroupTypes)
                {
                    groupTypes.Add(new Models.GroupType() { GroupTypeNo = gtype.GroupTypeNo, GroupTypeDesc = gtype.GroupTypeDesc });
                }

                GroupEdit groupmodel = new GroupEdit()
                {
                    GroupNo = GroupToUpdate.GroupNo,
                    GroupDesc = GroupToUpdate.GroupDesc,
                    GroupPersonNo = GroupToUpdate.GroupPersonNo,
                    GroupType = GroupToUpdate.GroupTypeNo,
                    GroupTypes = groupTypes,
                    StartDate = GroupToUpdate.StartDate,
                    EndDate = GroupToUpdate.EndDate,
                    SystemDate = (DateTime)GroupToUpdate.SystemDate
                };
                ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(GroupToUpdate.GroupNo);
                return View("ViewEditGroup", groupmodel);
            }
            GroupEdit Dummygroupmodel = new GroupEdit() { GroupNo = 0, GroupDesc = "", GroupPersonNo = 0 };
            ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(Dummygroupmodel.GroupNo);
            return View("ViewEditGroup", Dummygroupmodel);
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

            // if Add to group do the below:
            if (person.AddToGroup == true)
                AddGroupMember(person);

            return View("Index", ListOfPersons());
        }

        [HttpPost]
        public ActionResult Save(PersonModelEdit model)
        {
            var PersonCaptured = new Person()
            {
                Name = model.Name,
                Surname = model.Surname,
                GenderNo = model.Gender,
                DOB = model.DOB,
                IdNo = model.ID,
                IdTypeNo = model.IDType,
                PersonTypeNo = model.PersonType
            };
            unitOfWork.PersonRepository.Add(PersonCaptured);
            unitOfWork.Save();

            return View("Index", ListOfPersons());
        }
        [HttpPost]
        public ActionResult AddWork(int GroupNo, int PersonNo,string WorkName, int WorkTypeNo,DateTime StartDate,DateTime EndDate, GroupEdit group)
        {
            var EntityFmWork = new Work()
            {
                PersonNo = PersonNo,
                GroupNo = GroupNo,
                WorkTypeNo = WorkTypeNo,
                WorkDesc = WorkName,
                StartDate = StartDate,
                EndDate = EndDate,
                SystemDate = DateTime.Now
            };
            unitOfWork.WorkRepository.Add(EntityFmWork);
            unitOfWork.Save();
            ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(GroupNo);
            //GroupEdit Dummygroupmodel = new GroupEdit() { GroupNo = 0, GroupDesc = "", GroupPersonNo = 0 };
            return View("ViewEditGroup", group);

            //return View("AddWork");
        }

        public ActionResult CreateGroup(GroupEdit model)
        {
            var grp = new Group()
            {
                GroupTypeNo = model.GroupType,
                GroupDesc = model.GroupDesc,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                SystemDate = DateTime.Now,
                GroupPersonNo = model.GroupPersonNo
            };
            unitOfWork.GroupRepository.Add(grp);
            unitOfWork.Save();
            ViewBag.Groups = ListOfGroups();
            return View("CreateGroup");
        }
        public ActionResult ViewCreateWork(int id)
        {
            ViewBag.ListOfWork = ListOfWork();
            ViewBag.GroupNo = id;
            ViewBag.PersonNo = 707; /** Get from login **/
            return View(ListOfWork());
        }

        public ActionResult ViewEditWork(int id,WorkEdit work)
        {
            ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(work.GroupNo);
            ViewBag.GroupNo = work.GroupNo;
            ViewBag.PersonNo = 707; /** Get from login **/



            Work WorkToUpdate = unitOfWork.WorkRepository.FindBy(x => x.WorkNo == id);
            IEnumerable<WorkType> WorkTypes = unitOfWork.WorkTypeRepository.Find(x => x.WorkTypeDesc != "456");

            List<WorkTypeEdit> workTypes = new List<WorkTypeEdit>();
            foreach (var wtype in WorkTypes)
            {
                workTypes.Add(new WorkTypeEdit() { WorkTypeNo = wtype.WorkTypeNo, WorkTypeDesc = wtype.WorkTypeDesc });
            }

            WorkEdit workmodel = new WorkEdit()
            {
                WorkNo = WorkToUpdate.GroupNo,
                WorkDesc = WorkToUpdate.WorkDesc,
                WorkTypeNo = WorkToUpdate.WorkTypeNo,
                StartDate = WorkToUpdate.StartDate,
                EndDate = WorkToUpdate.EndDate,
                SystemDate = WorkToUpdate.SystemDate,
                WorkTypes = workTypes
            };


            return View(workmodel);
        }
    }
}