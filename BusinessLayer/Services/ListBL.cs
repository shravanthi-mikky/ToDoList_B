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

        public ListItemModel AddTask(ListItemModel book, int UserId)
        {
            try
            {
                return iListRL.AddTask(book, UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
