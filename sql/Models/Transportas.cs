using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace sql.Models
{
    public class Transportas : ITable
    {
        public string Numeris { get; set; }
        public string Stoteles { get; set; }
        public string LaikuSkirtumai { get; set; }
        public string PradinesStotelesLaikai { get; set; }
        public string PradinesStotelesLaikaiSavaitgaliais { get; set; }
        public string PradineGalutineStotele { get; set; }
        public string SavaitesDienos { get; set; }
        public string Priemone { get; set; }
        public bool PritaikytaNeigaliesiems { get; set; }
        public Transportas()
        {
        }
        public void PradineGalutineStoteleSet()
        {
            char[] sep = { ';' };
            string[] stoteles = Stoteles.Split(sep);
            PradineGalutineStotele = stoteles[0] + " - " + stoteles[stoteles.Length - 1];
        }
    }
}
