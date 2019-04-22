using PersonData;
using System;
using System.Data.Entity.Infrastructure;

namespace StudentData.Custom
{
    public class UnitOfWork : IDisposable
    {
        private IObjectContextAdapter _context = new InstituteEntities();
        private GenericRepository<Person> _personRepository;
        private GenericRepository<PersonType> _personTypeRepository;
        private GenericRepository<IDType> _idTypeRepository;
        private GenericRepository<GroupType> _groupTypeRepository;
        private GenericRepository<Group> _groupRepository;
        private GenericRepository<Work> _workRepository;
        private GenericRepository<WorkType> _workTypeRepository;
        private GenericRepository<GroupPerson> _groupPersonRepository;
        private GenericRepository<WorkPersonResult> _workPersonResultRepository;



        public GenericRepository<Person> PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new GenericRepository<Person>(_context.ObjectContext);
                }
                return _personRepository;
            }
        }
        public GenericRepository<PersonType> PersonTypeRepository
        {
            get
            {
                if (_personTypeRepository == null)
                {
                    _personTypeRepository = new GenericRepository<PersonType>(_context.ObjectContext);
                }
                return _personTypeRepository;
            }
        }

        public GenericRepository<GroupPerson> GroupPersonRepository
        {
            get
            {
                if (_groupPersonRepository == null)
                {
                    _groupPersonRepository = new GenericRepository<GroupPerson>(_context.ObjectContext);
                }
                return _groupPersonRepository;
            }
        }
        public GenericRepository<IDType> IdTypeRepository
        {
            get
            {
                if (_idTypeRepository == null)
                {
                    _idTypeRepository = new GenericRepository<IDType>(_context.ObjectContext);
                }
                return _idTypeRepository;
            }
        }

        public GenericRepository<GroupType> GroupTypeRepository
        {
            get
            {
                if (_groupTypeRepository == null)
                {
                    _groupTypeRepository = new GenericRepository<GroupType>(_context.ObjectContext);
                }
                return _groupTypeRepository;
            }
        }
        public GenericRepository<Group> GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GenericRepository<Group>(_context.ObjectContext);
                }
                return _groupRepository;
            }
        }

        //public GenericRepository<GroupMember> GroupMemberRepository
        //{
        //    get
        //    {
        //        if (_groupMemberRepository == null)
        //        {
        //            _groupMemberRepository = new GenericRepository<GroupMember>(_context.ObjectContext);
        //        }
        //        return _groupMemberRepository;
        //    }
        //}

        public GenericRepository<Work> WorkRepository
        {
            get
            {
                if (_workRepository == null)
                {
                    _workRepository = new GenericRepository<Work>(_context.ObjectContext);
                }
                return _workRepository;
            }
        }
        public GenericRepository<WorkType> WorkTypeRepository
        {
            get
            {
                if (_workTypeRepository == null)
                {
                    _workTypeRepository = new GenericRepository<WorkType>(_context.ObjectContext);
                }
                return _workTypeRepository;
            }
        }
        public GenericRepository<WorkPersonResult> WorkResultRepository
        {
            get
            {
                if (_workPersonResultRepository == null)
                {
                    _workPersonResultRepository = new GenericRepository<WorkPersonResult>(_context.ObjectContext);
                }
                return _workPersonResultRepository;
            }
        }
        public void Save()
        {
            _context.ObjectContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.ObjectContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

