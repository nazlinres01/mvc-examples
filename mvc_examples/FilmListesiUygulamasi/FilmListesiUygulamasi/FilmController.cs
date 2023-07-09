using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FilmListesiUygulamasi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly FilmContext _context;
        private readonly IMailService _mailService;

        public FilmController(FilmContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms(int pageSize)
        {
            var films = await _context.Films.Take(pageSize).ToListAsync();
            return Ok(films);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            var film = await _context.Films.FirstOrDefaultAsync(f => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            var averageRating = CalculateAverageRating(id);
            var userRating = GetUserRating(id);
            var userNotes = GetUserNotes(id);

            var result = new
            {
                Film = film,
                AverageRating = averageRating,
                UserRating = userRating,
                UserNotes = userNotes
            };

            return Ok(result);
        }

        [HttpPost("{id}/ratings")]
        public async Task<IActionResult> AddRating(int id, [FromBody] Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var film = await _context.Films.FirstOrDefaultAsync(f => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            var rating = new Rating
            {
                FilmId = id,
                Score = model.Score,
                Note = model.Note
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return Created("", null);
        }

        [HttpPost("{id}/recommend")]
        public async Task<IActionResult> RecommendFilm(int id, [FromBody] EmailDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var film = await _context.Films.FirstOrDefaultAsync(f => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            _mailService.SendEmail(model.Email, "Film Tavsiyesi", $"Merhaba, size bir film tavsiyesi olarak {film.Title} adlı filmi öneriyoruz.");

            return Ok();
        }

        private double CalculateAverageRating(int filmId)
        {
            var ratings = _context.Ratings.Where(r => r.FilmId == filmId).Select(r => r.Score);
            if (ratings.Any())
            {
                return ratings.Average();
            }
            return 0;
        }

      private int GetUserRating(int filmId)
{
    // Kullanıcının verdiği puanı veritabanından al
    // Örneğin, kullanıcının puanını UserRating tablosundan çekebilirsiniz
    var rating = _context.UserRatings.FirstOrDefault(r => r.FilmId == filmId && r.UserId == currentUserId);
    if (rating != null)
    {
        return rating.Score;
    }
    return 0;
}

private List<string> GetUserNotes(int filmId)
{
    // Kullanıcının eklediği notları veritabanından al
    // Örneğin, kullanıcının notlarını UserNotes tablosundan çekebilirsiniz
    var notes = _context.UserNotes.Where(n => n.FilmId == filmId && n.UserId == currentUserId).Select(n => n.Note).ToList();
    return notes;
}

    }
}
