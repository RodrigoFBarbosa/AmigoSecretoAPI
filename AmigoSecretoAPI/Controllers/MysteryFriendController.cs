using AmigoSecretoAPI.Models;
using AmigoSecretoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmigoSecretoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MysteryFriendController : ControllerBase
{
    private readonly APIService _service;

    public MysteryFriendController()
    {
        _service = new APIService();
    }

    [HttpPost("group")]
    public ActionResult<Group> CreateGroup([FromBody] string name)
    {
        var group = _service.CreateGroup(name);
        return CreatedAtAction(nameof(CreateGroup), new { groupId = group.Id }, group);
    }

    [HttpPost("group/{groupId}/participant")]
    public IActionResult AddParticipant(Guid groupId, [FromBody] Participant participant)
    {
        try
        {
            _service.AddParticipant(groupId, participant);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("group/{groupId}/generate-matches")]
    public IActionResult GenerateMatches(Guid groupId)
    {
        try
        {
            _service.GenerateMatches(groupId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        } 
    }

    [HttpGet("group/{groupId}participants{participantId}/mystery-friend")]
    public ActionResult<Participant> GetMysteryFriend(Guid groupId, Guid participantId)
    {
        var mysteryFriend = _service.GetMysteryFriend(groupId, participantId);
        if (mysteryFriend == null) return NotFound("Friend not found.");
        return Ok(mysteryFriend);
    }
}
