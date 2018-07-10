using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Wynlist.Data;
using Wynlist.Data.Entities;
using System.Threading.Tasks;
using AutoMapper;
using WYNlist.ViewModels;

namespace Wynlist.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RecipesController : Controller
    {
        private readonly IWynlistRespository _respository;
        private readonly ILogger<RecipesController> _logger;
        private readonly UserManager<WynUser> _userManager;
        private readonly IMapper _mapper;

        public RecipesController(IWynlistRespository respository, ILogger<RecipesController> logger, IMapper mapper, UserManager<WynUser> userManager)
        {
            _respository = respository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;

        }

        [HttpGet] //Get all recipes
        public IActionResult Get()
        {
            try
            {
                //var username = User.Identity.Name;
                return Ok(_respository.GetAllRecipes(User.Identity.Name));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Recipes: {ex}");
                return BadRequest("Filed to get all Recipes");
            }
        }

        [HttpGet("{recipeId:int}")] //Get Recipe by Id
        public IActionResult Get(int recipeId)
        {
            try
            {
                // var username = User.Identity.Name;
                var recipe = _respository.GetRecipeById(User.Identity.Name, recipeId);

                if (recipe != null) return Ok(recipe);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Recipe: {ex}");
            }
            return BadRequest("Filed to get Recipe");
        }

        [HttpPost] //Add a new Recipe to the Db
        public async Task<IActionResult> Post([FromBody] RecipeViewModel model)
        {
            //add it to the db
            try
            {
                if (ModelState.IsValid)
                {
                    var newRecipe = _mapper.Map<RecipeViewModel, Recipe>(model);

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newRecipe.User = currentUser; //associates the user to the recipe

                    _respository.AddEntity(newRecipe);
                    if (_respository.SaveAll())
                    {
                        return Created($"/api/recipes/{newRecipe.Id}", _mapper.Map<Recipe, RecipeViewModel>(newRecipe));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new Recipe: {ex}");
            }
            return BadRequest("Failed to save new Recipe");
        }

        [HttpPut("{recipeId:int}")]
        public async Task<IActionResult> Put(int recipeId, [FromBody] RecipeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                var oldRecipe = _respository.GetRecipeById(User.Identity.Name, recipeId);
                if (oldRecipe == null) return NotFound($"Could not find a recipe with ID of {recipeId}");

                _mapper.Map(model, oldRecipe);

                if (_respository.SaveAll())
                {
                    return Ok(_mapper.Map<RecipeViewModel>(oldRecipe));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update Recipe: {ex}");
            }

            return BadRequest("Couldn't update recipe");
        }
    }
}