using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IListRL
    {
        public ListItemModel AddTask(ListItemModel book, int UserId);

        //public ListItemModel AddTask(ListItemModel book);

        public bool DeleteItem(ListItemDelete listItemDelete);
    }
}
