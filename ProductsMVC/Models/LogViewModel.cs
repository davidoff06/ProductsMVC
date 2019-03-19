using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsMVC.Models
{
    public class LogViewModel
    {
        public int Id { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }
        public string ActionDescription { get; set; }
        public string Request { get; set; }
    }
}