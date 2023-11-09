using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        internal DbSet<T> dbSet; // dbSet = _appDbContext.BookTypes

        // Bu nesne dependency injectiondan geliyor. Biir kere oluşturulup hep kullanılıyor.
        public Repository(AppDbContext appDbContext)
        {
         _appDbContext = appDbContext;
            this.dbSet = _appDbContext.Set<T>();
            //asp.net core un özel include metodu. foreign key i getirecek.
            _appDbContext.Books.Include(k => k.BookType).Include(k => k.BookTypeId);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
 

        // Vt'dan bir kayıt getirir.
        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filtre);

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProp);

                }
            }

            return query.FirstOrDefault();//Tek bir sorgu getirmesini garantiler.
        }

        // Tüm listeyi getirir.
        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach(var includeProp in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                { 

                    query = query.Include(includeProp);
                
                }
            }

            return query.ToList();
        }

        //  Tek bir kaydı siler.
        public void Remove(T entity)
        {
            dbSet.Remove(entity);                       
        }

        // Birden fazla silmek istiyorsam - aralık siler
        public void DeleteThis(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
