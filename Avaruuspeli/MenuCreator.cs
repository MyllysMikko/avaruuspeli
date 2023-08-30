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

        public void StartMenu(float x, float y, float width, float height, float between)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.between = between;
        }

        public bool Button(string text)
        {


            bool input = RayGui.GuiButton(new Rectangle(x - width * 0.5f, y - height * 0.5f, width, height), text);
            y += height + between;

            return input;
        }

    }
}
