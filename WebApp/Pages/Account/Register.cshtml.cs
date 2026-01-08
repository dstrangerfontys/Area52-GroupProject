using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Area52.Core.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Area52.WebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public RegisterModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            [Display(Name = "Naam")]
            [StringLength(100)]
            public string Name { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            [Display(Name = "E-mailadres")]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            [MinLength(6, ErrorMessage = "Wachtwoord moet minimaal 6 tekens lang zijn.")]
            public string Password { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Herhaal wachtwoord")]
            [Compare(nameof(Password), ErrorMessage = "Wachtwoorden komen niet overeen.")]
            public string ConfirmPassword { get; set; } = string.Empty;

            [Display(Name = "Telefoonnummer")]
            public string? Phone { get; set; }

            [Display(Name = "Adres")]
            public string? Address { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Bestaat er al een klant met dit e-mailadres?
            var existing = _customerRepository.GetByEmail(Input.Email);
            if (existing != null)
            {
                ModelState.AddModelError(nameof(Input.Email), "Er bestaat al een account met dit e-mailadres.");
                return Page();
            }

            // Nieuw customer-object op basis van de invoer
            var customer = new Customer
            {
                Name = Input.Name,
                Email = Input.Email,
                PasswordHash = PasswordHasher.Hash(Input.Password),
                Phone = Input.Phone,
                Address = Input.Address
            };

            customer = _customerRepository.Add(customer);

            // Na registreren direct inloggen (cookie zetten)
            await SignInCustomerAsync(customer);

            // Terug naar de homepage
            return RedirectToPage("/Index");
        }

        private async Task SignInCustomerAsync(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Name),
                new Claim(ClaimTypes.Email, customer.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);
        }
    }
}