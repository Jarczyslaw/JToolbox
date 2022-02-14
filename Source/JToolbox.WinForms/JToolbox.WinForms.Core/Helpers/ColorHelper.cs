using JToolbox.Core.Helpers;
using System;
using System.Drawing;

namespace JToolbox.WinForms.Core.Helpers
{
    public static class ColorHelper
    {
        public static Color GetGradient(Color startColor, Color endColor, float i)
        {
            i = (float)MathHelper.Clamp(i, 0f, 1f);

            var r = (int)Math.Round(MathHelper.Lerp(startColor.R, endColor.R, i));
            var g = (int)Math.Round(MathHelper.Lerp(startColor.G, endColor.G, i));
            var b = (int)Math.Round(MathHelper.Lerp(startColor.B, endColor.B, i));

            return Color.FromArgb(r, g, b);
        }
    }
}