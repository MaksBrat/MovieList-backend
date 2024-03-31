﻿using System.ComponentModel.DataAnnotations;

namespace MovieList.Domain.RequestModels.Profile
{
    public class ProfileRequest
    {
        public int Id { get; set; }     
        public string Name { get; set; }

        [Range(1, 110, ErrorMessage = "Invalid age")]
        public int? Age { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime RegistratedAt { get; set; }
    }
}
