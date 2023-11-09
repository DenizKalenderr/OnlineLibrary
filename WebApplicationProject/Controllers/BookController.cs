using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationProject.Models;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Controllers
{
    
    public class BookController : Controller
    {
        // _bookRepository kullanarak tüm kitap listesini çekiyoruz.
        private readonly IBookRepository _bookRepository;//_ olması projenin tamamında kullanılacağı anlamına gelir.
        private readonly IBookTypeRepository _bookTypeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepository, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _bookTypeRepository = bookTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        //Sadece öğrenci ve admin girebilecek.
        [Authorize(Roles = "Admin, Student")]
        public IActionResult Index()
        {
            List<Book> objBookList = _bookRepository.GetAll(includeProps: "BookType").ToList();
            return View(objBookList);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> BookTypeList = _bookTypeRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.Id.ToString(),
            });

            ViewBag.BookTypeList = BookTypeList;

            if(id==null || id == 0)
            {
                //ekleme
                return View();
            }
            else
            {
                //Güncelleme
                Book? bookDb = _bookRepository.Get(u => u.Id == id); //Buraya gönderdiğim id ye eşit olan kaydı getir. Expression<Func<T, bool>> filtre
                if (bookDb == null)
                {
                    return NotFound();
                }
                return View(bookDb);

            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(Book book, IFormFile? file)
        {
            //var errors = ModelState.Values.SelectMany(x => x.Errors);


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img");

                //string bookPath = Path.Combine(wwwRootPath,@"img");// Combine, birleştirir. İşletim sistemine göre / yerleştirir.

                if(file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create)) // Dosyayı kopyalar, nereye kopyalanacağını da belirtir
                    {
                        file.CopyTo(fileStream);
                    }
                    book.ImageUrl = @"\img\" + file.FileName; // Resim bilgilerini aldık.
                }
                
                if (book.Id == 0)
                {
                    _bookRepository.Add(book);
                    TempData["basarili"] = "Yeni Kitap başarıyla oluşturuldu!";
                }
                else
                {
                    _bookRepository.Update(book);
                    TempData["basarili"] = "Kitap güncelleme başarılı!";
                }

                _bookRepository.Save();//Bunu görünce de vt'na gidip kayıt işlemini atıyor. SaveChanges() yapmazsan bilgiler vt'a eklenmez.
                
                return RedirectToAction("Index", "Book"); //Action Adı, Controller Adı
            }
            return View();        
        }

        /*public IActionResult Update(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Book? bookDb = _bookRepository.Get(u => u.Id == id); //Buraya gönderdiğim id ye eşit olan kaydı getir. Expression<Func<T, bool>> filtre
            if (bookDb == null)
            {
                return NotFound();
            }
            return View(bookDb);

        }*/
        /*
        [HttpPost]
        public IActionResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Update(book);//Ef vt'nına veri atacağını anlıyor
                _bookRepository.Save();//Bunu görünce de vt'na gidip kayıt işlemini atıyor. SaveChanges() yapmazsan bilgiler vt'a eklenmez.
                TempData["basarili"] = "Yeni Kitap başarıyla güncellendi!";
                return RedirectToAction("Index", "Book"); //Action Adı, Controller Adı
            }
            return View();
        }
        */

        // GET ACTION = delete.cshtml i getirir
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? bookDb = _bookRepository.Get(u => u.Id == id);
            if (bookDb == null)
            {
                return NotFound();
            }
            return View(bookDb);
        }

        // POST ACTION

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult DELETEPOST(int? id)
        {
            Book? book = _bookRepository.Get(u => u.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            _bookRepository.Remove(book);
            _bookRepository.Save();
            TempData["basarili"] = "Silme işlemi başarılı!";
            return RedirectToAction("Index","Book");//Tekrar listeye döneriz.
        }
    }
}
