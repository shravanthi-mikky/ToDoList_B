using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IListBL
    {
        public ListItemModel AddTask(ListItemModel book, int UserId);

        //public ListItemModel AddTask(ListItemModel book);

        public bool DeleteItem(ListItemDelete listItemDelete);

        public List<ListItemModel> GetAllTasks();
    }
}
