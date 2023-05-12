using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class Background
    {
        public int Window_width { get; private set; }
        int Window_height;
        int size;
        List<Star> starlist;
        double nextSpawn;
        float spawnDelay;



        public Background(int Window_Width, int Window_Height, float spawnDelay, int size)
        {
            this.Window_width = Window_Width;
            this.Window_height = Window_Height;
            starlist = new List<Star>();
            this.spawnDelay = spawnDelay;
            nextSpawn = 0;
            this.size = size;
        }

        public void Update()
        {
            if (Raylib.GetTime() > nextSpawn)
            {
                SpawnStar();
            }

            foreach(Star star in starlist)
            {
                star.Update();
                if (star.Transform.position.Y > Window_height)
                {
                    star.active = false;
                }
            }

        }

        /// <summary>
        /// Spawnaa tähtiä. Sama periaate kuin luotien luonnissa.
        /// </summary>
        void SpawnStar()
        {
            nextSpawn = Raylib.GetTime() + spawnDelay;
            Random rand = new Random();
            Vector2 startPos = new Vector2(rand.Next(0, Window_width - size), 0 - size);
            foreach (Star tähti in starlist)
            {
                if (!tähti.active)
                {
                    tähti.Transform.position = startPos;
                    tähti.active = true;
                    return;
                }
            }
            
            

            Star star = new Star(startPos, new Vector2(0, 1), 200, new Vector2(size, size));
            starlist.Add(star);
        }

        public void Draw()
        {
            foreach (Star star in starlist)
            {
                star.Draw();
            }
        }


        class Star
        {
            public TransformComponent Transform { get; private set; }
            public CollisionComponent Collision { get ; private set; }

            public bool active;
            public Star(Vector2 startPosition, Vector2 direction, float speed, Vector2 size)
            {
                Transform = new TransformComponent(startPosition, direction, speed);
                Collision = new CollisionComponent(size);
                active = true;
            }

            public void Update()
            {
                if (active)
                {
                    Transform.position += Transform.direction * Transform.speed * Raylib.GetFrameTime();
                }
            }

            public void Draw()
            {
                if (active)
                {
                    Raylib.DrawRectangleV(Transform.position, Collision.size, Raylib.WHITE);
                }
            }
        }
    }
}
