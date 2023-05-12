using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class MovingText
    {
        public TransformComponent transform { get; private set; }

        public string text;

        public Color color;

        public int size;

        public float aliveUntil;

        public bool active;


        public MovingText(string Text, Vector2 startPosition, Vector2 direction, float speed, float lifeTime, Color color, int size)
        {
            this.text = Text;
            transform = new TransformComponent(startPosition, direction, speed);
            this.color = color;
            aliveUntil = (float)Raylib.GetTime() + lifeTime;
            active = true;
            this.size = size;
        }

        public void Update()
        {
            if (active)
            {
                transform.position += transform.direction * transform.speed * Raylib.GetFrameTime();
            }

            if (Raylib.GetTime() > aliveUntil)
            {
                active = false;
            }
        }

        public void Draw()
        {
            if (active)
            {
                Raylib.DrawTextEx(Raylib.GetFontDefault(), text, transform.position, size, 10, color);
            }
        }
    }
}
