using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public int Gender { get; set; }
        public IEnumerable<PersonType> PersonTypes { get; set; }
        public IEnumerable<IDType> IDTypes { get; set; }

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
}