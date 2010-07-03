using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ColoMath
{
    public static class ColorMath
    {
        /// <summary>
        /// Alters the brightness of the color.
        /// </summary>
        public static Color ChangeBrightness(this Color color, int amount)
        {
            float brightness = ((float)(amount * 0.01)) + color.GetBrightness();
            brightness = SetToMaxOrMin(brightness);
            return ColorFromAhsb(color.A, color.GetHue(), color.GetSaturation(), brightness);
        }

        /// <summary>
        /// Alters the hue of the color.
        /// </summary>
        public static Color ChangeHue(this Color color, int degrees)
        {
            float hue = degrees + color.GetHue();
            if (hue > 360f)
            {
                hue -= 360f;
            }
            if (hue < 0f)
            {
                hue += 360f;
            }
            return ColorFromAhsb(color.A, hue, color.GetSaturation(), color.GetBrightness());
        }

        public static Color ChangeSaturation(this Color color, int amount)
        {
            float saturation = (amount * 0.01f) + color.GetSaturation();
            saturation = SetToMaxOrMin(saturation);
            return ColorFromAhsb(color.A, color.GetHue(), saturation, color.GetBrightness());
        }

        /// <summary>
        /// Retrieves the complement of the inputted color.
        /// (The complement is the exact opposite of a color)
        /// </summary>
        public static Color GetComplement(this Color color)
        {
            return color.ChangeHue(180);
        }

        /// <summary>
        /// Retrieves the left analogous color.
        /// </summary>
        public static Color GetLeftAnalogous(this Color color)
        {
            return color.ChangeHue(30);
        }

        public static Color GetLeftSplitComplement(this Color color)
        {
            return color.ChangeHue(150);
        }

        public static Color GetLeftTriad(this Color color)
        {
            return color.ChangeHue(120);
        }

        /// <summary>
        /// Retrieves the right analogous color.
        /// </summary>
        public static Color GetRightAnalogous(this Color color)
        {
            return color.ChangeHue(30);
        }

        public static Color GetRightSplitComplement(this Color color)
        {
            return color.ChangeHue(-150);
        }

        /// <summary>
        /// Returns the right triad of the color.
        /// </summary>
        public static Color GetRightTriad(this Color color)
        {
            return color.ChangeHue(-120);
        }

        /// <summary>
        /// Returns the hexadecimal value of a Color instance. 
        /// </summary>
        public static string ToHexString(this Color c)
        {
            string hexString = "00000000";
            hexString = hexString + ColorTranslator.ToWin32(c).ToString("X");
            return hexString.Substring(hexString.Length - 8, 8);
        }

        /// <summary>
        /// Constrains the input alpha value to be between 1 and 0.
        /// </summary>
        private static float SetToMaxOrMin(float alpha)
        {
            return alpha > 1f ? 1f :
                   alpha < 0f ? 0f : alpha;
        }

        /// <summary>
        /// Returns a Color instance from the given alpha, hue, saturation, and brightness values.
        /// The formula that this was dervied from was on a site that is no longer active. 
        /// I am regretfully unable to provide a link. . . 
        /// </summary>
        public static Color ColorFromAhsb(int alpha, float hue, float saturation, float brightness)
        {
            float greenValue;
            float redValue;
            float blueValue;
            
            // Check argument ranges . . .
            if ((0 > alpha) || (0xff < alpha))
                throw new ArgumentOutOfRangeException("alpha", alpha, "Invalid Alpha");
            if ((0f > hue) || (360f < hue))
                throw new ArgumentOutOfRangeException("hue", hue, "Invalid Hue");
            if ((0f > saturation) || (1f < saturation))
                throw new ArgumentOutOfRangeException("saturation", saturation, "Invalid Saturation");
            if ((0f > brightness) || (1f < brightness))
                throw new ArgumentOutOfRangeException("brightness", brightness, "Invalid Brightness");

            // Color is lacking saturation, return shade of gray based on brightness. . .
            if (0f == saturation)
            {
                var grayValue = Convert.ToInt32(brightness*255f);
                return Color.FromArgb(alpha, grayValue, grayValue, grayValue);
            }

            if (0.5 < brightness)
            {
                greenValue = (brightness - (brightness * saturation)) + saturation;
                blueValue = (brightness + (brightness * saturation)) - saturation;
            }
            else
            {
                greenValue = brightness + (brightness * saturation);
                blueValue = brightness - (brightness * saturation);
            }

            int hueValue = (int)Math.Floor(hue / 60f);
            if (300f <= hue)
                hue -= 360f;
            
            hue /= 60f;
            hue -= 2f * ((float)Math.Floor((((hueValue + 1f) % 6f) / 2f)));
            
            if (0 == (hueValue % 2))
                redValue = (hue * (greenValue - blueValue)) + blueValue;
            else
                redValue = blueValue - (hue * (greenValue - blueValue));
            
            int green = Convert.ToInt32(greenValue * 255f);
            int red = Convert.ToInt32(redValue * 255f);
            int blue = Convert.ToInt32(blueValue * 255f);
            switch (hueValue)
            {
                case 1:
                    return Color.FromArgb(alpha, red, green, blue);

                case 2:
                    return Color.FromArgb(alpha, blue, green, red);

                case 3:
                    return Color.FromArgb(alpha, blue, red, green);

                case 4:
                    return Color.FromArgb(alpha, red, blue, green);

                case 5:
                    return Color.FromArgb(alpha, green, blue, red);
            }
            return Color.FromArgb(alpha, green, red, blue);

        }
    }
}
