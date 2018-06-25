using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Wynlist.Data.Entities;

namespace Wynlist.Data
{
    public class WynlistRespository : IWynlistRespository
    {
        private readonly WynlistContext _ctx;
        private readonly ILogger<WynlistRespository> _logger;

        public WynlistRespository(WynlistContext ctx, ILogger<WynlistRespository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
        //Need to add Stores and Items to this later

        
        public bool SaveAll() //SAVE ALL
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<ListType> GetAllListTypes() // Get all ListTypes
        {
            try
            {
                return _ctx.ListTypes
                    .OrderBy(l => l.ListTypeName)
                    .ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all ListTypes: {ex}");
                return null;
            }
        }

        public ListType GetListTypeById(int id) // Get ListType by Id
        {
            try
            {
                return _ctx.ListTypes
                    .Where(l => l.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get ListType: {ex}");
                return null;
            }
        }

        public IEnumerable<List> GetAllLists(string username) //Get all Lists
        {
            //_logger.LogInformation("GetAllLists was called.");
            try
            {
                return _ctx.Lists
                    .Where(l => l.User.UserName == username)
                    .OrderBy(l => l.ListName)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Lists: {ex}");
                return null;
            }
        }

        public List GetListById(string username, int id) //Get List by Id
        {
            try
            {
                return _ctx.Lists
                    .Where(l => l.User.UserName == username && l.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get List: {ex}");
                return null;
            }
        }

        public IEnumerable<Recipe> GetAllRecipes(string username) //Get all Recipes
        {
            try
            {
                    return _ctx.Recipes
                    .Where(r => r.User.UserName == username)
                    .OrderBy(r => r.RecipeName)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Recipes: {ex}");
                return null;
            }
        }

        public Recipe GetRecipeById(string username, int recipeId) //Get Recipe by Id
        {
            try
            {
                return _ctx.Recipes
                    .Where(r => r.User.UserName == username && r.Id == recipeId)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Recipe: {ex}");
                return null;
            }
        }

        public IEnumerable<RecipeType> GetAllRecipeTypes() //Get all RecipeTypes
        {
            try
            {
                return _ctx.RecipeTypes
                    .OrderBy(r => r.RecipeTypeName)
                    .ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all RecipeTypes: {ex}");
                return null;
            }
        }

        public RecipeType GetRecipeTypeById(int id) //Get RecipeType by Id
        {
            try
            {
                return _ctx.RecipeTypes
                    .Where(r => r.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get RecipeType: {ex}");
                return null;
            }
        }

        public IEnumerable<Store> GetAllStores()
        {
            try
            {
                return _ctx.Stores
                    .OrderBy(s => s.StoreName)
                    .ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Stores: {ex}");
                return null;
            }
        }

        public Store GetStoreById(int id) //Get Store by Id
        {
            try
            {
                return _ctx.Stores
                    .Where(r => r.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Store: {ex}");
                return null;
            }
        }

        public IEnumerable<Item> GetAllItems() //Get all Items
        {
            try
            {
                return _ctx.Items
                    .OrderBy(i => i.ItemName)
                    .ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Items: {ex}");
                return null;
            }
        }

        public Item GetItemById(int id) //Get Item by Id
        {
            try
            {
                return _ctx.Items
                    .Where(i => i.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Item: {ex}");
                return null;
            }
        }

    }
}