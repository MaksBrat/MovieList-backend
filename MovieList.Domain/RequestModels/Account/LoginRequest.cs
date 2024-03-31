﻿using System.ComponentModel.DataAnnotations;

namespace MovieList.Domain.RequestModels.Account
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Password length must be between 6 and 30 characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
