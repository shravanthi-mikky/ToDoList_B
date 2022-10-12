using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ListItemModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string dueDate { get; set; }
        public DateTime startDate { get; set; }

        public int UserId { get; set; }

    }

    public class ListItemDelete
    {
        public int ItemId { get; set; }
    }
}
