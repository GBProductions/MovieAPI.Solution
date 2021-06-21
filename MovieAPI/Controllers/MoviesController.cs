using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MovieAPI.Models;
using System;
using System.Linq;

namespace MovieAPI.Controllers 
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoviesController : ControllerBase
  {
    private readonly MovieAPIContext _db;

    public MoviesController(MovieAPIContext db)
    {
      _db = db;
    }

    //Get: api/Movies
    [HttpGet]

    // public async Task<ActionResult<IEnumerable<Movie>>> Get()
    // {
    //   return await _db.Movies.ToListAsync();
    // }
    public async Task<ActionResult<IEnumerable<Movie>>> Get(string name, string genre, string director, int year, string leadChar, string supportChar, double rating)
    {
      var query = _db.Movies.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      if (genre != null)
      {
        query = query.Where(entry => entry.Genre == genre);
      }

      if (director != null)
      {
        query = query.Where(entry => entry.Director == director);
      }

      if (year != 0)
      {
        query = query.Where(entry => entry.Year == year);
      }

      if (leadChar != null)
      {
        query = query.Where(entry => entry.LeadChar == leadChar);
      }

      if (supportChar != null)
      {
        query = query.Where(entry => entry.SupportChar == supportChar);
      }

      if (rating != 0)
      {
        query = query.Where(entry => entry.Rating == rating);
      }

      return await query.ToListAsync();
    }

    //POST api/movies
    [HttpPost]
    public async Task<ActionResult<Movie>> Post(Movie movie)
    {
      _db.Movies.Add(movie);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieId }, movie);
    }

    // Get: api/Movies.{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
      var movie = await _db.Movies.FindAsync(id);
      if (movie == null)
      {
        return NotFound();
      }
      return movie;
    }

    //PUT: api/movies/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Movie movie)
    {
      if (id != movie.MovieId)
      {
        return BadRequest();
      }

      _db.Entry(movie).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }

      catch (DbUpdateConcurrencyException)
      {
        if (!MovieExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }


    private bool MovieExists(int id)
    {
      return _db.Movies.Any(e => e.MovieId == id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
      var movie = await _db.Movies.FindAsync(id);
      if (movie == null)
      {
        return NotFound();
      }
      _db.Movies.Remove(movie);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    //GET: Movie randomizer
    [HttpGet("random")]
    public async Task<ActionResult<Movie>> GetRandomMovie(int id)
    {
      // int totalMovies = await _db.Movies.
      // var randomMovie = await _db.Movies.FindAsync(id)
      int highestId = _db.Movies.Max(u => u.MovieId);
      Random rnd = new Random();
      int randomId = rnd.Next(1, highestId);
      var randomMovie = await _db.Movies.FindAsync(randomId);

      return randomMovie;

    }
  }
}
// 
// int highestId = db.Movies.Max(u => u.MovieId);
// Random rnd = new Random();
// int randomId = rnd.Next(1, highestId);
// var randomMovie = await _db.Movies.FindAsync(randomMovie);




// randomMovie = db.Movies.OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(1).First();

// return (from movies where )
