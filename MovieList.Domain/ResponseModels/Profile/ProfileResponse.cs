using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Domain.ResponseModels.Profile
{
    public class ProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime RegistratedAt { get; set; }
    }
}
