using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RehberWebApi.Models.DataContext;

namespace RehberWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaporController : ControllerBase
    {
        private readonly RehberDbContext _context;
        public RaporController(RehberDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var konumlar = _context.IletisimBilgileris.GroupBy(p => p.Konum).Select(s => s.Key).ToList();
            var result = from konum in konumlar
                         select new
                         {
                             Konum = konum,
                             KisiSayisi = _context.IletisimBilgileris.Where(p => p.Konum == konum).Count(),
                             TelefonSayisi = _context.IletisimBilgileris.Where(p => p.Konum == konum && p.TelefonNumarasi != "").Count(),

                         };

            return Ok(result.OrderBy(p => p.KisiSayisi).ToList());
        }
    }
}
