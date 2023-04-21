using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    class Game
    {
        GameState state;
        int window_width = 960;
        int window_height = 720;

        int playerSize = 40;

        int rows = 2;
        int columns = 4;
        int maxScore = 40;
        int minScore = 10;

        Background bg;

        Player player;
        List<Bullet> bullets;

        List<Enemy> enemies;

        int scoreCounter = 0;



        public void Run()
        {
            Init();
            GameLoop();
        }

        void Init()
        {
            state = GameState.Play;
            Raylib.InitWindow(window_width, window_height, "Avaruuspeli");

            Texture playerImage = Raylib.LoadTexture("data/images/playerShip1_orange.png");
            Vector2 playerStart = new Vector2(window_width / 2, window_height - 80);
            player = new Player(playerStart, 300, playerSize, playerImage);

            bg = new Background(window_width, window_height, 0.1f, 5);

            bullets = new List<Bullet>();

            enemies = new List<Enemy>();



            //TODO formaatio
            

            SpawnEnemies();


        }


        void SpawnEnemies()
        {
            int startX = 0;
            int startY = 0;
            int currentX = startX;
            int currentY = startY;

            

            for (int row = 0; row < rows; row++)
            {
                currentX = startX;
                currentY += playerSize;
                int enemyScore = maxScore - row * 10;
                if (enemyScore < minScore)
                {
                    enemyScore = minScore;
                }
                for (int col = 0; col < columns; col++)
                {
                    currentX += playerSize;

                    Vector2 enemyStart = new Vector2(currentX, currentY);

                    bool löytyi = false;

                    foreach (Enemy enemy in enemies )
                    {
                        if (!enemy.active)
                        {
                            enemy.transform.position = enemyStart;
                            enemy.active = true;
                            enemy.scoreValue = enemyScore;
                            enemy.transform.direction = new Vector2(1, 0);
                            löytyi = true;
                            break;
                        }
                    }

                    if (!löytyi)
                    {
                        Texture enemyImage = Raylib.LoadTexture("data/images/enemyBlack1.png");

                        enemies.Add(new Enemy(enemyStart, new Vector2(1, 0), 200, playerSize, enemyScore, enemyImage));
                    }

                    currentX += playerSize;
                }
                currentY += playerSize;

            }
            Console.WriteLine(enemies.Count);
        }



        private void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                
                switch(state)
                {
                    case GameState.Play:
                        Draw();
                        Update();
                        break;

                    case GameState.ScoreScreen:
                        ScoreUpdate();
                        ScoreDraw();
                        break;
                }
                
                
            }
        }

        private void ScoreUpdate()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                scoreCounter = 0;
                SpawnEnemies();

                //reset bullets
                foreach (Bullet bullet in bullets)
                {
                    if (bullet.active)
                    {
                        bullet.active = false;
                    }
                }
                Vector2 pos = new Vector2(window_width / 2, window_height - 80);

                player.transform.position = pos;

                state = GameState.Play;
            }
        }

        private void ScoreDraw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            Raylib.DrawText(scoreCounter.ToString(), window_width / 2, window_height / 2, 50, Raylib.SKYBLUE);
            Raylib.EndDrawing();
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

            Raylib.DrawText(scoreCounter.ToString(),10, 10, 16, Raylib.SKYBLUE);

            Raylib.EndDrawing();
        }

        int CountActiveEnemies()
        {
            int active = 0;

            foreach (Enemy enemy in enemies)
            {
                if (enemy.active)
                {
                    active++;
                }
            }

            return active;
        }

        void Update()
        {

           

            UpdatePlayer();
            UpdateEnemies();
            UpdateBullets();
            CheckCollisions();


  

            

            bg.Update();
        }

        void UpdatePlayer()
        {
            if (player.Update())
            {
                ShootBullet(player.transform.position, new Vector2(0, -1), 500, 20);
            }
            KeepInBounds(player.transform, player.collision, 0, 0, window_width, window_height);
        }

        void UpdateEnemies()
        {
            bool changeDir = false;
            foreach (Enemy enemy in enemies)
            {
                enemy.Update();
                if (KeepInBounds(enemy.transform, enemy.collision, 0, 0, window_width, window_height))
                {
                    changeDir = true;
                }
            }

            if (changeDir)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.transform.direction.X *= -1;
                }
            }
        }
        void UpdateBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                if (bullet.active == false)
                {
                    continue;
                }
                bullet.Update();
                if (KeepInBounds(bullet.transform, bullet.collision, 0, -bullet.collision.size.Y, window_width, window_height))
                {
                    bullet.active = false;
                }
            }
        }

        void CheckCollisions()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.active) 
                { 
                    Rectangle enemyRec = GetRectangle(enemy.transform, enemy.collision);

                    foreach(Bullet bullet in bullets)
                    {
                        if (bullet.active)
                        {
                            Rectangle bulletRec = GetRectangle(bullet.transform, bullet.collision);

                            if (Raylib.CheckCollisionRecs(bulletRec, enemyRec))
                            {
                                enemy.active = false;
                                bullet.active = false;
                                scoreCounter += enemy.scoreValue;
                                Console.WriteLine(scoreCounter);

                                if (CountActiveEnemies() == 0)
                                {
                                    state = GameState.ScoreScreen;
                                }
                            }
                        }
                    }
                }
            }
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

            Bullet newBullet = new Bullet(player.transform.position, new Vector2(0, -1), speed, 20);
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

        enum GameState
        {
            Play,
            ScoreScreen
        }
    }
}
