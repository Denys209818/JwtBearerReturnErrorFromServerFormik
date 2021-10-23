using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowErrorsInFormik.Models
{
    public class RegisterViewModel
    {
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Phone { get; set; }
        public IFormFile Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
