using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class MenuCreator
    {
        private float x;
        private float y;
        private float width;
        private float height;
        private float between;

        /// <summary>
        /// Asettaa mistä kohtaan valikon ominaisuuksia aletaan piirtämään.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="between"></param>
        public void StartMenu(float x, float y, float width, float height, float between)
        {
            this.width = width;
            this.height = height;
            this.between = between;
            this.x = x - width * 0.5f;
            this.y = y - height * 0.5f;

        }

        public bool Button(string text)
        {


            bool input = RayGui.GuiButton(new Rectangle(x , y, width, height), text);
            y += height + between;

            return input;
        }

        public void Label(string text)
        {
            RayGui.GuiLabel(new Rectangle(x, y, width, height), text);
            y += height + between;

        }

        public float SliderBar(string label, string min, string max, float value,  float minVal, float maxVal)
        {
            RayGui.GuiLabel(new Rectangle(x, y, width, height), label);
            x += 10;
            y += height;
            value = RayGui.GuiSliderBar(new Rectangle(x, y, width * 2, height), min, max, value, minVal, maxVal);
            y += height + between;

            x -= 10;

            return value;
        }

        public float Slider(string label, string min, string max, float value, float minVal, float maxVal)
        {
            RayGui.GuiLabel(new Rectangle(x, y, width, height), label);
            x += 10;
            y += height;
            value = RayGui.GuiSlider(new Rectangle(x, y, width * 2, height), min, max, value, minVal, maxVal);
            y += height + between;

            x -= 10;

            return value;
        }

        public unsafe bool Spinner(string text, int* spinnerValue, int min, int max, bool spinnerActive)
        {
            bool edit = RayGui.GuiSpinner(new Rectangle(x, y, width, height), text, spinnerValue, min, max, spinnerActive);
            y += height + between;

            return edit;

        }


    }
}
