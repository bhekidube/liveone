using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentData;

namespace PersonData
{
    public class GenericRepository<TEntity> :IRepository<TEntity>
        where TEntity : class
    {
        protected ObjectSet<TEntity> _objectSet;
        private ObjectContext context;

        public GenericRepository(ObjectContext context)
        {
            this.context = context;
            _objectSet = context.CreateObjectSet<TEntity>();
        }

        public void Add(TEntity newEntity) {
            _objectSet.AddObject(newEntity);
        }
        public void Remove(TEntity entity) {
            _objectSet.DeleteObject(entity);
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
            return _objectSet.Where(predicate);
        }// IQueryable - lets u compose queries VS IEnumerable
         /* "Expression<Func<TEntity, bool>> predicate" means the caller will provide a lambda 
          * expression based on the TEntity type, and this expression will return a Boolean value
         */
    }

    //Generic repository where we pass a type
    public interface IRepository<TEntity> 
    {
        void Add(TEntity newEntity);
        void Remove(TEntity entity);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);// IQueryable - lets u compose queries VS IEnumerable
    }


    /*public void DoWork(IPersonRepository repository)
    {
        repository.Add(new Person());
        repository.Remove(new Person()); 
        
        var managers = repository.GetAllManagers();
        or
        var otherManagers = repository.Find(e => e.IsManager == true);
     
    }*/

}
