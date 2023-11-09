using WebApplicationProject.Utility;

namespace WebApplicationProject.Models
{
    public class HireRepository : Repository<Hire>, IHireRepository
    {
        private  AppDbContext _appDbContext;
        public HireRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        public void Update(Hire hire)
        {
            _appDbContext.Update(hire);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        
    }
}
