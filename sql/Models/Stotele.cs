using System;
using System.Collections.Generic;
using System.Text;

namespace sql.Models
{
    public class Stotele : ITable
    {
        public Stotele()
        {
        }
        public string Name { get; set; }
        public int MarsrutoId { get; set; }
        public string Laikas { get; set; }
        public override string ToString()
        {
            return Name + " " + MarsrutoId.ToString() + " " + Laikas;
        }
    }
}
