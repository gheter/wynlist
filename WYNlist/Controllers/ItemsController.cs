using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Wynlist.Data;
using Wynlist.Data.Entities;

namespace Wynlist.Controllers
{
    [Route("api/[Controller]")]
    public class ItemsController : Controller
    {
        private readonly IWynlistRespository _respository;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IWynlistRespository respository, ILogger<ItemsController> logger)
        {
            _respository = respository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_respository.GetAllItems());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Items: {ex}");
                return BadRequest("Filed to get all Items");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var item = _respository.GetItemById(id);

                if (item != null) return Ok(item);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Item: {ex}");
                return BadRequest("Filed to get Item");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody]Item model)
        {
            //add it to the db
            try
            {
                _respository.AddEntity(model);
               if (_respository.SaveAll()){
                    return Created($"/api/Items/{model.Id}", model);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new Item: {ex}");
            }

            return BadRequest("Failed to save new Item");
        }
    }
}