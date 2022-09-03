using acp_core.Models;
using acp_core.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Security.Claims;

namespace acp_core.Areas.Identity.Pages.Account
{
    public class FinishSetupModel : PageModel
    {
        private readonly UserManager<Athlete> _userManager;
        private readonly SignInManager<Athlete> _signInManager;

        public FinishSetupModel(
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
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Genders { get; set; }
        public byte[] RandomImage { get; set; }

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
            ///             [Phone]
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
                PhoneNumber = phoneNumber,
                Username = userName,
                Avatar = ImageHelper.GenerateRandomImageAsBytes(350, 350)
            };

            var countryList = new List<SelectListItem>();
            var isoList = ISO3166.Country.List.OrderBy(x => x.Name);
            for (var i = 0; i < isoList.Count(); i++)
            {
                countryList.Add(new SelectListItem { Value = i.ToString(), Text = isoList.ElementAt(i).Name });
            }
            Countries = countryList;

            Genders = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Male" },
                new SelectListItem { Value = "2", Text = "Female" }
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }
            else if (!user.InitialLogin)
            {
                var returnUrl = Url.Content("~/");
                return LocalRedirect(returnUrl);
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
            user.InitialLogin = false;
            if(user.UserName != Input.Username)
                user.UserName = Input.Username;

            user.BirthDate = Input.BirthDate;
            user.Gender = Input.Gender;
            user.Weight = Input.Weight;
            user.Nationality = Input.Nationality;
            user.Description = Input.Description;
            user.MaximalHeartRate = Input.MaximalHeartRate;
            user.FunctionalThresholdPower = Input.FunctionalThresholdPower;

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
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = "Thanks for registering";
            var returnUrl = Url.Content("~/");
            return LocalRedirect(returnUrl);
        }
    }
}
