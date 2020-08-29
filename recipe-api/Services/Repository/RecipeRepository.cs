using recipe_api.Models;
using recipe_api.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipe_api.Services.Repository
{
    
    public class RecipeRepository:IRecipeRepository
    {
        private readonly RecipeDBContext context;
        public RecipeRepository(RecipeDBContext recipeContext)
        {
            context = recipeContext;
        }
        public ResponseEntity<Recipe> AddRecipe(Recipe Recipe)
        {
            var retData = new ResponseEntity<Recipe>();
            try
            {
                if (Recipe.RecipeID > 0)
                {
                    context.Recipes.Update(Recipe);
                }
                else
                {
                    context.Recipes.Add(Recipe);
                }
                
                context.SaveChanges();
                retData.ReturnData = Recipe;
                retData.transactionStatus = TransactionStatus.Success;
            }
            catch (Exception ex)
            {
                retData.transactionStatus = TransactionStatus.Failed;
                retData.returnMessage = ex.InnerException.Message;
            }
            return retData;
        }

        public ResponseEntity<List<Recipe>> GetAllRecipes()
        {
            var retData = new ResponseEntity<List<Recipe>>();
            try
            {
                var allRecipes = context.Recipes.ToList();
                retData.ReturnData = allRecipes;
                retData.transactionStatus = TransactionStatus.Success;
            }
            catch (Exception ex)
            {
                retData.transactionStatus = TransactionStatus.Failed;
                retData.returnMessage = ex.InnerException.Message;
            }
            return retData;
        }

        public ResponseEntity<List<RecipeDto>> GetAllRecipesWithoutImages()
        {
            var retData = new ResponseEntity<List<RecipeDto>>();
            try
            {
                var allRecipes = context.Recipes.Select(R => new RecipeDto()
                {
                    Title = R.Title,
                    Ingredients = R.Ingredients,
                    Steps = R.Steps,
                    Level = R.Level,
                    RecipeID=R.RecipeID
                }).ToList();
                retData.ReturnData = allRecipes;
                retData.transactionStatus = TransactionStatus.Success;
            }
            catch (Exception ex)
            {
                retData.transactionStatus = TransactionStatus.Failed;
                retData.returnMessage = ex.InnerException.Message;
            }
            return retData;
        }

        public ResponseEntity<Recipe> GetRecipe(long id)
        {
            var retData = new ResponseEntity<Recipe>();
            try
            {
                var Recipe = context.Recipes.Find(id);
                retData.ReturnData = Recipe;
                retData.transactionStatus = TransactionStatus.Success;
            }
            catch (Exception ex)
            {
                retData.transactionStatus = TransactionStatus.Failed;
                retData.returnMessage = ex.InnerException.Message;
            }
            return retData;
        }
    }
}
