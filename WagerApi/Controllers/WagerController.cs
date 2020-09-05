using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Roulette.Models;

namespace RouletteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagerController : ControllerBase
    {
        AdminWager Admin;
        public WagerController()
        {
            Admin = new AdminWager();
        }
        /// <summary>
        /// send new wager
        /// </summary>
        /// <returns>confirmation message </returns>
        [HttpPost]
        [Route("CreateWager")]
        public IActionResult CreateWager([FromBody] Wager ObjWager)
        {
            try
            {
                IHeaderDictionary headers = Request.Headers;
                if (headers.ContainsKey("user"))
                {
                    string iduser = headers["user"];
                    string result = Admin.CreateWager(ObjWager, iduser);

                    return Ok(result);
                    
                }
                else 
                {
                    return StatusCode(403);
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// close the roulette 
        /// </summary>
        /// <param name="id">Id to roulette</param>
        /// <returns>List to wager</returns>
        [HttpGet("ClosetWager/{id}")]
        public IActionResult ListWager(string id)
        {
            try
            {
                string result = Admin.ListWager(id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }

}
