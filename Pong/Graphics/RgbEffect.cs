using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics
{
    public class RgbEffect
    {
        private List<Color> rgb = new List<Color>() { Color.Red, Color.Chartreuse, Color.LightSkyBlue };
        private Color rgbColor;
        private float currentValue = 0;
        private float timeBetweenSwitch;
        private int index = 0;

        public RgbEffect(float switchRate=1) 
        {
            rgbColor = rgb[index];
            timeBetweenSwitch = switchRate;

            // later on, allow for setting the colors
        }

        public void Update(GameTime gameTime)
        {
            //var colorToUse = (Globals.scoreMultiplier >= 6) ? rgbTextColor : Color.White;

            currentValue += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentValue >= timeBetweenSwitch)
            {
                currentValue = 0;
                index++;
            }

            rgbColor = Color.Lerp(rgb[(index) % (rgb.Count)], rgb[(index + 1) % (rgb.Count)], currentValue / timeBetweenSwitch);
        }

        public Color GetCurrentColor()
        {
            return rgbColor;
        }
    }
}
