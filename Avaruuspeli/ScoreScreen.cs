using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;

namespace Avaruuspeli
{
    internal class ScoreScreen
    {
        MenuCreator mc = new MenuCreator();

        public EventHandler BackPressed;

        public int finalScore = 0;

        public void Draw()
        {
            int window_height = Raylib.GetScreenHeight();
            int window_width = Raylib.GetScreenWidth();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            Font defaultFont = Raylib.GetFontDefault();
            string gameOver = "GAME OVER";
            int gameOverSize = 100;
            Vector2 gameOverTextSize = Raylib.MeasureTextEx(defaultFont, gameOver, gameOverSize, 10);
            Vector2 gameOverPos = new Vector2((window_width / 2) - (gameOverTextSize.X / 2), window_height / 2 - (gameOverTextSize.Y / 2));

            string score = $"Final score: {finalScore}";
            int scoreSize = 50;
            Vector2 scoreTextSize = Raylib.MeasureTextEx(defaultFont, score, scoreSize, 10);
            Vector2 scorePos = new Vector2((window_width / 2) - (scoreTextSize.X / 2), gameOverPos.Y + gameOverTextSize.Y);

            string retry = "ENTER to try again";
            int retrysize = 20;
            Vector2 retryTextSize = Raylib.MeasureTextEx(defaultFont, retry, retrysize, 10);
            Vector2 retryPos = new Vector2((window_width / 2) - (retryTextSize.X / 2), scorePos.Y + scoreTextSize.Y * 1.5f);

            Raylib.DrawTextEx(defaultFont, gameOver, gameOverPos, gameOverSize, 10, Raylib.GREEN);

            Raylib.DrawTextEx(defaultFont, score, scorePos, scoreSize, 10, Raylib.BLUE);

            Raylib.DrawTextEx(defaultFont, retry, retryPos, retrysize, 10, Raylib.GREEN);
            Raylib.EndDrawing();

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                BackPressed.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
