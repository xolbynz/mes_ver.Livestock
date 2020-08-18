using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace 스마트팩토리.Controls
{
    [ToolboxBitmap(typeof(ComboBox))]
    public partial class conCheckBox : CheckBox
    {
        private void conComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.SendWait("{tab}");
            }
            //else if (e.KeyCode == Keys.Down)
            //{
            //    SendKeys.SendWait("{tab}");
            //}
            //else if (e.KeyCode == Keys.Up)
            //{
            //    SendKeys.SendWait("+{tab}");
            //}

        }

        public conCheckBox()
        {
            this.KeyDown += new KeyEventHandler(conComboBox_KeyDown);
        }

    }
}
