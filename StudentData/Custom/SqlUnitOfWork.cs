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

