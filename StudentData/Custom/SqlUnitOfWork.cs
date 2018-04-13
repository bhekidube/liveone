using PersonData;
using System;
using System.Data.Entity.Infrastructure;

namespace StudentData.Custom
{
    public class UnitOfWork : IDisposable
    {
        private IObjectContextAdapter _context = new InstituteEntities();
        private GenericRepository<Person> _personRepository;

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

