using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly TvShowsService _tvShowsService;

        public ShowsController(TvShowsService tvShowsService)
        {
            _tvShowsService = tvShowsService;
        }

        // GET api/shows
        [HttpGet]
        public ActionResult<List<TvShow>> Get()
        {
            return _tvShowsService.Get();
        }

        // GET api/shows/read
        [HttpGet("read")]
        public ActionResult<List<TvShow>> ReadConfigFile()
        {
            _tvShowsService.ReadConfigFile();
            return StatusCode(200);
        }


        // // GET api/shows/topten
        // [HttpGet]
        // public ActionResult<IEnumerable<string>> GetTopTen()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // GET api/shows/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/shows
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/shows/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/shows/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}