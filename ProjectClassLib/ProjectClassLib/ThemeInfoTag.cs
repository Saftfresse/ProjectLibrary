using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClassLib
{
    public class ThemeInfoTag
    {
        string section, font, forecolor, backcolor, bordercolor, dashstyle;
        static int currId = 0;
        int id;
        ThemeInfoTag textInfo;

        public ThemeInfoTag()
        {
            Id = currId++;
        }

        public string Section { get => section; set => section = value; }
        public string Font { get => font; set => font = value; }
        public string Forecolor { get => forecolor; set => forecolor = value; }
        public string Backcolor { get => backcolor; set => backcolor = value; }
        public string Bordercolor { get => bordercolor; set => bordercolor = value; }
        public ThemeInfoTag TextInfo { get => textInfo; set => textInfo = value; }
        public string Dashstyle { get => dashstyle; set => dashstyle = value; }
        public int Id { get => id; set => id = value; }
    }
}
