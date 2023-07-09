public class FilmDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Director { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    public double AverageRating { get; set; }
    public int UserRating { get; set; }
    public List<string> UserNotes { get; set; }
}
