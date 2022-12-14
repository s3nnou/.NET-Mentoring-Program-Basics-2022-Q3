using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.ClientModels;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace BrainstormSessions.Api
{
    public class IdeasController : ControllerBase
    {
        private readonly IBrainstormSessionRepository _sessionRepository;
        private readonly ILog _log = LogManager.GetLogger(typeof(IdeasController));

        public IdeasController(IBrainstormSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        #region snippet_ForSessionAndCreate
        [HttpGet("forsession/{sessionId}")]
        public async Task<IActionResult> ForSession(int sessionId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                _log.Warn($"There is no ideas for session with id = {sessionId}");
                return NotFound(sessionId);
            }

            var result = session.Ideas.Select(idea => new IdeaDTO()
            {
                Id = idea.Id,
                Name = idea.Name,
                Description = idea.Description,
                DateCreated = idea.DateCreated
            }).ToList();

            _log.Debug($"Succesfully retrived ideas {result} for session with id = {sessionId}");
            _log.Info($"Succesfully retrived ideas for session with id = {sessionId}");

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]NewIdeaModel model)
        {
            if (!ModelState.IsValid)
            {
                _log.Error($"Idea model is not valid.");
                return BadRequest(ModelState);
            }

            var session = await _sessionRepository.GetByIdAsync(model.SessionId);
            if (session == null)
            {
                _log.Warn($"There is no session with id = {model.SessionId}");
                return NotFound(model.SessionId);
            }

            _log.Info($"Succesfully retrived session with id = {model.SessionId}");

            var idea = new Idea()
            {
                DateCreated = DateTimeOffset.Now,
                Description = model.Description,
                Name = model.Name
            };

            session.AddIdea(idea);
            _log.Debug($"Succesfully created idea {idea} for session with id = {model.SessionId}");

            await _sessionRepository.UpdateAsync(session);

            _log.Info($"Succesfully added idea to the session with id = {model.SessionId}");

            return Ok(session);
        }
        #endregion

        #region snippet_ForSessionActionResult
        [HttpGet("forsessionactionresult/{sessionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<IdeaDTO>>> ForSessionActionResult(int sessionId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);

            if (session == null)
            {
                _log.Error($"There is no session with id = {sessionId}");
                return NotFound(sessionId);
            }

            var result = session.Ideas.Select(idea => new IdeaDTO()
            {
                Id = idea.Id,
                Name = idea.Name,
                Description = idea.Description,
                DateCreated = idea.DateCreated
            }).ToList();

            _log.Debug($"Succesfully retrived ideas {result} for session with id = {sessionId}");
            _log.Info($"Succesfully retrived ideas for session with id = {sessionId}");

            return result;
        }
        #endregion

        #region snippet_CreateActionResult
        [HttpPost("createactionresult")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BrainstormSession>> CreateActionResult([FromBody]NewIdeaModel model)
        {
            if (!ModelState.IsValid)
            {
                _log.Error($"Idea model is not valid.");
                return BadRequest(ModelState);
            }

            var session = await _sessionRepository.GetByIdAsync(model.SessionId);

            if (session == null)
            {
                _log.Error($"There is no session with id = {model.SessionId}");
                return NotFound(model.SessionId);
            }

            _log.Info($"Succesfully retrived session with id = {model.SessionId}");

            var idea = new Idea()
            {
                DateCreated = DateTimeOffset.Now,
                Description = model.Description,
                Name = model.Name
            };
            session.AddIdea(idea);

            _log.Debug($"Succesfully created idea {idea} for session with id = {model.SessionId}");

            await _sessionRepository.UpdateAsync(session);
            _log.Info($"Succesfully added idea to the session with id = {model.SessionId}");

            return CreatedAtAction(nameof(CreateActionResult), new { id = session.Id }, session);
        }
        #endregion
    }
}
