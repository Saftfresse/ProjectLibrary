using System;
using System.Windows.Forms;

namespace ProjectClassLib
{
    public class MyButton : Button
    {
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }
    }
}

