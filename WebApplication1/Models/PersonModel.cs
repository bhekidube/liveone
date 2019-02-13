using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class PersonModelEdit
    {
        public int PersonNo { get; set; }
        public int PersonType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DOB { get; set; }
        public int IDType { get; set; }
        public string ID { get; set; }
        public int GroupType { get; set; }
        public int Group { get; set; }
        public int Gender { get; set; }
        public IEnumerable<PersonType> PersonTypes { get; set; }
        public IEnumerable<IDType> IDTypes { get; set; }
        public IEnumerable<GroupType> GroupTypes { get; set; }
        public IEnumerable<GroupEdit> Groups { get; set; }
        public bool AddToGroup { get; set; }

    }

    public class IDType
    {
        public int IDTypeNo { get; set; }
        public string IDTypeDesc { get; set; }
    }
    public class PersonType
    {
        public int PersonTypeNo { get; set; }
        public string PersonTypeDesc { get; set; }
    }
    public class GroupType
    {
        public int GroupTypeNo { get; set; }
        public string GroupTypeDesc { get; set; }
        public DateTime SystemDate { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GroupEdit
    {
        public int GroupNo { get; set; }
        public int GroupType { get; set; }
        public int GroupPersonNo { get; set; }
        public string GroupDesc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SystemDate { get; set; }
        public int SelectedPersonNo { get; set; }
        public IEnumerable<SelectListItem> Persons { get; set; }
        public IEnumerable<GroupType> GroupTypes { get; set; }
    }
    public class WorkEdit
    {
        public int WorkNo;
        public string WorkDesc;
        public int WorkTypeNo;
        public int GroupNo;
        public string GroupName;
        public int PersonNo;
        public DateTime StartDate;
        public DateTime EndDate;
        //public IEnumerable<GroupEdit> Groups { get; set; }
        public IEnumerable<WorkTypeEdit> WorkTypes { get; set; }
        public DateTime SystemDate;
    }
    public class WorkTypeEdit
    {
        public int WorkTypeNo;
        public string WorkTypeDesc;
        public DateTime SystemDate;
        public DateTime StartDate;
        public DateTime EndDate;
    }



}