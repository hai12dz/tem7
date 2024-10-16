using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tem7.Models;
using tem7.Models.TranDauModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tem7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranDauAPIController : ControllerBase
    {

        QlgiaiBongDaContext db = new QlgiaiBongDaContext();

        [HttpGet("{maclb}")]
        public IEnumerable<TranDauTheoCLB> GetTranDauTheoMaCLB(string maclb)
        {
          

            var trandaus = (from p in db.Trandaus.Where(x=>x.Clbnha==maclb||x.Clbkhach==maclb)
                             select new TranDauTheoCLB
                             {
                                 TranDauId=p.TranDauId,
                                 NgayThiDau=p.NgayThiDau,
                                 Clbkhach=p.Clbkhach,
                                 Clbnha = p.Clbnha,
                                 Vong = p.Vong,
                                 SanVanDongId = p.SanVanDongId,
                                 KetQua = p.KetQua,
                                 TrangThai = p.TrangThai,
                                 Anh=p.Anh

                             }).ToList();

            return trandaus;
        }


    }
}
