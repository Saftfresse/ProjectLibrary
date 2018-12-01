using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectClassLib
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Tag { get; set; }
        public object Value { get; set; }

        public ComboBoxItem()
        {

        }

        public override string ToString()
        {
            return Text;
        }
    }
}
