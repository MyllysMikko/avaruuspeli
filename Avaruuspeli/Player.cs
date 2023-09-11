using Raylib_CsLo;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Avaruuspeli
{
    internal class Player
    {
        public TransformComponent transform { get; private set; }
        public CollisionComponent collision { get; private set; }

        float shootDelay = 1f;
        double shootTimer = 0;

        Texture playerImage;


        public Player(Vector2 startPos, int speed, int size, Texture playerImage)
        {
            transform = new TransformComponent(startPos, new Vector2(0, 0), speed);
            collision = new CollisionComponent(new Vector2(size, size));
            this.playerImage = playerImage;
        }

        public void Draw()
        {

            float scaleX = collision.size.X / playerImage.width;
            float scaleY = collision.size.Y / playerImage.height;
            float scale = Math.Min(scaleX, scaleY);

            Raylib.DrawTextureEx(playerImage, transform.position, 0f, scale, Raylib.WHITE);
            //Raylib.DrawRectangleLines((int)transform.position.X, (int)transform.position.Y, (int)collision.size.X, (int) collision.size.Y, Raylib.SKYBLUE);
        }

        public bool Update()
        {
            shootTimer += Raylib.GetFrameTime();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                transform.position.X -= transform.speed * Raylib.GetFrameTime();
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                transform.position.X += transform.speed * Raylib.GetFrameTime();
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && shootTimer >= shootDelay)
            {
                shootTimer = 0;

                return true;
            }

            return false;
        }
    }
}
