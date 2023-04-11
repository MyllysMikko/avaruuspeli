using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    class Game
    {
        int window_width = 960;
        int window_height = 720;
        Player player;
        Background bg;
        List<Bullet> bullets;

        float shootDelay = 0.5f;
        double nextShoot = 0;

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

            Raylib.EndDrawing();
        }

        void Update()
        {

            if (player.Update() && Raylib.GetTime() > nextShoot)
            {
                //Vector2 spawnPos = player.transform.position + new Vector2(player.collision.size.X / 2, 5);
                Bullet bullet = new Bullet(player.transform.position, new Vector2(0, -1), 300, 20);
                //bullet.transform.position.X += bullet.collision.size.X / 2;
                bullet.transform.position.X = (player.transform.position.X + player.collision.size.X / 2) - bullet.collision.size.X / 2;
                bullet.transform.position.Y -= bullet.collision.size.Y;
                bullets.Add(bullet);
                nextShoot = Raylib.GetTime() + shootDelay;
            }


            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].transform.position.Y < 0 - bullets[i].collision.size.Y)
                {
                    bullets.RemoveAt(i);
                }
            }
            bg.Update();
        }

        void KeepInBounds(TransformComponent transform, CollisionComponent collision, int left, int top, int right, int bottom)
        {
            transform.position.X = Math.Clamp(transform.position.X, left, right - collision.size.X);
            transform.position.Y = Math.Clamp(transform.position.Y, top, bottom - collision.size.Y);
        }
    }
}
