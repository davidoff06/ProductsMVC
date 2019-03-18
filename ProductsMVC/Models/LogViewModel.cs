using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsMVC.Models
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public byte[] Timestamp { get; set; }
        public Nullable<int> User_id { get; set; }
        public string Action_desciption { get; set; }
        public string Request { get; set; }
    }
}