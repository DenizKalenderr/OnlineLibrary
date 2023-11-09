using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private  AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        public void Update(Book book)
        {
            _appDbContext.Update(book);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        
    }
}
