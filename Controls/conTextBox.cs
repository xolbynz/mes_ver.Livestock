using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace 스마트팩토리.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public partial class conTextBox : TextBox
    {
        private void conTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && _AutoTab == true)
            {
                e.Handled = true;
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

        private void conTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private bool autoTab = true;
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("엔터키 입력시 자동 탭여부 설정하세요.")]
        public bool _AutoTab
        {
            get { return autoTab; }
            set { autoTab = value; }
        }
        
        private string waterMarkText = string.Empty; // 워터마크로 사용할 문자열
        private Color waterMarkColor = Color.Gray;   // 워터마크로 사용할 문자색
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("배경글을 입력하세요.")]
        public string _WaterMarkText
        {
            get { return waterMarkText; }
            set { waterMarkText = value; }
        }

        public Color _WaterMarkColor
        {
            get { return waterMarkColor; }
            set { waterMarkColor = value; }
        }

        public conTextBox()
        {
            saveBackColor = BackColor;
            this.AutoSize = false;
            this.KeyPress += new KeyPressEventHandler(conTextBox_KeyPress);
            this.KeyDown += new KeyEventHandler(conTextBox_KeyDown);
        }

        private static int WM_PAINT = 0x000F;
        protected override void WndProc(ref Message m)
        {
            // base.WndProc 중복 호출을 피하기 위해서
            bool isCallAlready = false;

            if (m.Msg == 0x000F) // WM_PAINT = 0x000F
            {
                // WM_PAINT 메세지를 받아서 처리
                // 원래 처리해야될 로직을 먼저 호출해서 처리해줌
                base.WndProc(ref m);
                isCallAlready = true;

                if (m.Msg == WM_PAINT)
                {
                    Graphics g = Graphics.FromHwnd(Handle);
                    Rectangle bounds = new Rectangle(0, 0, Width, Height);
                    ControlPaint.DrawBorder(g, bounds, borderColor, ButtonBorderStyle.Solid);
                    g.Dispose();
                }

                DrawWaterMarkText();
            }
            else if (m.Msg == 0x0008 && this.Multiline) // WM_KILLFOCUS = 0x0008
            {
                // Multiline == true 일때는 포커스 빠질때 WM_PAINT가 발생 안하므로
                DrawWaterMarkText();
            }

            if (false == isCallAlready)
                base.WndProc(ref m);

            //base.WndProc(ref m);

            //if (m.Msg == WM_PAINT)
            //{
            //    Graphics g = Graphics.FromHwnd(Handle);
            //    Rectangle bounds = new Rectangle(0, 0, Width, Height);
            //    ControlPaint.DrawBorder(g, bounds, borderColor, ButtonBorderStyle.Solid);
            //    g.Dispose();
            //}
        }

        // 텍스트박스의 크기를 계산해서 워터마크를 그려줌
        private void DrawWaterMarkText()
        {
            if (string.IsNullOrEmpty(this.Text) &&
                false == string.IsNullOrEmpty(this._WaterMarkText) &&
                this.IsHandleCreated &&
                false == this.Focused &&
                this.Visible)
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    // 텍스트의 vertical 정렬을 하기 위한 계산들
                    StringFormat sf = new StringFormat();
                    float textHeight = g.MeasureString(this._WaterMarkText, this.Font, this.Width, sf).Height;
                    float textY = ((float)this.Height - textHeight) / (float)2.0;
                    RectangleF bounds = new RectangleF(
                        0, textY, (float)this.Width, (float)this.Height - (textY * (float)2.0));

                    g.DrawString(this._WaterMarkText, this.Font, new SolidBrush(this._WaterMarkColor), bounds, sf);
                }
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            saveBackColor = BackColor;
            BackColor = focusedBackColor;
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            BackColor = saveBackColor;
            base.OnLeave(e);
        }

    }
}
