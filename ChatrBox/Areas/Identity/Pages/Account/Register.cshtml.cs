// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using ChatrBox.Areas.API.Controllers;
using ChatrBox.Areas.Config.Controllers;
using ChatrBox.CoreComponents.API;
using ChatrBox.Data;
using ChatrBox.Models;
using ChatrBox.Models.CommunityControls;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace ChatrBox.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ChatrBox.Data.Chatr> _signInManager;
        private readonly UserManager<ChatrBox.Data.Chatr> _userManager;
        private readonly IUserStore<ChatrBox.Data.Chatr> _userStore;
        private readonly IUserEmailStore<ChatrBox.Data.Chatr> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ChatrBox.Data.Chatr> userManager,
            IUserStore<ChatrBox.Data.Chatr> userStore,
            SignInManager<ChatrBox.Data.Chatr> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

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
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Username, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Username, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        //create user's default community
                        var defaultIcon = (ImageBase)ImageUploader.AssignDefaultIcon();

                        var userCom = new Community
                        {
                            OwnerId = user.Id,
                            Name = $"{Input.Username}'s Community",
                            Description = $"{Input.Username}'s personal community. NOTE: This community " +
                                $"was automatically generated upon joining this Chat'r Box server feel free " +
                                $"to modify or delete this community at your lesure.",
                            Visibility = Models.CommunityControls.Visibility.Closed,
                            ContentFilter = Models.CommunityControls.ContentFilter.Censored,
                            ImageUrl = defaultIcon.ImageUrl,
                            ImageHash = defaultIcon.ImageHash
                        };

                        _context.Communities.Add(userCom);
                        _context.SaveChanges(); //required for Id assignment

                        //add user to his own community. Added with Visibility.Open so user
                        //can post in their own community, the community is set to Private
                        //by default.
                        var comUser = CommunityUser.Create(userCom.Id, user.Id, Visibility.Open); 
                        
                        _context.CommunityUsers.Add(comUser);

                        var topic = new Topic
                        {
                            Name = "general",
                            Description = "AUTO GENERATED",
                            CommunityId = userCom.Id,
                            LastPost = DateTime.UtcNow,
                            PostPermission = Models.CommunityControls.PostPermission.Open
                        };

                        _context.Topics.Add(topic);
                        _context.SaveChanges();

                        var cheddar = _context.Users.FirstOrDefault(c => c.UserName == "Cheddar_Chatr")
                            ?? throw new InvalidOperationException("System account not found! Consider " +
                            "updating database.");

                        var welcomeMsg = new Message
                        {
                            SenderId = cheddar.Id,
                            MessagePlain = System.IO.File.ReadAllText(Path.Combine(AdminController.HomePath, "AutomatedMessages", "welcome.txt")),
                            Timestamp = DateTime.UtcNow,
                            TopicId = topic.Id,
                            IsEdited = false
                        };

                        _context.Messages.Add(welcomeMsg);
                        _context.SaveChanges();

                        //add user to announcemnet community

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ChatrBox.Data.Chatr CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ChatrBox.Data.Chatr>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ChatrBox.Data.Chatr)}'. " +
                    $"Ensure that '{nameof(ChatrBox.Data.Chatr)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ChatrBox.Data.Chatr> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ChatrBox.Data.Chatr>)_userStore;
        }
    }
}
