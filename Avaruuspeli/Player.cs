using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    internal class Player
    {
        public TransformComponent transform { get; private set; }
        public CollisionComponent collision { get; private set; }

        float shootDelay = 1f;
        double nextShoot = 0;




        public Player(Vector2 startPos, int speed, int size)
        {
            transform = new TransformComponent(startPos, new Vector2(0, 0), speed);
            collision = new CollisionComponent(new Vector2(size, size));
        }

        public void Draw()
        {
            Raylib.DrawRectangleV(transform.position, collision.size, Raylib.SKYBLUE);
        }

        public bool Update()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                transform.position.X -= transform.speed * Raylib.GetFrameTime();
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                transform.position.X += transform.speed * Raylib.GetFrameTime();
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && Raylib.GetTime() > nextShoot)
            {
                nextShoot = Raylib.GetTime() + shootDelay;

                return true;
            }

            return false;
        }
    }
}
