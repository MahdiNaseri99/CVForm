using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CVForm.Models;
using CVForm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVForm.Pages.forms
{
    public class Create : PageModel
    {
        [BindProperty] public CreateUserCommand Input { get; set; }
        private readonly UserService _service;
        private readonly SkillService _skillService;
        private readonly NationalityService _nationalityService;
        [BindProperty] public IList<SelectListItem> SkillList { get; set; }
        [BindProperty] public IList<SelectListItem> NationalityList { get; set; }
        
        public Create(UserService service, SkillService skillService, NationalityService nationalityService)
        {
            _service = service;
            _skillService = skillService;
            _nationalityService = nationalityService;
        }

        public IActionResult OnGet()
        {
            Input = new CreateUserCommand();
            SkillList = _skillService.GetSkills();
            NationalityList = _nationalityService.GetNationality();
            
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (!IsValidName(Input.FirstName) || !IsValidName(Input.LastName))
                    ModelState.AddModelError(String.Empty, "Only Characters Allowed");
                if (!IsValidEmail(Input.Email))
                    ModelState.AddModelError(String.Empty, "Email Format InCorrect");
                if (!IsValidPassword(Input.Password))
                    ModelState.AddModelError(String.Empty, "Password Is Not Accepted");
                
                if (ModelState.IsValid)
                {  
                    foreach (SelectListItem skill in SkillList)
                    {
                        if (skill.Selected)
                        {
                            var temp = new CreateSkillCommand
                            {
                                Id = Convert.ToInt32(skill.Value),
                                SkillName = skill.Text
                            };
                            Input.Skills.Add(temp);
                        }
                    }
                    foreach (SelectListItem nationality in NationalityList)
                    {
                        if (nationality.Selected)
                        {
                            var temp = new CreateNationalityCommand
                            {
                                Id = Convert.ToInt32(nationality.Value),
                                NationalityName = nationality.Text
                            };
                            Input.Nationalities.Add(temp);
                            // Input.Skills[i].Id = Convert.ToInt32(skill.Value);
                            // Input.Skills[i].SkillName = skill.Text;
                            // i++;
                        }
                    }
                    var id = await _service.CreateUser(Input);
                    return RedirectToPage("ViewCV", new {id = id});
                }
            }
            catch (Exception)
            {
                // TODO: Log error
                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured saving the User"
                );
            }

            //If we got to here, something went wrong
            return Page();
        }
        
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();
                    
                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private bool IsValidName(string name)
        {
            if (!Regex.Match(name, "^[a-zA-Z]*$").Success)
                return false;
            return true;
        }

        private bool IsValidPassword(string passwd)
        {
            // Min 8 char and max 16 char
            if (passwd.Length < 8 || passwd.Length > 16)
                return false;

            // One upper case
            if (!passwd.Any(char.IsUpper))
                return false;

            // Atleast one lower case
            if (!passwd.Any(char.IsLower))
                return false;

            // No white space
            if (passwd.Contains(" "))
                return false;

            // Check for one special character
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            
            foreach (char ch in specialChArray)
                if (passwd.Contains(ch))
                    return true;
            
            return false;
        }
    }
}