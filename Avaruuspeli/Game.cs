using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    class Game
    {
        int window_width = 960;
        int window_height = 720;

        Background bg;

        Player player;
        List<Bullet> bullets;

        Enemy enemy;



        public void Run()
        {
            Init();
            GameLoop();
        }

        void Init()
        {
            Raylib.InitWindow(window_width, window_height, "Avaruuspeli");

            Vector2 playerStart = new Vector2(window_width / 2, window_height - 80);
            player = new Player(playerStart, 300, 40);

            bg = new Background(window_width, window_height, 0.1f, 5);

            bullets = new List<Bullet>();

            enemy = new Enemy(new Vector2(window_width / 2, 80), new Vector2(1,0), 200, 40);
        }

        private void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                Draw();
                Update();
                KeepInBounds(player.transform, player.collision, 0, 0, window_width, window_height);
                
            }
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            bg.Draw();

            Raylib.ClearBackground(Raylib.BLACK);
            player.Draw();

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }

            enemy.Draw();

            Raylib.EndDrawing();
        }

        void Update()
        {

            if (player.Update())
            {
                //Vector2 spawnPos = player.transform.position + new Vector2(player.collision.size.X / 2, 5);
                Bullet bullet = new Bullet(player.transform.position, new Vector2(0, -1), 300, 20);
                //bullet.transform.position.X += bullet.collision.size.X / 2;
                bullet.transform.position.X = (player.transform.position.X + player.collision.size.X / 2) - bullet.collision.size.X / 2;
                bullet.transform.position.Y -= bullet.collision.size.Y;
                bullets.Add(bullet);
            }


            //foreach (Bullet bullet in bullets)
            //{
            //    
            //}

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update();
                if (bullets[i].transform.position.Y < 0 - bullets[i].collision.size.Y)
                {
                    bullets.RemoveAt(i);
                }
            }

            enemy.Update();
            if (KeepInBounds(enemy.transform, enemy.collision, 0, 0, window_width, window_height))
            {
                enemy.transform.direction.X *= -1;
            }

            bg.Update();
        }

        bool KeepInBounds(TransformComponent transform, CollisionComponent collision, int left, int top, int right, int bottom)
        {
            float newX = Math.Clamp(transform.position.X, left, right - collision.size.X);
            float newY = Math.Clamp(transform.position.Y, top, bottom - collision.size.Y);

            bool xChange = newX != transform.position.X;
            bool yChange = newY != transform.position.Y;

            transform.position.X = newX;
            transform.position.Y = newY;

            return xChange || yChange;


        }
    }
}
