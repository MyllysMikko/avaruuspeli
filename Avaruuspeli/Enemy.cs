using Raylib_CsLo;
using System.Numerics;

namespace Avaruuspeli
{
    internal class Enemy
    {
        public TransformComponent transform;
        public CollisionComponent collision;

        public Enemy(Vector2 startPos, Vector2 direction, float speed, int size)
        {
            transform = new TransformComponent(startPos, direction, speed);
            collision = new CollisionComponent(new Vector2(size, size));
        }

        public void Update()
        {

            transform.position += transform.direction * transform.speed * Raylib.GetFrameTime();


        }

        public void Draw()
        {
            Raylib.DrawRectangleV(transform.position, collision.size, Raylib.GREEN);
        }
    }
}
