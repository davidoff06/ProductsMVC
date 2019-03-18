namespace ProductDAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Log
    {
        public int Id { get; set; }
        public byte[] Timestamp { get; set; }
        public Nullable<int> User_id { get; set; }
        public string Action_description { get; set; }
        public string Request { get; set; }
    }
}
