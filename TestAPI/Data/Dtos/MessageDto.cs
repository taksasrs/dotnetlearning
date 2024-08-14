using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Data.Dtos
{
    public class MessageDto
    {
        public string message { get; set; }

        public string author { get; set; }

        public DateTime dateTime { get; set; } = DateTime.Now;
    }
}