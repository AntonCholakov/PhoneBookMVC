using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PhoneBookMVC.Filters
{
    public class EmailCustomAttribute : ValidationAttribute
    {
        public EmailCustomAttribute() : base("Invalid email") { }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string email = value as string;

            if (String.IsNullOrEmpty(email))
                return new ValidationResult("Email field cannot be empty");

            // Return true if email is in valid e-mail format.
            if(!Regex.IsMatch(email,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase))
            {
                return new ValidationResult("Email is not in a correct format");
            }

            return ValidationResult.Success;
        }
    }
}