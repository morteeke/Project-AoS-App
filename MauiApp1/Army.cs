using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class Army
    {
        public List<Unit> Units { get; set; }
        public string Name { get; set; }
     
        
        public Army(string name, Unit unit) 
        {
            Units = new List<Unit>();
            Name = name;
            Units.Add(unit);
        }
    }
}
