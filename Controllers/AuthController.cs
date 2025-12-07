using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce_ERP.Models;
using ECommerce_ERP.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using ECommerce_ERP.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace ECommerce_ERP.Controllers;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context; // Your EF Core DbContext
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    // GET: /Auth/RegisterBasic
    [HttpGet]
    public IActionResult RegisterBasic()
    {
        return View(new RegisterViewModel());
    }

    // POST: /Auth/RegisterBasic
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterBasic(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Check if email already exists
        if (_context.UserMaster.Any(u => u.UserEmail == model.UserEmail))
        {
            ModelState.AddModelError("UserEmail", "Email is already registered.");
            return View(model);
        }

        var user = new User
        {
            UserName = model.UserName,
            UserEmail = model.UserEmail
        };

        // Hash password before saving
        user.PasswordHash = _passwordHasher.HashPassword(user, model.UserPassword);

        _context.UserMaster.Add(user);
        await _context.SaveChangesAsync();

        // Redirect to Login after successful registration
        return RedirectToAction("LoginBasic", "Auth");
    }

    // GET: /Auth/LoginBasic
    [HttpGet]
    public IActionResult LoginBasic()
    {
        return View(new LoginViewModel());
    }

    // POST: /Auth/LoginBasic
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginBasic(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Find user by email or username
        var user = await _context.UserMaster
            .FirstOrDefaultAsync(u => u.UserEmail == model.EmailOrUsername || u.UserName == model.EmailOrUsername);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // Verify password
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // ? Create session
        //HttpContext.Session.SetInt32("UserId", user.UserId);
        //HttpContext.Session.SetString("UserName", user.UserName);
        //HttpContext.Session.SetString("UserEmail", user.UserEmail);

        // TODO: You could also use authentication cookies instead of session for production.

        var userSession = new UserSession
        {
            UserId = user.UserId,
            Role = user.UserRole,
            Name = user.UserName,
            Email = user.UserEmail,
            //ProfilePic = user.ProfilePic,
            Cart = new List<CartItem>(), // Empty cart initially
            Currency = "INR",
            Language = "en",
            LoginTime = DateTime.Now
        };

        // Store in session as JSON
        HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(userSession));
        if (userSession.Role == 2) 
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Index", "Dashboards");
        }
    }

    public IActionResult ForgotPasswordBasic() => View();
  //public IActionResult LoginBasic() => View();
  //public IActionResult RegisterBasic() => View();
}
