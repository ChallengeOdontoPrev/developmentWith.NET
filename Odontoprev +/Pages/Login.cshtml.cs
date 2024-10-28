// Pages/Login.cshtml.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdontoPrev.Services;
using OdontoPrev.ViewModels;
using System.Security.Claims;

namespace OdontoPrev.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthorService _authorService;

        public LoginModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [BindProperty]
        public LoginViewModel LoginInput { get; set; } = new LoginViewModel();

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Author/Dashboard");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var author = await _authorService.AuthenticateAsync(LoginInput.Username, LoginInput.Password);

            if (author == null)
            {
                ErrorMessage = "Usuário ou senha inválidos";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, author.Username),
                new Claim(ClaimTypes.Email, author.Email),
                new Claim(ClaimTypes.NameIdentifier, author.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            // Redireciona para o Dashboard após o login
            return RedirectToPage("/Author/Dashboard");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}