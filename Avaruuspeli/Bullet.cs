

using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    internal class Bullet
    {
        public TransformComponent transform { get; private set; }
        public CollisionComponent collision { get; private set; }

        public Bullet(Vector2 startPosition, Vector2 direction, float speed, int size)
        {
            transform = new TransformComponent(startPosition, direction, speed);
            collision = new CollisionComponent(new Vector2(size, size));
        }

        public void Update()
        {
            transform.position += transform.direction * transform.speed * Raylib.GetFrameTime();
        }

        public void Draw()
        {
            Raylib.DrawRectangleV(transform.position, collision.size, Raylib.RED);
        }
    }
}
