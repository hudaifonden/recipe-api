using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace recipe_api.Models
{
    public class Recipe
    {
        [Key]
        public long RecipeID { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[] Image1 { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[] Image2 { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[] Image3 { get; set; }

        public string[] Ingredients { get; set; }

        public string[] Steps { get; set; }

        public string Level { get; set; }

        [NotMapped]
        public IFormFile Image1File {get;set;}
        [NotMapped]
        public IFormFile Image2File { get; set; }
        [NotMapped]
        public IFormFile Image3File { get; set; }

        
        public DateTime CreatedDate { get; set; }



        
    }

    
}
