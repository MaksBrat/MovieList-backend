﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Domain.ResponseModels.Chat
{
    public class MessageResponse
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string AvatarUrl { get; set; }
        public string DateCreated { get; set; } 
    }
}
