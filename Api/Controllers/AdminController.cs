using api.TransferModels;
using infrastructure.DataModels;

namespace library.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using service;

[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Admin>> GetAdmin(int id)
    {
        var admin = await _adminService.GetAdminByIdAsync(id);
        if (admin == null)
        {
            return NotFound();
        }
        return Ok(admin);
    }

    // Additional methods for Create, Update, Delete, etc.


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        var isAuthenticated = await _adminService.AuthenticateAdminAsync(model.Username, model.Password);

        if (isAuthenticated)
        {
            // Authentication successful
            return Ok(new { MessageToClient = "Successfully logged in" });
        }

        // Authentication failed
        return Unauthorized(new { MessageToClient = "Invalid login attempt" });
    }
}
