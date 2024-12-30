using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SkeinGang.AdminUI.Models;

public class LoginForm
{
    [FromForm, Required]
    public required string Username { get; set; }
    
    [FromForm, Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
