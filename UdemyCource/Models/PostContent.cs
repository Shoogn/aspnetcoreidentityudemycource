using System;
using System.Collections.Generic;

namespace UdemyCource.Models
{
    public class PostContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AddedBy { get; set; }
        public string Details { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
