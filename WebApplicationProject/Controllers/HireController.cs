using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationProject.Models;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class HireController : Controller
    {
        private readonly IHireRepository _hireRepository;
        private readonly IBookRepository _bookRepository;
        public readonly IWebHostEnvironment _webHostEnvironment; //Verileri gönderip almak için yrd sınıfı
        public HireController(IHireRepository hireRepository, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _hireRepository = hireRepository;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<Book> objBookList = _bookRepository.GetAll().ToList();//vt'dan gidip verileir çekip listeye atayacak.

            List<Hire> objHireList = _hireRepository.GetAll(includeProps: "Book").ToList();
            return View(objHireList);
        }

        //GET
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.BookName,
                Value = k.Id.ToString(),
            });

            ViewBag.BookList = BookList;

            if(id==null || id == 0)
            {
                //ekleme
                return View();
            }
            else
            {
                //Güncelleme
                Hire? hireDb = _hireRepository.Get(u => u.Id == id); //Buraya gönderdiğim id ye eşit olan kaydı getir. Expression<Func<T, bool>> filtre
                if (hireDb == null)
                {
                    return NotFound();
                }
                return View(hireDb);

            }
        }

        [HttpPost]
        public IActionResult AddUpdate(Hire hire)
        {
            
            if (ModelState.IsValid)
            {
                
                
                if (hire.Id == 0)
                {
                    _hireRepository.Add(hire);
                    TempData["basarili"] = "Yeni Kiralama Kaydı başarıyla oluşturuldu!";
                }
                else
                {
                    _hireRepository.Update(hire);
                    TempData["basarili"] = "Kiralama Kayıt güncelleme başarılı!";
                }

                _hireRepository.Save();//Bunu görünce de vt'na gidip kayıt işlemini atıyor. SaveChanges() yapmazsan bilgiler vt'a eklenmez.
                
                return RedirectToAction("Index", "Hire"); //Action Adı, Controller Adı
            }
            return View();        
        }

        // GET ACTIIOM
        public IActionResult Delete(int? id)
        {

            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.BookName,
                Value = k.Id.ToString(),
            });

            ViewBag.BookList = BookList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Hire? hireDb = _hireRepository.Get(u => u.Id == id);
            if (hireDb == null)
            {
                return NotFound();
            }
            return View(hireDb);
        }

        // POST ACTION

        [HttpPost, ActionName("Delete")]
        public IActionResult DELETEPOST(int? id)
        {
            Hire? hire = _hireRepository.Get(u => u.Id == id);
            if(hire == null)
            {
                return NotFound();
            }
            _hireRepository.Remove(hire);
            _hireRepository.Save();
            TempData["basarili"] = "Silme işlemi başarılı!";
            return RedirectToAction("Index","Hire");//Tekrar listeye döneriz.
        }
    }
}
