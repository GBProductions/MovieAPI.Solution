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
  [Route("api/movies/{movieId}/[controller]")]
  [ApiController]
  public class ActorsController : ControllerBase
  {
    private readonly MovieAPIContext _db;
    public ActorsController(MovieAPIContext db)
    {
      _db = db;
    }

  //GET api/movies/1/actors

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Actor>>> Get()
  {
    return await _db.Actors.ToListAsync();
  }
  // [HttpGet]
  // public async Task<ActionResult<IEnumerable<Actor>>> Get(string name, int age, bool oscarWinner, int movieId)
  // {
    // var query = _db.Actors.AsQueryable();

    //   if (name != null)
    //   {
    //     query = query.Where(entry => entry.Name == name);
    //   }

    //   if (age != null)
    //   {
    //     query = query.Where(entry => entry.Age == age);
    //   }

    //   if (oscarWinner != null)
    //   {
    //     query = query.Where(entry => entry.OscarWinner == oscarWinner);
    //   }

    //   if (movieId != 0)
    //   {
    //     query = query.Where(entry => entry.MovieId == movieId);
    //   }
      // return await query.ToListAsync();
  // }

    //POST api/movies/1/actors
    [HttpPost]
    public async Task<ActionResult<Actor>> Post(int movieId, Actor actor)
    {
      actor.MovieId = movieId;
      _db.Actors.Add(actor);
      await _db.SaveChangesAsync();

      return CreatedAtAction("Post", new { id = actor.ActorId, actor});
      // return CreatedAtAction(nameof(GetActor), new { id = actor.ActorId }, actor);
    }

    //Get: api/movies/1/actors/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Actor>> GetActor(int id)
    {
      var actor = await _db.Actors.FindAsync(id);

      if (actor == null)
      {
        return NotFound();
      }

      return actor;
    }



  }
}