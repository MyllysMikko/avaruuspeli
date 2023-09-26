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
        SaveManager saveManager = new SaveManager();
        public EventHandler StartPressed;
        public EventHandler OptionsPressed;

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
            if (mc.Button("Start Game"))
            {
                StartPressed.Invoke(this, EventArgs.Empty);
            }
            if (mc.Button("Options"))
            {
                OptionsPressed.Invoke(this, EventArgs.Empty);
            }

#if DEBUG
            if (mc.Button("Load"))
            {
                saveManager.LoadFromFile();
            }
#endif
            if (mc.Button("Quit Game"))
            {
                Raylib.CloseWindow();
                return;
            }


            Font defaultFont = Raylib.GetFontDefault();

            string title = "Space Invaders";
            int titleSize = 100;

            Vector2 titleTextSize = Raylib.MeasureTextEx(defaultFont, title, titleSize, 10);

            Vector2 titlePos = new Vector2(x - (titleTextSize.X / 2), Raylib.GetScreenHeight() / 4 - (titleTextSize.Y / 2));

            Raylib.DrawTextEx(defaultFont, title, titlePos, titleSize, 10, Raylib.GREEN);

            Raylib.EndDrawing();
        }
    }
}
