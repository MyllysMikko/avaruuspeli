using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class MainMenu
    {
        MenuCreator mc = new MenuCreator();
        public EventHandler StartPressed;
        public EventHandler QuitPressed;

        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            float x = Raylib.GetScreenWidth() * 0.5f;
            float y = Raylib.GetScreenHeight() * 0.5f;
            float menuWidth = 120;
            float menuHeight = 40;
            float between = 10;



            mc.StartMenu(x, y, menuWidth, menuHeight, between);
            if (mc.Button("Testi"))
            {
                StartPressed.Invoke(this, EventArgs.Empty);
            }
            if (mc.Button("Testi2"))
            {
                QuitPressed.Invoke(this, EventArgs.Empty);
            }


            Font defaultFont = Raylib.GetFontDefault();
            //
            string title = "Space Invaders";
            //
            int titleSize = 100;
            //
            ////int textWidth = Raylib.MeasureText(title, titleSize);
            //
            Vector2 titleTextSize = Raylib.MeasureTextEx(defaultFont, title, titleSize, 10);
            //
            //string under = "Press ENTER";
            //
            //int underSize = 50;
            //
            ////int underTextWidth = Raylib.MeasureText(under, underSize);
            //
            //Vector2 underTextSize = Raylib.MeasureTextEx(defaultFont, under, underSize, 10);
            //
            Vector2 titlePos = new Vector2(x - (titleTextSize.X / 2), Raylib.GetScreenHeight() / 4 - (titleTextSize.Y / 2));
            //
            //Vector2 underTextPos = new Vector2((window_width / 2) - (underTextSize.X / 2), titlePos.Y + titleTextSize.Y);
            //
            ////Raylib.DrawText(text, window_width / 2 - (textSize / 2), window_height / 2, 100, Raylib.GREEN);
            Raylib.DrawTextEx(defaultFont, title, titlePos, titleSize, 10, Raylib.GREEN);
            //Raylib.DrawTextEx(defaultFont, under, underTextPos, underSize, 10, Raylib.GREEN);

            Raylib.EndDrawing();
        }
    }
}
