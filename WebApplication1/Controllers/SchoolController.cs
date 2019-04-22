using StudentData;
using StudentData.Custom;
using System;
using System.Collections.Generic;
using System.Linq;// Refactor to remove this...make generic
using System.Web.Mvc;// Refactor to remove this...make generic
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

        public void AddGroupMember(int PersonNo,int GroupNo)
        {
            var grpMember = new GroupPerson()
            {
                GroupMemberPersonNo = PersonNo,
                GroupNo = GroupNo,
                SystemDate = DateTime.Now
            };
            var groups = unitOfWork.GroupPersonRepository.Find(x => x.GroupMemberPersonNo == PersonNo).AsEnumerable();
            var group = groups.Where(x => x.GroupNo == GroupNo).FirstOrDefault();
            //var groupMember = unitOfWork.GroupPersonRepository.FindBy(x => x.GroupMemberPersonNo == PersonNo);
            if (group == null)
                unitOfWork.GroupPersonRepository.Add(grpMember);
            unitOfWork.Save();
        }

        public ActionResult DeleteGroupMember(int PersonNo,int GroupNo)
        {
            var groupMembers = unitOfWork.GroupPersonRepository.Find(x => x.GroupNo == GroupNo).AsEnumerable();
            var groupMemberNo = groupMembers.Where(x => x.GroupMemberPersonNo == PersonNo).FirstOrDefault().GroupMemberNo;
            GroupPerson GroupPersonToDelete = unitOfWork.GroupPersonRepository.FindBy(x => x.GroupMemberNo == groupMemberNo);

            unitOfWork.GroupPersonRepository.Remove(GroupPersonToDelete);
            unitOfWork.Save();
            return ViewEditGroup(GroupNo, new GroupEdit());
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

        public ActionResult DeleteGroupWork(int WorkNo,int GroupNo)
        {
            Work WorkToDelete = unitOfWork.WorkRepository.FindBy(x => x.WorkNo == WorkNo);

            unitOfWork.WorkRepository.Remove(WorkToDelete);
            unitOfWork.Save();
            Group group = unitOfWork.GroupRepository.FindBy(x => x.GroupNo == GroupNo);
            return ViewEditGroup(GroupNo, new GroupEdit());
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
        private IEnumerable<WorkPersonModelEdit> ListOfWorkMembers(int WorkNo)
        {
            var results = unitOfWork.WorkResultRepository.Find(x=>x.WorkNo == WorkNo);
            List<WorkPersonModelEdit> list = new List<WorkPersonModelEdit>();
            foreach (var r in results)
            {
                var person = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == r.PersonNo);


                list.Add(new WorkPersonModelEdit()
                {
                
                    Name = person.Name,
                    Surname = person.Surname,
                    PersonNo = person.PersonNo,
                    PersonMark = r.ResultNo
                });

            }
            IEnumerable<WorkPersonModelEdit> en = list;
            return en;
        }
        private IEnumerable<PersonModelEdit> ListOfMembersByGroup(int GroupNo)
        {
            List<PersonModelEdit> list = new List<PersonModelEdit>();
            foreach (var person in unitOfWork.GroupPersonRepository.Find(x => x.GroupNo == GroupNo))
            {

                var p = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == person.GroupMemberPersonNo);
                list.Add(new PersonModelEdit()
                {
                    Name = p.Name,
                    Surname = p.Surname,
                    PersonNo = p.PersonNo
                });
            }
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
                    SystemDate = (DateTime)GroupToUpdate.SystemDate,
                    Persons = ListOfPersons().Select(x=> new SelectListItem { Value = x.PersonNo.ToString(),Text = x.Name}),
                    SelectedPersonNo = group.SelectedPersonNo
                };
                if (groupmodel.SelectedPersonNo != 0)
                    AddGroupMember(groupmodel.SelectedPersonNo, groupmodel.GroupNo);
                ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(GroupToUpdate.GroupNo);
                ViewBag.ListOfmembersByGroup = ListOfMembersByGroup(GroupToUpdate.GroupNo);
                ViewBag.ListOfPersons = ListOfPersons();
                return View("ViewEditGroup", groupmodel);
            }
            GroupEdit Dummygroupmodel = new GroupEdit() { GroupNo = 0, GroupDesc = "", GroupPersonNo = 0 };
            ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(Dummygroupmodel.GroupNo);
            ViewBag.ListOfmembersByGroup = ListOfMembersByGroup(Dummygroupmodel.GroupNo);
            ViewBag.ListOfPersons = ListOfPersons();
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
            //if (person.AddToGroup == true)
            //    AddGroupMember(person);

            return View("Index", ListOfPersons());
        }
        /*
         *
         * View members of the group - Done
         * Add member from current users - Done
         * Remove a member from group - Done
         * View marks of class member per work item
         * ViewEditWork - Fix "Work Name" is shows only part of the name and List of work items below - Done
         * Generate a text file report for a user and their work.
         * 
         */

        public ActionResult ViewPersonDetail(int PersonNo)
        {
            Person PersonDetail = unitOfWork.PersonRepository.FindBy(x => x.PersonNo == PersonNo);
            PersonModelEdit personmodel = new PersonModelEdit()
            {
                PersonNo = PersonDetail.PersonNo,
                Name = PersonDetail.Name,
                Surname = PersonDetail.Surname,
                Gender = PersonDetail.GenderNo,
                ID = PersonDetail.IdNo,
                IDType = PersonDetail.IdTypeNo,
                PersonType = PersonDetail.PersonTypeNo,
                DOB = PersonDetail.DOB
            };
            return ViewUpdate(personmodel);
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
            return ViewEditGroup(GroupNo, new GroupEdit());
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
            ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(id);
            ViewBag.GroupNo = work.GroupNo;
            ViewBag.PersonNo = 707; /** Get from login **/
            ViewBag.ListOfWorkMembers = ListOfWorkMembers(id);



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

        public ActionResult ViewEditWorkDetail(int id, WorkEdit work)
        {
            ViewBag.ListOfWorkByGroup = ListOfWorkByGroup(work.GroupNo);
            ViewBag.GroupNo = work.GroupNo;
            ViewBag.PersonNo = 707; /** Get from login **/
            ViewBag.ListOfWorkMembers = ListOfWorkMembers(work.WorkNo);



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