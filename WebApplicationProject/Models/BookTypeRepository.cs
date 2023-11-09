using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
    {
        private  AppDbContext _appDbContext;
        public BookTypeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        public void Update(BookType bookType)
        {
            _appDbContext.Update(bookType);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        
    }
}
