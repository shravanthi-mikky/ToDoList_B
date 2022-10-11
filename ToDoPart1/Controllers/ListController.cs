using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ToDoPart1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListController : ControllerBase
    {
        IListBL iListBL;
        public ListController(IListBL iListBL)
        {
            this.iListBL = iListBL;
        }
        [Authorize]

        [HttpPost("AddItem")]
        public IActionResult AddTask(ListItemModel listItemModel)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "id").Value);

                var result = iListBL.AddTask(listItemModel, userid);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Note Added Successfully", result = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add note" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
