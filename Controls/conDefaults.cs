using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace 스마트팩토리.Controls
{
    internal static class conDefaults
    {
        // NOTE: Do NOT set to NowStyleManager.AMBIENT_VALUE !
        public const string Theme = "Light";

        // NOTE: Do NOT set to NowStyleManager.AMBIENT_VALUE !
        public const string Style = "Blue";

        public const conBorderStyle BorderStyle = conBorderStyle.None;

        // This will massively reduce flicker by disabling redrawing during resize
        public static bool FormSuspendLayoutDuringResize = false;

        //public const NowForm.NowFormShadowType FormShadowType = NowForm.NowFormShadowType.SystemAeroShadow;

        public static bool DrawFocusRectangle = false;

        // Font fallbacks
        //public const conFontSize conFontSize = conFontSize.Medium;
        //public const conFontWeight conFontWeight = conFontWeight.Regular;
        public static readonly string FontFamily = SystemFonts.DefaultFont.Name;
        public static readonly FontStyle FontStyle = SystemFonts.DefaultFont.Style;
        public const float FontSize = 14f;

        // Categories

        public const string CatAppearance = "con 모양 설정";
        public const string CatBehavior = "con Behavior";
        public const string CatDataField = "con 데이터 설정";

    }
}
