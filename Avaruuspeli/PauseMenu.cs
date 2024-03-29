﻿using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class PauseMenu
    {
        MenuCreator mc = new MenuCreator();

        public EventHandler BackPressed;
        public EventHandler OptionsPressed;
        public EventHandler RestartPressed;
        public EventHandler MainMenuPressed;

        public float timer;
        public float enemiesKilled;

        public void Draw(float timer, float enemiesKilled)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);

            float x = Raylib.GetScreenWidth() * 0.5f;
            float y = Raylib.GetScreenHeight() * 0.5f;
            float menuWidth = 120;
            float menuHeight = 40;
            float between = 10;



            mc.StartMenu(x * 0.5f, y * 0.5f, menuWidth, menuHeight, between);

            mc.Label($"Time: {timer:0.0}");
            mc.Label($"Enemies killed: {enemiesKilled}");

            mc.StartMenu(x, y, menuWidth, menuHeight, between);

            mc.Label("Pause");

            if (mc.Button("Back"))
            {
                BackPressed.Invoke(this, EventArgs.Empty);
            }
            if (mc.Button("Options"))
            {
                OptionsPressed.Invoke(this, EventArgs.Empty);
            }
            if (mc.Button("Restart Game"))
            {
                RestartPressed.Invoke(this, EventArgs.Empty);
            }
            if (mc.Button("Main Menu"))
            {
                MainMenuPressed.Invoke(this, EventArgs.Empty);
            }

            Raylib.EndDrawing();
        }
    }
}
