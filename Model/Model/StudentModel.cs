﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDemo.Models.Model
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(30, ErrorMessage = "Full Name must be at most 30 characters")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Full Name can only contain alphabets")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [StringLength(20, ErrorMessage = "User Name must be at most 20 characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(".*@gmail\\.com$", ErrorMessage = "Email must be from gmail.com domain")]
        public string? UserEmail { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 characters long.")]
        public string? Phone { get; set; }

      //  [Required(ErrorMessage = "Gender is required")]
        public string? GenderName { get; set; }


      //  [Required(ErrorMessage = "Gender is required")]
        public int? GenderID { get; set; }


      //  [Required(ErrorMessage = "State is required")]
        public int? StateID { get; set; }


      //  [Required(ErrorMessage = "State is required")]
        public string? StateName {  get; set; }

    }
}
