using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class BookTypeController : Controller
    {

        private readonly IBookTypeRepository _bookTypeRepository;//_ olması projenin tamamında kullanılacağı anlamına gelir.
        public BookTypeController(IBookTypeRepository context)
        {
            _bookTypeRepository = context;
        }
        public IActionResult Index()
        {
            List<BookType> objBookTypeList = _bookTypeRepository.GetAll().ToList();//vt'dan gidip verileir çekip listeye atayacak.
            return View(objBookTypeList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Add(bookType);//Ef vt'nına veri atacağını anlıyor
                _bookTypeRepository.Save();//Bunu görünce de vt'na gidip kayıt işlemini atıyor. SaveChanges() yapmazsan bilgiler vt'a eklenmez.
                TempData["basarili"] = "Yeni Kitap Türü başarıyla oluşturuldu!";
                return RedirectToAction("Index", "BookType"); //Action Adı, Controller Adı
            }
            return View();        
        }

        public IActionResult Update(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            BookType? bookTypeDb = _bookTypeRepository.Get(u => u.Id == id); //Buraya gönderdiğim id ye eşit olan kaydı getir. Expression<Func<T, bool>> filtre
            if (bookTypeDb == null)
            {
                return NotFound();
            }
            return View(bookTypeDb);
        }

        [HttpPost]
        public IActionResult Update(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Update(bookType);//Ef vt'nına veri atacağını anlıyor
                _bookTypeRepository.Save();//Bunu görünce de vt'na gidip kayıt işlemini atıyor. SaveChanges() yapmazsan bilgiler vt'a eklenmez.
                TempData["basarili"] = "Yeni Kitap Türü başarıyla güncellendi!";
                return RedirectToAction("Index", "BookType"); //Action Adı, Controller Adı
            }
            return View();
        }

        // GET ACTION = delete.cshtml i getirir.
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType? bookTypeDb = _bookTypeRepository.Get(u => u.Id == id);
            if (bookTypeDb == null)
            {
                return NotFound();
            }
            return View(bookTypeDb);
        }

        // POST ACTION

        [HttpPost, ActionName("Delete")]
        public IActionResult DELETEPOST(int? id)
        {
            BookType? bookType = _bookTypeRepository.Get(u => u.Id == id);
            if(bookType == null)
            {
                return NotFound();
            }
            _bookTypeRepository.Remove(bookType);
            _bookTypeRepository.Save();
            TempData["basarili"] = "Silme işlemi başarılı!";
            return RedirectToAction("Index","BookType");//Tekrar listeye döneriz.
        }
    }
}
