using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wynlist.Data.Entities;

namespace Wynlist.Data
{

    public class WynlistSeeder
    {
        private readonly WynlistContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private UserManager<WynUser> _userManager;


        public WynlistSeeder(WynlistContext ctx, 
            IHostingEnvironment hosting,
            UserManager<WynUser> userManager)
        {
            _userManager = userManager;
            _ctx = ctx;
            _hosting = hosting;
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("gabeheter@hotmail.com");

            if (user == null)
            {
                user = new WynUser()
                {
                    FirstName = "Gabe",
                    LastName = "Heter",
                    UserName = "gabeheter@hotmail.com",
                    Email = "gabeheter@hotmail.com"
                };

                var result = await _userManager.CreateAsync(user, "Gabe_ki11");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to creat default User");
                }
            }

            if (!_ctx.ListTypes.Any())
            {
                //Need to create sample data
                //var filepath = Path.Combine(_hosting.ContentRootPath, "Data/list.json");
                //var json = File.ReadAllText(filepath);
                //var lists = JsonConvert.DeserializeObject<IEnumerable<List>>(json);
                //_ctx.List.AddRange(lists);

                var listtype = new ListType()
                {
                    ListTypeName = "Grocery"
                };

                _ctx.ListTypes.Add(listtype);
                _ctx.SaveChanges();
            }

            if (!_ctx.Lists.Any())
            {
                //Need to create sample data
                //var filepath = Path.Combine(_hosting.ContentRootPath, "Data/list.json");
                //var json = File.ReadAllText(filepath);
                //var lists = JsonConvert.DeserializeObject<IEnumerable<List>>(json);
                //_ctx.List.AddRange(lists);

                var list = new List()
                {
                    User = user,
                    ListName = "My Grocery",
                    ListTypeId = 5          
                };

                _ctx.Lists.Add(list);
                _ctx.SaveChanges();
            }

            if (!_ctx.RecipeTypes.Any())
            {
                //Need to create sample data
                //var filepath = Path.Combine(_hosting.ContentRootPath, "Data/list.json");
                //var json = File.ReadAllText(filepath);
                //var lists = JsonConvert.DeserializeObject<IEnumerable<List>>(json);
                //_ctx.List.AddRange(lists);

                var recipetype = new RecipeType()
                {
                    RecipeTypeName = "Dinner"
                };

                _ctx.RecipeTypes.Add(recipetype);
                _ctx.SaveChanges();
            }

            if (!_ctx.Recipes.Any())
            {
                //Need to create sample data
                //var filepath = Path.Combine(_hosting.ContentRootPath, "Data/list.json");
                //var json = File.ReadAllText(filepath);
                //var lists = JsonConvert.DeserializeObject<IEnumerable<List>>(json);
                //_ctx.List.AddRange(lists);

                var recipe = new Recipe()
                {
                    RecipeName = "Tacos (ground beef)",
                    RecipeTypeId = 1
                };

                _ctx.Recipes.Add(recipe);
                _ctx.SaveChanges();
            }
        }
        
    }

}
