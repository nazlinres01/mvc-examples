using FilmListesiUygulamasi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmListesiUygulamasi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly FilmContext _context;

        public UsersController(FilmContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            // Kullanıcı adı ve e-posta benzersiz olmalıdır
            if (await _context.Users.AnyAsync(u => u.UserName == model.UserName))
            {
                ModelState.AddModelError("UserName", "Kullanıcı adı zaten kullanılıyor.");
                return BadRequest(ModelState);
            }

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "E-posta adresi zaten kullanılıyor.");
                return BadRequest(ModelState);
            }

            // Kullanıcıyı veritabanına kaydet
            var user = new Users
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password // Şifrenin doğru şekilde şifrelenmesi için gerekli işlemler yapılmalıdır
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Kullanıcı başarıyla kaydedildi.");
        }
    }
}
