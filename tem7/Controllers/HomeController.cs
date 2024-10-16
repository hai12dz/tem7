using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using tem7.Models;

namespace tem7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QlgiaiBongDaContext db = new QlgiaiBongDaContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var lstcaulacbo = db.Caulacbos.ToList();

            ViewBag.lsttrandau = db.Trandaus.Take(12).ToList();

            return View(lstcaulacbo);
        }

        public IActionResult Privacy()
        {
            return View();
        }




        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(string? id)
        {

            var trandau = db.Trandaus.Where(x => x.TranDauId == id).SingleOrDefault();

            ViewBag.SanVanDongId = new SelectList(db.Sanvandongs.ToList(), "SanVanDongId", "TenSan");
            ViewBag.Clbnha = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            ViewBag.Clbkhach = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            return View(trandau);

        }
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(Trandau trandau)
        {
            if (ModelState.IsValid)
            {
                db.Update(trandau);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");


        }



        [HttpGet]
        [Route("Create")]
        public IActionResult Create(string? id)
        {


            ViewBag.SanVanDongId = new SelectList(db.Sanvandongs.ToList(), "SanVanDongId", "TenSan");
            ViewBag.Clbnha = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            ViewBag.Clbkhach = new SelectList(db.Caulacbos.ToList(), "CauLacBoId", "TenClb");
            return View();

        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Trandau trandau)
        {
            if (ModelState.IsValid)
            {
                db.Trandaus.Add(trandau);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");


        }




        //[HttpGet]
        // [Route("Delete")]
        // public IActionResult Delete(string? id)
        // {
        //     /*
        //     // Xóa các b?n ghi liên quan trong b?ng TRANDAU_CAUTHU
        //     var trandaucauthu = db.TrandauCauthus.Where(x => x.TranDauId == id).ToList();
        //     foreach (var item in trandaucauthu)
        //     {
        //         db.TrandauCauthus.Remove(item);
        //     }

        //     // Ti?p t?c v?i các b?ng khác n?u c?n
        //     var trandaughiban = db.TrandauGhibans.Where(x => x.TranDauId == id).ToList();
        //     foreach (var item in trandaughiban)
        //     {
        //         db.TrandauGhibans.Remove(item);
        //     }
        //     */

        //     TempData["Message"] = "khong xoa duoc san pham nay";

        //     var trandaucauthu = db.TrandauCauthus.Where(x=>x.CauThuId==id);
        //     var trandaughiban = db.TrandauGhibans.Where(x => x.CauThuId == id);
        //     if (trandaughiban.Count() > 0 || trandaucauthu.Count() > 0)
        //     {

        //         TempData["Message"] = "khong xoa duoc san pham nay";
        //         return RedirectToAction("Index", "Home");
        //     }
        //     db.Remove(db.Trandaus.Where(x => x.TranDauId == id).SingleOrDefault());
        //     db.SaveChanges();
        //     return RedirectToAction("Index", "Home");

        // }


        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(string id)
        {
            TempData["Message"] = "";


            var trandaucauthu = db.TrandauCauthus.Where(x => x.TranDauId == id).ToList();
            var trandaughiban = db.TrandauGhibans.Where(x => x.TranDauId == id).ToList();

            if (trandaucauthu.Count > 0 || trandaughiban.Count > 0)
            {
                TempData["Message"] = "khong the xoa ban ghi nay.";
                return RedirectToAction("Index", "Home");
            }

            var tranDau = db.Trandaus.SingleOrDefault(x => x.TranDauId == id);
            if (tranDau != null)
            {
                db.Trandaus.Remove(tranDau);
                TempData["Message"] = "xoa thanh cong.";
            }
            else
            {
                TempData["Message"] = "Khong tim thay ban ghi de xoa.";
            }

            return RedirectToAction("Index", "Home");
        }















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
