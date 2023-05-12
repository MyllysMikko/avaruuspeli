using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    /// <summary>
    /// Pääpelin luokka
    /// </summary>
    class Game
    {
        GameState state;
        int window_width = 960;
        int window_height = 720;

        int playerSize = 40;

        int rows = 3;
        int columns = 9;
        int maxScore = 40;
        int minScore = 10;
        int enemySpeed = 150;

        float enemyShootDelay = 1;
        double nextEnemyShoot = 1;

        Background bg;

        Player player;
        List<Bullet> bullets;
        List<Bullet> enemyBullets;

        List<Enemy> enemies;

        List<MovingText> movingTexts;

        Texture[] enemyImage = new Texture[2];
        Texture[] bulletImage = new Texture[2];
        Sound[] shootSounds = new Sound[2];
        Sound[] explosions = new Sound[2];

        int scoreCounter = 0;

        float timer;

        int combo = 0;




        public void Run()
        {
            Init();
            GameLoop();
        }

        /// <summary>
        /// Alustaa pelin, lataa tekstuurit ja äänet, luo listat luodeille ja vihollisille.
        /// Kutsuu "SpawnEnemies" metodin
        /// </summary>
        void Init()
        {
            state = GameState.Start;
            Raylib.InitWindow(window_width, window_height, "Avaruuspeli");

            Texture playerImage = Raylib.LoadTexture("data/images/playerShip1_orange.png");
            Vector2 playerStart = new Vector2(window_width / 2, window_height - 80);
            player = new Player(playerStart, 300, playerSize, playerImage);

            bg = new Background(window_width, window_height, 0.1f, 5);

            bullets = new List<Bullet>();

            enemies = new List<Enemy>();

            movingTexts = new List<MovingText>();

            enemyImage[0] = Raylib.LoadTexture("data/images/enemyBlack1.png");
            enemyImage[1] = Raylib.LoadTexture("data/images/enemyBlack2.png");

            bulletImage[0] = Raylib.LoadTexture("data/images/laserBlue16.png");
            bulletImage[1] = Raylib.LoadTexture("data/images/laserRed16.png");



            Raylib.InitAudioDevice();
            shootSounds[0] = Raylib.LoadSound("data/sound/Pshoot.wav");
            shootSounds[1] = Raylib.LoadSound("data/sound/Eshoot.wav");
            explosions[0] = Raylib.LoadSound("data/sound/explosion.wav");
            explosions[1] = Raylib.LoadSound("data/sound/playerExplodes.wav");

            timer = 0;

            SpawnEnemies();


        }

        /// <summary>
        /// Spawnaa viholliset
        /// </summary>
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
                            enemy.transform.speed = enemySpeed;
                            löytyi = true;
                            break;
                        }
                    }

                    if (!löytyi)
                    {


                        enemies.Add(new Enemy(enemyStart, new Vector2(1, 0), enemySpeed, playerSize, enemyScore, enemyImage[(row) % 2]));
                    }

                    currentX += playerSize;
                }
                currentY += playerSize;

            }
        }


        /// <summary>
        /// Pelin pyöriminen.
        /// Suorittaa eri metodit riippuen pelin tilanteesta.
        /// Kun peli suljetaan, unloadataan äänet ja tekstuurit
        /// </summary>
        private void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                
                switch(state)
                {
                    case GameState.Start:
                        StartDraw();
                        StartUpdate();
                        break;

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
            foreach (Sound sound in shootSounds)
            {
                Raylib.UnloadSound(sound);
            }
            foreach (Sound sound in explosions)
            {
                Raylib.UnloadSound(sound);
            }
            foreach(Texture texture in bulletImage)
            {
                Raylib.UnloadTexture(texture);
            }
            foreach (Texture texture in enemyImage)
            {
                Raylib.UnloadTexture(texture);
            }

            Raylib.CloseAudioDevice();
        }

        /// <summary>
        /// Alkuvalikon piirtäminen
        /// </summary>
        private void StartDraw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            bg.Draw();

            Font defaultFont = Raylib.GetFontDefault();

            string title = "Space Invaders";

            int titleSize = 100;

            //int textWidth = Raylib.MeasureText(title, titleSize);

            Vector2 titleTextSize = Raylib.MeasureTextEx(defaultFont, title, titleSize, 10);

            string under = "Press ENTER";

            int underSize = 50;

            //int underTextWidth = Raylib.MeasureText(under, underSize);

            Vector2 underTextSize = Raylib.MeasureTextEx(defaultFont, under, underSize, 10);

            Vector2 titlePos = new Vector2((window_width / 2) - (titleTextSize.X / 2), window_height / 2 - (titleTextSize.Y / 2));

            Vector2 underTextPos = new Vector2((window_width / 2) - (underTextSize.X / 2), titlePos.Y + titleTextSize.Y);

            //Raylib.DrawText(text, window_width / 2 - (textSize / 2), window_height / 2, 100, Raylib.GREEN);
            Raylib.DrawTextEx(defaultFont, title, titlePos, titleSize, 10, Raylib.GREEN);
            Raylib.DrawTextEx(defaultFont, under, underTextPos, underSize, 10, Raylib.GREEN);

            Raylib.EndDrawing();
        }

        private void StartUpdate()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                state = GameState.Play;
            }
            bg.Update();
        }

        /// <summary>
        /// ENTERiä painaessa aloitetaan peli uudestaan.
        /// </summary>
        private void ScoreUpdate()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                scoreCounter = 0;
                timer = 0;
                SpawnEnemies();

                //reset bullets
                foreach (Bullet bullet in bullets)
                {
                    if (bullet.active)
                    {
                        bullet.active = false;
                    }
                }

                foreach (MovingText text in movingTexts)
                {
                    text.active = false;
                }

                Vector2 pos = new Vector2(window_width / 2, window_height - 80);

                player.transform.position = pos;

                nextEnemyShoot = Raylib.GetTime() + enemyShootDelay;

                combo = 0;


                state = GameState.Play;
            }
        }

        /// <summary>
        /// Pisteruudun piirtäminen
        /// </summary>
        private void ScoreDraw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            Font defaultFont = Raylib.GetFontDefault();
            string gameOver = "GAME OVER";
            int gameOverSize = 100;
            Vector2 gameOverTextSize = Raylib.MeasureTextEx(defaultFont, gameOver, gameOverSize, 10);
            Vector2 gameOverPos = new Vector2((window_width / 2) - (gameOverTextSize.X / 2), window_height / 2 - (gameOverTextSize.Y / 2));

            string score = $"Final score: {scoreCounter}";
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
        }

        private void Die()
        {
            state = GameState.ScoreScreen;
            foreach (Enemy enemy in enemies)
            {
                enemy.active = false;
            }

        }

        /// <summary>
        /// Pääpelin piirtäminen
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BLACK);
            bg.Draw();

            player.Draw();

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }

            foreach (Enemy enemy in enemies)
            { 
                enemy.Draw();
            }

            foreach (MovingText text in movingTexts)
            {
                text.Draw();
            }

            Raylib.DrawText(scoreCounter.ToString(),10, 10, 16, Raylib.SKYBLUE);
            DrawTimer();

            Raylib.EndDrawing();
        }

        private void DrawTimer()
        {
            Font defaultFont = Raylib.GetFontDefault();
            string time = timer.ToString("0.#");


            int timeSize = 16;
            Vector2 timeTextSize = Raylib.MeasureTextEx(defaultFont, time, timeSize, 10);
            Vector2 timePos = new Vector2(window_width - timeTextSize.X, 10);
            Raylib.DrawTextEx(defaultFont, time, timePos, timeSize, 10, Raylib.SKYBLUE);
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

        /// <summary>
        /// Pääpelin päivittäminen
        /// </summary>
        void Update()
        {


            UpdatePlayer();
            UpdateEnemies();
            EnemyShoots();
            UpdateBullets();
            CheckCollisions();
            UpdateTexts();

            

            bg.Update();

            timer += Raylib.GetFrameTime();
        }

        void UpdatePlayer()
        {
            if (player.Update())
            {
                ShootBullet(player.transform, player.collision, new Vector2(0, -1), 500, 20, true);
                Raylib.PlaySound(shootSounds[0]);
            }
            KeepInBounds(player.transform, player.collision, 0, 0, window_width, window_height);

        }

        /// <summary>
        /// Vihollisten päivitys.
        /// Tarkistaa myös onko vihollinen päässyt pelaajan luokse, ja tässä tilanteessa lopettaa pelin.
        /// </summary>
        void UpdateEnemies()
        {
            bool changeDir = false;
            foreach (Enemy enemy in enemies)
            {
                if (enemy.transform.position.Y + enemy.collision.size.Y >= player.transform.position.Y)
                {
                    Die();
                }

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
                    Vector2 moveDown = new Vector2(0, 10);

                    enemy.transform.direction.X *= -1;
                    enemy.transform.position += moveDown;

                }
            }
        }

        void UpdateTexts()
        {
            foreach (MovingText text in movingTexts)
            {
                text.Update();
            }
        }

        /// <summary>
        /// Spawnaa vihollisen luodin
        /// </summary>
        void EnemyShoots()
        {
            if (Raylib.GetTime() > nextEnemyShoot)
            {
                Random rand = new Random();

                bool ammuttu = false;
                while (!ammuttu)
                {
                    int enemyIndex = rand.Next(enemies.Count);
                    if (enemies[enemyIndex].active)
                    {
                        ShootBullet(enemies[enemyIndex].transform, enemies[enemyIndex].collision, new Vector2(0, 1), 500, 20, false);
                        ammuttu = true;
                        nextEnemyShoot = Raylib.GetTime() + enemyShootDelay;
                        Raylib.PlaySound(shootSounds[1]);
                    }
                }
            }
        }

        /// <summary>
        /// Kutsutaan luotien Update metodit. Mikäli pelaajan luoti poistuu ruudulta, resetataan combo.
        /// </summary>
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
                    if (bullet.playerBullet)
                    {
                        combo = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Tarkistaa luotien collisionit vihollisiin ja pelaajaan.
        /// Luodin osuessa viholliseen, nostetaan comboa yhdellä
        /// Tällä hetkellä luoteja tarkistetaan vain jos on vihollisia (foreach enemy if enemy.active... jne)
        /// Eli jos vihollisia ei ole niin luotien collisioneita ei tarkisteta.
        /// Tämä voisi aiheuttaa ongelmia mutta tällä hetkellä jos ei ole yhtäkään aktiivista vihollista niin peli loppuu, joten tämän pitäisi olla fine.
        /// </summary>
        void CheckCollisions()
        {

            foreach (Enemy enemy in enemies)
            {
                if (enemy.active) 
                { 
                    Rectangle enemyRec = GetRectangle(enemy.transform, enemy.collision);

                    foreach(Bullet bullet in bullets)
                    {
                        if (bullet.active && bullet.playerBullet)
                        {
                            Rectangle bulletRec = GetRectangle(bullet.transform, bullet.collision);

                            if (Raylib.CheckCollisionRecs(bulletRec, enemyRec))
                            {
                                enemy.active = false;
                                bullet.active = false;
                                combo++;
                                scoreCounter += enemy.scoreValue * combo;
                                if (combo > 1)
                                {
                                    SpawnComboText(combo, 0.5f, enemy.transform.position);
                                }
                                Console.WriteLine(scoreCounter);

                                Raylib.PlaySound(explosions[0]);

                                if (CountActiveEnemies() == 0)
                                {
                                    state = GameState.ScoreScreen;

                                    CalculateTimeScore();
                                }
                            }
                        }
                        else if (bullet.active)
                        {
                            Rectangle bulletRec = GetRectangle(bullet.transform, bullet.collision);

                            Rectangle playerRec = GetRectangle(player.transform, player.collision);

                            if (Raylib.CheckCollisionRecs(bulletRec, playerRec))
                            {
                                Raylib.PlaySound(explosions[1]);
                                Die();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Laskee lisäpisteet ajan perusteella. 10 pistettä jokaisesta sekunnista alle kahden minuutin.
        /// Kutsutaan vain jos pelaaja tuhoaa kaikki viholliset.
        /// </summary>
        void CalculateTimeScore()
        {
            int minuteLimit = 2;
            int secondsLimit = minuteLimit * 60;
            int scorePerSecondUnderLimit = 10;

            int under = (int)(secondsLimit - timer);

            if (under > 1)
            {
                scoreCounter += under * scorePerSecondUnderLimit;
            }
        }

        /// <summary>
        /// Spawnaa luodin. Käytetään pelaajan sekä vihollisten luoteihin.
        /// Jos on ns. "vapaita" luoteja, uudelleenkäytetään nämä. Muuten luodaan uusi luoti.
        /// </summary>
        /// <param name="transform">Ampujan transformi. Käytetään luodin sijainnin määrrittelemisessä</param>
        /// <param name="collision">Ampujan collision. Käytetään luodin sijainnin määrittelemisessä</param>
        /// <param name="dir">Luodin suunta</param>
        /// <param name="speed">Luodin nopeus</param>
        /// <param name="size">Luodin koko</param>
        /// <param name="playerBullet">Onko tämä pelaajan (true) vai vihollisen (false) luoti?</param>
        void ShootBullet(TransformComponent transform, CollisionComponent collision, Vector2 dir, float speed, int size, bool playerBullet)
        {
            Texture bulletText;
            if (playerBullet)
            {
                bulletText = bulletImage[0];
            }
            else
            {
                bulletText = bulletImage[1];
            }

            foreach (Bullet bullet in bullets)
            {
                if (bullet.active == false)
                {
                    bullet.Reset(transform.position, dir, speed, size, playerBullet);
                    ResetBulletPos(bullet, transform, collision);
                    bullet.bulletImage = bulletText;
                    return;
                }
            }



            Bullet newBullet = new Bullet(transform.position, dir, speed, size, bulletText, playerBullet);
            ResetBulletPos(newBullet, transform, collision);
            bullets.Add(newBullet);


        }

        /// <summary>
        /// Luodaan comboteksti
        /// 
        /// </summary>
        /// <param name="comboNumber">Combon määrä</param>
        /// <param name="lifeTime">Kauanko teksti on olemassa</param>
        /// <param name="position">Tekstin alkusijainti</param>
        void SpawnComboText(int comboNumber, float lifeTime, Vector2 position)
        {

            string comboText = comboNumber + "x";
            Color[] colors = { Raylib.LIME, Raylib.GREEN, Raylib.YELLOW, Raylib.RED };
            int colorIndex = comboNumber - 2;
            if (colorIndex >= colors.Count())
            {
                colorIndex= colors.Count() - 1;
            }

            bool found = false;
            foreach (MovingText text in movingTexts)
            {
                if (!text.active)
                {
                    text.transform.position = position;
                    text.transform.direction = new Vector2(0, -1);
                    text.transform.speed = 20;
                    text.text = comboText;
                    text.color = colors[colorIndex];
                    text.aliveUntil = (float)Raylib.GetTime() + lifeTime;
                    text.active = true;

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MovingText combo = new MovingText(comboText, position, new Vector2(0, -1), 20, lifeTime, colors[colorIndex], 20);
                movingTexts.Add(combo);
            }
            Console.WriteLine($"text list {movingTexts.Count()}");
        }

        /// <summary>
        /// Pitää objektit ruudun sisällä. Mikäli objekti yrittää poistua ruudulta, tuodaan se takaisin ja palautetaan true.
        /// Jos objekti pysyi ruudun sisällä, palautetaan false.
        /// </summary>
        /// <param name="transform">Objektin transform. Tästä saadaan objektin sijainti.</param>
        /// <param name="collision">Objectin collision. Tästä saadaan objektin koko.</param>
        /// <param name="left">Ruudun vasen reuna</param>
        /// <param name="top">Ruudun yläreuna</param>
        /// <param name="right">Ruudun oikeareuna</param>
        /// <param name="bottom">Ruudun alareuna</param>
        /// <returns></returns>
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

        /// <summary>
        /// Hienosäätää luodin sijainnin ampujan keskelle.
        /// </summary>
        /// <param name="bullet">Siirrettävä luoti</param>
        /// <param name="transform">Ampujan transformi. Saadaan ampujan sijainti</param>
        /// <param name="collision">Ampujan collision. Saadaan ampujan koko jonka avulla voidaan sijoittaa luoti keskelle</param>
        void ResetBulletPos(Bullet bullet, TransformComponent transform, CollisionComponent collision)
        {
            bullet.transform.position.X = (transform.position.X + collision.size.X / 2) - bullet.collision.size.X / 2;
            bullet.transform.position.Y -= bullet.collision.size.Y;
            bullet.transform.position += bullet.transform.direction * 10;
        }

        /// <summary>
        /// Palauttaa objektin "rectanglen". Tämän avulla testajaan collisionit.
        /// </summary>
        /// <param name="transform">Objektin transform</param>
        /// <param name="collision">Objektin collision</param>
        /// <returns></returns>
        Rectangle GetRectangle(TransformComponent transform, CollisionComponent collision)
        {
            Rectangle rec = new Rectangle(transform.position.X, transform.position.Y, collision.size.X, collision.size.Y);

            return rec;
        }

        /// <summary>
        /// Pelin tilanteet.
        /// </summary>
        enum GameState
        {
            Start,
            Play,
            ScoreScreen
        }
    }
}
