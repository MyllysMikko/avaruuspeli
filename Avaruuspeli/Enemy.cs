using Raylib_CsLo;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

namespace Avaruuspeli
{
    internal class Enemy
    {
        public TransformComponent transform;
        public CollisionComponent collision;

        public bool active;

        public int scoreValue;

        public Texture enemyImage;

        public Enemy(Vector2 startPos, Vector2 direction, float speed, int size, int scoreValue, Texture enemyImage)
        {
            transform = new TransformComponent(startPos, direction, speed);
            collision = new CollisionComponent(new Vector2(size, size));
            active = true;
            this.scoreValue = scoreValue;
            this.enemyImage = enemyImage;
        }

        public void Update()
        {
            if (active)
            {
                transform.position += transform.direction * transform.speed * Raylib.GetFrameTime();
            }


        }

        public void Draw()
        {
            if (active)
            {
                float scaleX = collision.size.X / enemyImage.width;
                float scaleY = collision.size.Y / enemyImage.height;
                float scale = Math.Min(scaleX, scaleY);

                Raylib.DrawTextureEx(enemyImage, transform.position, 0f, scale, Raylib.WHITE);
                //Raylib.DrawRectangleLines((int)transform.position.X, (int)transform.position.Y, (int)collision.size.X, (int)collision.size.Y, Raylib.RED);
            }
        }
    }
}
