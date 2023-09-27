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

        public EventHandler MainMenuPressed;

        public int finalScore = 0;
        public int highScore = 0;

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

            string highScoreText = $"Highscore: {highScore}";
            int highScoreSize = 50;
            Vector2 highScoreTextSize = Raylib.MeasureTextEx(defaultFont, highScoreText, highScoreSize, 10);
            Vector2 highScorePos = new Vector2((window_width * 0.5f) - (highScoreTextSize.X * 0.5f), scorePos.Y + scoreTextSize.Y);



            Raylib.DrawTextEx(defaultFont, gameOver, gameOverPos, gameOverSize, 10, Raylib.GREEN);

            Raylib.DrawTextEx(defaultFont, score, scorePos, scoreSize, 10, Raylib.BLUE);

            Raylib.DrawTextEx(defaultFont, highScoreText, highScorePos, highScoreSize, 10, Raylib.RED);


            float x = Raylib.GetScreenWidth() * 0.5f;
            float y = Raylib.GetScreenHeight() * 0.5f;
            float menuWidth = 120;
            float menuHeight = 40;
            float between = 10;

            mc.StartMenu(x, scorePos.Y + scoreTextSize.Y * 3f, menuWidth, menuHeight, between);

            if (mc.Button("Retry"))
            {
                BackPressed.Invoke(this, EventArgs.Empty);
            }

            if (mc.Button("Main Menu"))
            {
                MainMenuPressed.Invoke(this, EventArgs.Empty);
            }

            Raylib.EndDrawing();

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                BackPressed.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
