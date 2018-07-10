using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Wynlist.Data.Entities;

namespace Wynlist.Data
{
    public interface IWynlistRespository
    {

        IEnumerable<List> GetAllLists(string username);
        List GetListById(string username, int id);

        IEnumerable<ListType> GetAllListTypes();
        ListType GetListTypeById(int id);

        IEnumerable<Recipe> GetAllRecipes(string username);
        Recipe GetRecipeById(string username, int recipeId);

        IEnumerable<RecipeType> GetAllRecipeTypes();
        RecipeType GetRecipeTypeById(int id);

        IEnumerable<Store> GetAllStores();
        Store GetStoreById(int id);

        IEnumerable<Item> GetAllItems();
        Item GetItemById(int id);

        //Need to add Stores and Items to this later

        bool SaveAll();
        Task<bool> SaveAllAsync();

        void AddEntity(object model);

    }
}
