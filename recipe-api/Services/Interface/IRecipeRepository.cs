using recipe_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipe_api.Services.Interface
{
    public interface IRecipeRepository
    {
        public ResponseEntity<Recipe> AddRecipe(Recipe Recipe);

        public ResponseEntity<List<Recipe>> GetAllRecipes();
        public ResponseEntity<List<RecipeDto>> GetAllRecipesWithoutImages();
        public ResponseEntity<Recipe> GetRecipe(long id);
    }
}
