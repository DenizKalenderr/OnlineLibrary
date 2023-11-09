using System.Linq.Expressions;

namespace WebApplicationProject.Models
{
    public interface IRepository<T> where T : class //Generic
    {
        // T-> BookType
        IEnumerable<T> GetAll(string? includeProps = null);//Hepsini getir
        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);//Bir şeyi getirirken seçim yaparak getireceğiz.
        void Add(T entity);
        void Remove(T entity);
        void DeleteThis(IEnumerable<T> entities);//Belirli bir aralığı silecek
    }
}
