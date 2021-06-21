using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
  public class Movie
  {
    public int MovieId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    public string Director { get; set; }

    [Required]
    public int Year { get; set; }

    [Required]
    public string LeadChar { get; set; }

    [Required]
    public string SupportChar { get; set; }

    [Required]
    public double Rating { get; set; }
  }
}