using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class ListBL : IListBL
    {
        IListRL iListRL;
        public ListBL(IListRL iListRL)
        {
            this.iListRL = iListRL;
        }

        //public ListItemModel AddTask(ListItemModel book, int UserId)
        public ListItemModel AddTask(ListItemModel book, int UserId)
        {
            try
            {
               return iListRL.AddTask(book, UserId);
               // return iListRL.AddTask(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteItem(ListItemDelete listItemDelete)
        {
            try
            {
                // return iListRL.AddTask(book, UserId);
                return iListRL.DeleteItem(listItemDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ListItemModel> GetAllTasks()
        {
            try
            {
                return iListRL.GetAllTasks();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
