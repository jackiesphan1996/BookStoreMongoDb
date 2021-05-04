using System;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMongoDb.Client.Models
{
    public class AuthorViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }
    }
}
