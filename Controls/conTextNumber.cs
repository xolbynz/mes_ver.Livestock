using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace 스마트팩토리.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public partial class conTextNumber : TextBox
    {
        private void conTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.SendWait("{tab}");
            }
        }

        private void conTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }

            bool bPoint = false;
            if (formatString.IndexOf('.') > -1)
            {
                bPoint = true;
            }

            TypingOnlyNumber(sender, e, bPoint, true);
        }

        private static void TypingOnlyNumber(object sender, KeyPressEventArgs e, bool includePoint, bool includeMinus)
        {
            bool isValidInput = false;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                if (includePoint == true) { if (e.KeyChar == '.') isValidInput = true; }
                if (includeMinus == true) { if (e.KeyChar == '-') isValidInput = true; }

                if (isValidInput == false) e.Handled = true;
            }

            if (includePoint == true)
            {
                if (e.KeyChar == '.' && (string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('.') > -1)) e.Handled = true;
            }
            if (includeMinus == true)
            {
                if (e.KeyChar == '-')
                {
                    if ((sender as TextBox).Text.IndexOf('-') < 1)
                    {
                        if ((sender as TextBox).SelectionLength == 0)
                        {
                            if (string.IsNullOrEmpty((sender as TextBox).Text.Trim()) == false)
                            {
                                e.Handled = true;
                                if ((sender as TextBox).Text.Trim() == "-")
                                {
                                    (sender as TextBox).Text = "";
                                }
                                else
                                {
                                    (sender as TextBox).Text = (double.Parse((sender as TextBox).Text.Trim()) * -1).ToString();
                                }
                            }
                        }
                    }
                }
                //if (e.KeyChar == '-' && (!string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('-') > -1)) e.Handled = true;
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

        private string formatString = "#,0";
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("숫자 형식을 입력하세요.")]
        public string _FormatString
        {
            get { return formatString; }
            set { formatString = value; }
        }

        private string valueType = "수량";
        [Browsable(true)]
        [Category(conDefaults.CatAppearance)]
        [Description("값의 특성(수량, 단가)을 입력하세요.")]
        public string _ValueType
        {
            get { return valueType; }
            set { valueType = value; }
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

        public conTextNumber()
        {
            saveBackColor = BackColor;
            this.AutoSize = false;
            //this.TextAlign = HorizontalAlignment.Right;
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
            Text = Text.Replace(",", "");

            Select(0, Text.Length);
            //// 내용 전부 선택되는 거 방지.
            //Select(Text.Length, 0);

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            BackColor = saveBackColor;
            if (Text.Trim() == "" || Text.Trim() == "-")
            {
                Text = "0";
            }

            int nPointCnt = 0;
            if (formatString.IndexOf('.') > -1)
            {
                nPointCnt = formatString.Length - formatString.IndexOf('.') - 1;
            }
            if (Text.IndexOf('.') > -1)
            {
                string sTmp = Text;
                if (sTmp.Length - sTmp.IndexOf('.') - 1 > nPointCnt)
                {
                    Text = sTmp.Substring(0, sTmp.IndexOf('.') + 1);
                    Text += sTmp.Substring(sTmp.IndexOf('.') + 1, nPointCnt);
                }
            }
            Text = double.Parse(Text).ToString(formatString);
            base.OnLeave(e);
        }

    }
}
