using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/UserOperation")]
public class UsersController : ControllerBase
{
    private static List<User> Users = new List<User>();

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        try
        {
            return Users;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        try
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return user;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            user.Id = Users.Count + 1;
            Users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Department = updatedUser.Department;
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            Users.Remove(user);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}