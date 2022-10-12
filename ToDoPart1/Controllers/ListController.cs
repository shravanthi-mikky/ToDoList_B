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
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                var result = iListBL.AddTask(listItemModel, userid);

                //var result = iListBL.AddTask(listItemModel);

                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Task Added Successfully", result = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add Task" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteBooks(ListItemDelete listItemDelete)
        {
            try
            {
                var reg = this.iListBL.DeleteItem(listItemDelete);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "List item Deleted Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to delete" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        //[Authorize]
        //[HttpGet("Get")]
        //public IActionResult GetAllBooks()
        //{
        //    try
        //    {
        //        var reg = this.iListBL.GetAllListItems();
        //        if (reg != null)

        //        {
        //            return this.Ok(new { Success = true, message = "All List Details", Response = reg });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { Success = false, message = "Unable to get details" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest(new { Success = false, message = ex.Message });
        //    }
        //}

        //[HttpPut("Update")]
        //public IActionResult UpdateList(ListItemDelete listItemDelete)
        //{
        //    try
        //    {
        //        var reg = this.iListBL.UpdateList(listItemDelete);
        //        if (reg != null)

        //        {
        //            return this.Ok(new { Success = true, message = "List Updated Sucessfull", Response = reg });
        //        }
        //        else
        //        {
        //            return this.BadRequest(new { Success = false, message = "List details not updated" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest(new { Success = false, message = ex.Message });
        //    }
        //}

    }
}
