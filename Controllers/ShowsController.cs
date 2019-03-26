using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
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
        public ActionResult<List<TvShowShort>> Get()
        {
            return _tvShowsService.GetAllShows();
        }

        // GET api/shows/episodes
        [HttpGet("episodes")]
        public ActionResult<List<Episode>> GetEpisodes()
        {
            return _tvShowsService.GetEpisodes();
        }

        // GET api/shows/read
        // [HttpGet("read")]
        // public ActionResult<Boolean> ReadConfigFile()
        // {
        //     _tvShowsService.ReadConfigFile();
        //     return StatusCode(200);
        // }

        // GET api/shows/upload
        [HttpPost("upload")]
        public ActionResult<Boolean> UploadConfigFile(IFormFile file)
        {
            // _tvShowsService.ReadFileNew(file)
            var result = string.Empty;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                result = reader.ReadToEnd();
            }

            var temp = string.IsNullOrEmpty(result) ? null : result.Split('\n').ToList();

            var finalResult = _tvShowsService.ReadFromConfigFile(temp);
            if (finalResult)
            {
                return StatusCode(200);
            } else {
                return StatusCode(500);
            }
        }


        // GET api/shows/topten
        [HttpGet("topten")]
        public ActionResult<ICollection<TvShowShort>> GetTopTen()
        {
            return _tvShowsService.GetTopTen();
            // return StatusCode(200);
        }

        // GET api/shows/toptensave
        [HttpGet("toptensave")]
        public ActionResult<ICollection<TvShowShort>> GetTopTenAndSave()
        {
            return _tvShowsService.GetTopTenAndSave();
            // return StatusCode(200);
        }

        // GET api/shows/genres
        [HttpGet("genres")]
        public ActionResult<ICollection<string>> GetUserGenres()
        {
            return _tvShowsService.GetUserGenres();
            // return StatusCode(200);
        }

        // GET api/shows/nextweek
        [HttpGet("nextweek")]
        public ActionResult<ICollection<EpisodeShort>> GetNextWeek()
        {
            return _tvShowsService.GetNextWeek();
        }

        // GET api/shows/network
        [HttpGet("network")]
        public ActionResult<ICollection<TvShowNetwork>> GetByNetwork()
        {
            return _tvShowsService.GetByNetwork();
            // return StatusCode(200);
        }

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