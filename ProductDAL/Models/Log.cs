namespace ProductDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //FIXME: DatabaseGeneratedOption.Identity throws null reference exception during adding new value
        public int Id { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }
        public string ActionDescription { get; set; }
        public string Request { get; set; }
    }
}
