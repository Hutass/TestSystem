using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RightsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public RightsModel() { }
        public RightsModel(Rights r)
        {
            ID = r.ID;
            Name = r.Name;
        }
    }
}
