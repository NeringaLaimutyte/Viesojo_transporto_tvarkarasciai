using System;
using System.Collections.Generic;
using System.Text;

namespace sql.Models
{
    public class Laikasssss : ITable
    {
        public Laikasssss()
        {
        }

        public int Stotele { get; set; }
        public string Laikass { get; set; }
        public override string ToString()
        {
            return Laikass;
        }
    }
}
