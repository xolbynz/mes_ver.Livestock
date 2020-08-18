using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace 스마트팩토리.Controls
{
    [ToolboxBitmap(typeof(MaskedTextBox))]
    public partial class conMaskedTextBox : MaskedTextBox
    {
        private void conMaskedTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void conMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private Color saveBackColor = Color.White;
        private Color focusedBackColor = Color.White;
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("입력모드의 배경색을 지정하세요.")]
        public Color _FocusedBackColor
        {
            get { return focusedBackColor; }
            set { focusedBackColor = value; }
        }

        private Color borderColor = Color.White;
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("테두리 색을 지정하세요.")]
        public Color _BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public conMaskedTextBox()
        {
            saveBackColor = BackColor;
            this.AutoSize = false;
            this.KeyPress += new KeyPressEventHandler(conMaskedTextBox_KeyPress);
            this.KeyDown += new KeyEventHandler(conMaskedTextBox_KeyDown);
        }

        private static int WM_PAINT = 0x000F;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);
                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                ControlPaint.DrawBorder(g, bounds, borderColor, ButtonBorderStyle.Solid);
                g.Dispose();
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            saveBackColor = BackColor;
            BackColor = focusedBackColor;
            SendKeys.Send("^a");
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            BackColor = saveBackColor;
            base.OnLeave(e);
        }

    }
}
