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

        List<Enemy> enemies;



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

            enemies = new List<Enemy>();

            enemies.Add(new Enemy(new Vector2(window_width / 2, 80), new Vector2(1, 0), 200, 40));

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

            foreach (Enemy enemy in enemies)
            { 
                enemy.Draw();
            }

            Raylib.EndDrawing();
        }

        void Update()
        {

            if (player.Update())
            {
                ShootBullet(player.transform.position, new Vector2(0, -1), 300, 20);
            }


            foreach (Bullet bullet in bullets)
            {
                bullet.Update();

                if (KeepInBounds(bullet.transform, bullet.collision, 0, -bullet.collision.size.Y, window_width, window_height))
                {
                    bullet.active = false;
                }



                Rectangle bulletRec = GetRectangle(bullet.transform, bullet.collision);

                foreach (Enemy enemy in enemies)
                {
                    Rectangle enemyRec = GetRectangle(enemy.transform, enemy.collision);
                    if (Raylib.CheckCollisionRecs(bulletRec, enemyRec))
                    {
                        if (enemy.active)
                        {
                            Console.WriteLine("Enemy hit");
                            enemy.active = false;
                        }
                    }
                }


            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Update();
                if (KeepInBounds(enemy.transform, enemy.collision, 0, 0, window_width, window_height))
                {
                    enemy.transform.direction.X *= -1;
                }
            }

            bg.Update();
        }

        void ShootBullet(Vector2 pos, Vector2 dir, float speed, int size)
        {

            foreach (Bullet bullet in bullets)
            {
                if (bullet.active == false)
                {
                    bullet.Reset(pos, dir, speed, size);
                    ResetBulletPos(bullet, player);
                    return;
                }
            }

            Bullet newBullet = new Bullet(player.transform.position, new Vector2(0, -1), 300, 20);
            ResetBulletPos(newBullet, player);
            bullets.Add(newBullet);
            Console.WriteLine($"Bullet count: {bullets.Count}");


        }

        bool KeepInBounds(TransformComponent transform, CollisionComponent collision, float left, float top, float right, float bottom)
        {
            float newX = Math.Clamp(transform.position.X, left, right - collision.size.X);
            float newY = Math.Clamp(transform.position.Y, top, bottom - collision.size.Y);

            bool xChange = newX != transform.position.X;
            bool yChange = newY != transform.position.Y;

            transform.position.X = newX;
            transform.position.Y = newY;

            return xChange || yChange;


        }

        void ResetBulletPos(Bullet bullet, Player player)
        {
            bullet.transform.position.X = (player.transform.position.X + player.collision.size.X / 2) - bullet.collision.size.X / 2;
            bullet.transform.position.Y -= bullet.collision.size.Y;
        }

        Rectangle GetRectangle(TransformComponent transform, CollisionComponent collision)
        {
            Rectangle rec = new Rectangle(transform.position.X, transform.position.Y, collision.size.X, collision.size.Y);

            return rec;
        }
    }
}
