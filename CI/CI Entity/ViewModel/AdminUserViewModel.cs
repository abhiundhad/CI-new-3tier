﻿using CI_Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Entity.ViewModel
{
    public class AdminUserViewModel
    {
        public List<User> users { get; set; }

        [Required(ErrorMessage = "First Name is a Required field.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "FirstName")]
        [RegularExpression("^((?!^First Name$)[a-zA-Z '])+$", ErrorMessage = "First name  must be properly formatted.")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Last Name is a Required field.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "LastName")]
        [RegularExpression("^((?!^Last Name$)[a-zA-Z '])+$", ErrorMessage = "First name  must be properly formatted.")]
        public string lastname { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string email { get; set; }
        public string employeeid { get; set; }
        [Required(ErrorMessage = "Please Provide password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should contain atleast 8 charachter")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password should contain atleast one Capital letter , one small case letter, one Digit and one special symbol")]
            public string password { get; set; }
        public string status { get; set; }
        public string department { get; set; }
        public IFormFile files { get; set; }
        public string avatar { get; set; }
        public string profiletext { get; set; }
        public long? cityid { get; set; }
        public long? userId { get; set; }
        public long? countryid { get; set; }
        public List<City> allcity { get; set; }
        public List<Country> allcountry { get; set; }


    }
}