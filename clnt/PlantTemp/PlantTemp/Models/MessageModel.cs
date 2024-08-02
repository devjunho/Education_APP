using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantTemp.Models
{
    public class MessageModel
    {
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string isRead { get; set; }
        public string ImagePath { get; set; }
        public int Width{ get; set; }
        public int Height { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public MessageModel()
        {

        }
    }
}