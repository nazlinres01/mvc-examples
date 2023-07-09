using System;

namespace FilmListesiUygulamasi
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string UserName { get; internal set; }
        public string Email { get; internal set; }
    }
}
