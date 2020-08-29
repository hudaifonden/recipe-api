using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using recipe_api.Filters;
using recipe_api.Models;
using recipe_api.Services.Interface;
using AutoMapper;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace recipe_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository RecipeRepository;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            RecipeRepository = recipeRepository;
            _mapper = mapper;
        }

        //[EnableBodyRewind]
        [DisableFormValueModelBinding]
        [EnableCors("CorsPolicy")]
        [Route("addRecipe")]
        [System.Web.Http.HttpPost, DisableRequestSizeLimit]

        public void AddRecipe(IFormCollection Data)
        {
            Recipe recipe = new Recipe();
            RecipeDto recipeDTO = new RecipeDto();
            var retData = new ResponseEntity<Recipe>();
            try
            {
               
                foreach(var key in Data.Keys)
                {

                    if (key.ToUpper() == "TITLE")
                    {
                        recipeDTO.Title = Data[key].ToString();
                    }
                    else if (key.ToUpper() == "RECIPEID")
                    {
                        recipeDTO.RecipeID = Convert.ToInt64(Data[key].ToString());
                    }
                    else if (key.ToUpper() == "INGREDIENTS")
                    {
                        recipeDTO.Ingredients = Data[key].ToString().Split(",");
                    }
                    else if (key.ToUpper() == "STEPS")
                    {
                        recipeDTO.Steps = Data[key].ToString().Split(",");
                    }
                    else if (key.ToUpper() == "LEVEL")
                    {
                        recipeDTO.Level = Data[key].ToString();
                    }
                    
                    
                }

                recipe = _mapper.Map<Recipe>(recipeDTO);

                int i = 1;
                
                    foreach (var file in Data.Files)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                if (i == 1)
                                {
                                    recipe.Image1 = fileBytes;
                                }
                                else if (i == 2)
                                {
                                    recipe.Image2 = fileBytes;
                                }
                                else
                                {
                                    recipe.Image3 = fileBytes;
                                }

                            }
                        }
                        i++;
                    }
                
                
                
                

                var result = RecipeRepository.AddRecipe(recipe);
                retData = result;

            }
            catch(Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.InnerException.Message, System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);

            }
            //return retData;
        }

        [EnableCors("CorsPolicy")]
        [Route("GetAllRecipes")]
        [HttpGet]
        public IEnumerable<Recipe> GetAllRecipes()
        {
            IEnumerable<Recipe> Recipes = new List<Recipe>();
            try
            {
                Recipes = RecipeRepository.GetAllRecipes().ReturnData;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.InnerException.Message, System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            return Recipes;
        }

        [EnableCors("CorsPolicy")]
        [Route("GetRecipe/{id:int}")]
        [HttpGet]
        public Recipe GetRecipe(int id)
        {
            
            Recipe Recipe = new Recipe();
            try
            {
                Recipe = RecipeRepository.GetRecipe(id).ReturnData;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.InnerException.Message, System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            return Recipe;
        }

        [EnableCors("CorsPolicy")]
        [Route("GetAllRecipesWithoutImage")]
        [HttpGet]
        public IEnumerable<RecipeDto> GetAllRecipesWithoutImage()
        {
            IEnumerable<RecipeDto> Recipes = new List<RecipeDto>();
            try
            {
                Recipes = RecipeRepository.GetAllRecipesWithoutImages().ReturnData;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.InnerException.Message, System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            return Recipes;
        }


    }
}
