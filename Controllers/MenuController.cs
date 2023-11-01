using Microsoft.AspNetCore.Mvc;
using Rebar.Models;
using Rebar.Services;


namespace Rebar.Controllers
{
    [ApiController]

    [Route("api/Rebar")]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController()
        {
            _menuService = new MenuService();
        }

        [HttpPost("createBasicMenu")]
        public IActionResult PostCreateBasicMenu()
        {
            try
            {
                _menuService.CreateMenu();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpGet("menu")]
        public Task<List<Shake>?> Get()
        {
            Task<List<Shake>?> result = _menuService.GetMenu();
            return result;
        }

        [HttpPost("newShake")]
        public async Task<ActionResult> PostNewShake([FromBody] Shake shake)
        {
            try
            {
                var result = _menuService.CreateNewShake(shake);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the shake: " + ex.Message);
            }

            return Ok();
        }

        [HttpPost("newListShake")]
        public async Task<ActionResult> PostNewShakes([FromBody] List<Shake> shakes)
        {

            try
            {
                var result = _menuService.MultipleNewShake(shakes);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while creating the shakes: " + ex.Message);
            }

            return Ok();
        }

        [HttpPost("newOrder")]
        public IActionResult PostNewOrder([FromBody] ClientOrder order)
        {

            try
            {
                var result = _menuService.TakeOrder(order);
            }
            catch (Exception ex)
            {
                return BadRequest("Failure: " + ex.Message);
            }

            return this.Ok("Your order has been successfully received");
        }

        [HttpGet("dailyReport")]
        public IActionResult GetDailyReport(int password)
        {
            try
            {
                string message = _menuService.EndOfDay(password);
                return this.Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest("Failure: " + ex.Message);
            }

        }
    }
}
