

using Raylib_CsLo;
using System.Numerics;
using System.Transactions;

namespace Avaruuspeli
{
    internal class Bullet
    {
        public TransformComponent transform { get; private set; }
        public CollisionComponent collision { get; private set; }

        public bool active;

        public bool playerBullet;

        public Texture bulletImage;

        public Bullet(Vector2 startPosition, Vector2 direction, float speed, int size, Texture bulletImage, bool playerBullet)
        {
            transform = new TransformComponent(startPosition, direction, speed);
            collision = new CollisionComponent(new Vector2(size, size));

            active = true;
            this.bulletImage = bulletImage;
            this.playerBullet = playerBullet;
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
                float scaleX = collision.size.X / bulletImage.width;
                float scaleY = collision.size.Y / bulletImage.height;
                float scale = Math.Min(scaleX, scaleY);


                Rectangle bulletRec = new Rectangle(0, 0, 13, 54);
                Vector2 imagePos = new Vector2((int)(transform.position.X + collision.size.X * 0.5f) - (int)(bulletImage.width * 0.5f), transform.position.Y);
                Raylib.DrawTextureRec(bulletImage, bulletRec, imagePos, Raylib.WHITE);
                //Raylib.DrawRectangleLines((int)transform.position.X, (int)transform.position.Y, (int)collision.size.X, (int)collision.size.Y, Raylib.SKYBLUE);
            }
        }

        public void Reset(Vector2 pos, Vector2 dir, float speed, int size, bool playerBullet)
        {
            transform = new TransformComponent(pos, dir, speed);
            collision = new CollisionComponent(new Vector2(size, size));

            this.playerBullet = playerBullet;

            active = true;
        }

    }
}
