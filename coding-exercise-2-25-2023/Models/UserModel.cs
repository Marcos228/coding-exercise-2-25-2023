using Microsoft.AspNetCore.Mvc.RazorPages;
using coding_exercise_2_25_2023.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Net.Mail;

namespace coding_exercise_2_25_2023.Models
{
    public class UserModel
    {
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "The first name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "The last name is required.")]
        public string LastName { get; set; } = string.Empty;

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "The email is required.")]
        public string Email { get; set; } = string.Empty;

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "The date of birth is required.")]
        public DateTime? DateOfBirth { get; set; } 

        private int _age;
        public int Age { get {return _age;} }

        public void CalculateAge()
        {
            var today = DateTime.Today;
            _age = today.Year - DateOfBirth.Value.Year;
            if (DateOfBirth.Value.Date > today.AddYears(-_age)) _age--;
        }

        public void Validate() {
            if (!isEmailValid()) throw new Exceptions.ValidationException("Invalid email.");
            CalculateAge();
            if (Age < 18) throw new Exceptions.ValidationException("The user needs to be 18 years old or older.");
        }

        private bool isEmailValid()
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(Email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

    }
}
