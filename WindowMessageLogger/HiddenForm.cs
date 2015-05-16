using System;
using System.Windows.Forms;

namespace WindowMessageLogger
{
    public class HiddenForm : Form
    {
        protected override void DefWndProc(ref Message m)
        {
            Console.WriteLine(m.HWnd + ": " + m.LParam);
            base.DefWndProc(ref m);
        }
    }
}