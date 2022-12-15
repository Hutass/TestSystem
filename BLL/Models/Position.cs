using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PositionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public PositionModel() { }
        public PositionModel(Position p)
        {
            ID = p.ID;
            Name = p.Name;
        }
    }
}
