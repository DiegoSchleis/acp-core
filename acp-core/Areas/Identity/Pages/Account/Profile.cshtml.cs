using acp_core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace acp_core.Areas.Identity.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<Athlete> _userManager;
        private readonly SignInManager<Athlete> _signInManager;

        public ProfileModel(
            UserManager<Athlete> userManager,
            SignInManager<Athlete> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Username")]
            public string Username { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Tell something about yourself...")]
            public string? Description { get; set; }
            [Display(Name = "Nationality")]
            public string? Nationality { get; set; }
            [Display(Name = "Gender")]
            [PersonalData]
            public string? Gender { get; set; }
            [Display(Name = "Weight")]
            [PersonalData]
            public decimal? Weight { get; set; }
            [Required]
            [Display(Name = "Birth Date")]
            [PersonalData]
            public DateTime? BirthDate { get; set; }
            [Display(Name = "Your max. heart rate")]
            public int? MaximalHeartRate { get; set; }
            [Display(Name = "Your FTP (Functional Threshold Power)")]
            public int? FunctionalThresholdPower { get; set; }
            [Display(Name = "Choose an Avatar")]
            public byte[]? Avatar { get; set; }
        }

        private async Task LoadAsync(Athlete user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Username = userName,
                PhoneNumber = phoneNumber,
                Avatar = user.Avatar,
                Gender = user.Gender,
                Weight = user.Weight,
                BirthDate = user.BirthDate,
                MaximalHeartRate = user.MaximalHeartRate,
                FunctionalThresholdPower = user.FunctionalThresholdPower,
                Description = user.Description,
                Nationality = user.Nationality
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            user.BirthDate = Input.BirthDate;
            user.Weight = Input.Weight != null ? Input.Weight : user.Weight;
            user.Description = Input.Description;
            user.MaximalHeartRate = Input.MaximalHeartRate != null ? Input.MaximalHeartRate : user.MaximalHeartRate;
            user.FunctionalThresholdPower = Input.FunctionalThresholdPower != null ? Input.FunctionalThresholdPower : user.FunctionalThresholdPower;

            if (Request.Form != null && Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                if (file != null)
                {
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        user.Avatar = dataStream.ToArray();
                    }
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var username = await _userManager.GetUserNameAsync(user);
            if(username != null && username != Input.Username)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "Error: " +setUserNameResult.Errors.First().Description;
                    return RedirectToPage();
                }
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
