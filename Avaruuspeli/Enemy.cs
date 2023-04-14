using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    internal class Enemy
    {
        public TransformComponent transform;
        public CollisionComponent collision;

        public bool active;

        public Enemy(Vector2 startPos, Vector2 direction, float speed, int size)
        {
            transform = new TransformComponent(startPos, direction, speed);
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
                Raylib.DrawRectangleV(transform.position, collision.size, Raylib.GREEN);
            }
        }
    }
}
