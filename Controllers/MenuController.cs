using Microsoft.AspNetCore.Mvc;

using Rebar.Models;
using Rebar.DataAccess;
using Rebar.Services;
using System.Collections.Generic;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rebar.Controllers
{
    [ApiController]

    [Route("api/shake")]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;


        public MenuController()
        {
            _menuService = new MenuService();
        }


        [HttpGet]
        public Task<List<Shake>?> Get()
        {
           
            Task<List<Shake>?> result = _menuService.GetMenu();
            Console.WriteLine(result);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> PostNewShake([FromBody] Shake shake)
        {
            try
            {
                var result =_menuService.CreateNewShake(shake);
               
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the shake: " + ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public Task<ActionResult> PostNewOrder(ClientOrder order)
        {
            try
            {
                var result = _menuService.TakeOrder(order);

            }
            catch (Exception ex)
            {
                return BadRequest("jjj " + ex.Message);
            }
            return Ok();
        }

        /*
         * 
         * 
         * 
         * 
         * 
      
        

         * 
         * 
        [HttpGet]
        public async Task<List<Shake>> PostNewShake()
        {
            return await _menuService.GetMenu();
        }
         * [HttpGet]
        public void GetCheckout()
        {
             _menuService.EndOfDay();
            return 
        }
         Product item = repository.Get(id);
    if (item == null)
    {
        var message = string.Format("Product with id = {0} not found", id);
        HttpError err = new HttpError(message);
        return Request.CreateResponse(HttpStatusCode.NotFound, err);
    }
    else
    {
        return Request.CreateResponse(HttpStatusCode.OK, item);
    }
         
         
         */


        //CreateNewShake
        //TakeOrder
        //EndOfDay

    }
}
