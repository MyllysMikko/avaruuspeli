

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

        public Bullet(Vector2 startPosition, Vector2 direction, float speed, int size)
        {
            transform = new TransformComponent(startPosition, direction, speed);
            collision = new CollisionComponent(new Vector2(size, size));

            active = true;
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
                Raylib.DrawRectangleV(transform.position, collision.size, Raylib.RED);
            }
        }

        public void Reset(Vector2 pos, Vector2 dir, float speed, int size)
        {
            transform = new TransformComponent(pos, dir, speed);
            collision = new CollisionComponent(new Vector2(size, size));

            active = true;
        }

    }
}
