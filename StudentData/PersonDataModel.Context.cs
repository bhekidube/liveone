﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InstituteEntities : DbContext
    {
        public InstituteEntities()
            : base("name=InstituteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<IDType> IDTypes { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonType> PersonTypes { get; set; }
        public virtual DbSet<GroupType> GroupTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkType> WorkTypes { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<GroupPersonType> GroupPersonTypes { get; set; }
        public virtual DbSet<GroupPerson> GroupPersons { get; set; }
    
        public virtual int sp_test_print_out(ObjectParameter printMessages)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_test_print_out", printMessages);
        }
    
        public virtual ObjectResult<string> sp_test_print_out_to_select()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_test_print_out_to_select");
        }
    
        public virtual ObjectResult<test_NewGroupMembers_Result> test_NewGroupMembers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<test_NewGroupMembers_Result>("test_NewGroupMembers");
        }
    }
}
