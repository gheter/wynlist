using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Wynlist.Data;
using Wynlist.Data.Entities;

namespace Wynlist.Controllers
{
    [Route("api/[Controller]")]
    public class StoresController : Controller
    {
        private readonly IWynlistRespository _respository;
        private readonly ILogger<StoresController> _logger;

        public StoresController(IWynlistRespository respository, ILogger<StoresController> logger)
        {
            _respository = respository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_respository.GetAllStores());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Stores: {ex}");
                return BadRequest("Filed to get all Stores");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var store = _respository.GetStoreById(id);

                if (store != null) return Ok(store);
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Store: {ex}");
                return BadRequest("Filed to get Store");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody]Store model)
        {
            //add it to the db
            try
            {
                _respository.AddEntity(model);
               if (_respository.SaveAll()){
                    return Created($"/api/Stores/{model.Id}", model);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new Store: {ex}");
            }

            return BadRequest("Failed to save new Store");
        }
    }
}