using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Roulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        AdminRoulette Admin;

        public RouletteController() {
            Admin = new AdminRoulette();
        }
        /// <summary>
        ///    It's insert new roulette
        /// </summary>
        /// <returns>Id to roulette</returns>
        [HttpGet]
        [Route("Insert")]
        public IActionResult Insert()
        {
            try
            {
                int result = Admin.CreateRoulette();

                return Ok(result);

            }
            catch(Exception e)
            {
                return BadRequest();
            }
            
        }
        /// <summary>
        /// Open the roulette for It's can send wager 
        /// </summary>
        /// <param name="id">Id to roulette</param>
        /// <returns>confirmation message</returns>
        [HttpGet("Open/{id}")]
        public IActionResult Open(string id)
        {
            try
            {
                bool result = Admin.OpenRoulette(id);
                if (result)
                {
                    return Ok("La ruleta fue actualizada exitosamente");
                }
                else
                {
                    return Ok("Error intentar abrir la ruleta");
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// List to roulette
        /// </summary>
        /// <returns>List to roulette and roulette state</returns>
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            try
            {
                Dictionary<string,string> result = Admin.ListRoulette();

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
