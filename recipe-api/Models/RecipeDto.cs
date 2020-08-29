using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipe_api.Models
{
    public class RecipeDto
    {
        
        public long RecipeID { get; set; }
        public string Title { get; set; }

        public string[] Ingredients { get; set; }

        public string[] Steps { get; set; }

        public string Level { get; set; }

        
       
    }
}
