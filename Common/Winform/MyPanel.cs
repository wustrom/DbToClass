using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Common.Winform
{
    public class MyPanel : Panel
    {
        public MyPanel()
        {

            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x20;
                return cp;
            }
        }
    }
}
