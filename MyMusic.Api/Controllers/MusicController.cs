﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.VisualBasic;
using MyMusic.Api.Resources;
using MyMusic.Api.Validations;
using MyMusic.Core.Models;
using MyMusic.Core.Services;

namespace MyMusic.Api.Controllers
{
    [Route("api/[controller]")] 
    [ApiController] 

    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;

        public MusicController(IMusicService musicService, IMapper mapper)
        {
            _musicService = musicService;
            _mapper = mapper;
        }

        // GET api/musics
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MusicResource>>> GetAllMusics()
        {
            var musics = await _musicService.GetAllWithArtist();
            var musicResources = _mapper.Map<IEnumerable<Music>, IEnumerable<MusicResource>>(musics);
            return Ok(musicResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MusicResource>> GetMusicById(int id)
        {
            var music = await _musicService.GetMusicById(id);
            var musicResource = _mapper.Map<Music, MusicResource>(music);
            return Ok(musicResource);
        }

        [HttpPost("")]

        public async Task<ActionResult<MusicResource>> CreateMusic([FromBody] SaveMusicResource saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

            var musicToCreate = _mapper.Map<SaveMusicResource, Music>(saveMusicResource);
            var newMusic = await _musicService.CreateMusic(musicToCreate);
            var music = await _musicService.GetMusicById(newMusic.Id);
            var musicResource = _mapper.Map<Music, MusicResource>(music);

            return Ok(musicResource);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MusicResource>> UpdateMusic(int id, [FromBody] SaveMusicResource saveMusicResource)
        {
            //validate the request data
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            if (id <= 0 || !validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            //check if the music item exists
            var musicToBeUpdated = await _musicService.GetMusicById(id);
            if (musicToBeUpdated == null)
            {
                return NotFound();
            }

            // Map the incoming resource to the domain entity
            var music = _mapper.Map<SaveMusicResource, Music>(saveMusicResource);

            // Update the music
            await _musicService.UpdateMusic(musicToBeUpdated, music);

            //get the updated music and return it
            var updatedMusic = await _musicService.GetMusicById(id);
            var updatedMusicResource = _mapper.Map<Music, MusicResource>(updatedMusic);

            return Ok(updatedMusicResource);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var music = await _musicService.GetMusicById(id);
            if (music == null)
            {
                return NotFound();
            }

            await _musicService.DeleteMusic(music);

            return NoContent();
        }


    }
}
