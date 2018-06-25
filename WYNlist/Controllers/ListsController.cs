using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Wynlist.Data;
using Wynlist.Data.Entities;
using WYNlist.ViewModels;
using System.Threading.Tasks;

namespace Wynlist.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListsController : Controller
    {
        private readonly IWynlistRespository _respository;
        private readonly ILogger<ListsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<WynUser> _userManager;

        public ListsController(IWynlistRespository respository, ILogger<ListsController> logger, IMapper mapper, UserManager<WynUser> userManager)
        {
            _respository = respository;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet] //GetAllLists
        public IActionResult Get()
        {
            try
            {
                //var username = User.Identity.Name;
                return Ok(_respository.GetAllLists(User.Identity.Name));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get lists: {ex}");
                return BadRequest("Filed to get lists");
            }
        }

        [HttpGet("{id:int}")] //GetListById
        public IActionResult Get(int id)
        {
            try
            {
                var username = User.Identity.Name;
                var list = _respository.GetListById(username, id);

                if (list != null) return Ok(list);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get list: {ex}");
                return BadRequest("Filed to get list");
            }
        }

        [HttpPost] //SaveAll
        public async Task<IActionResult> Post([FromBody] ListViewModel model)
        {
            try //add it to the db
            {
                if (ModelState.IsValid)
                {
                    var newList = _mapper.Map<ListViewModel, List>(model);

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newList.User = currentUser; //associates the user to the list

                    _respository.AddEntity(newList);
                    if (_respository.SaveAll())
                    {
                        return Created($"/api/lists/{newList.Id}", _mapper.Map<List, ListViewModel>(newList));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new list: {ex}");
            }
            return BadRequest("Failed to save new List");
        }
    }
}