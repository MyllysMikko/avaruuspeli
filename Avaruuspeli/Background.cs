using Raylib_CsLo;
using System;
using System.Collections.Generic;
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
            Console.WriteLine(starlist.Count);
            if (Raylib.GetTime() > nextSpawn)
            {
                Random rand = new Random();
                Vector2 startPos = new Vector2(rand.Next(0, Window_width), 0 - size);
                nextSpawn = Raylib.GetTime() + spawnDelay;
                Star star = new Star(startPos, new Vector2(0, 1), 200, new Vector2(size, size));
                starlist.Add(star);
            }

            foreach(Star star in starlist)
            {
                star.Update();
            }

            for (int i = 0; i < starlist.Count; i++)
            {
                if (starlist[i].Transform.position.Y > Window_height)
                {
                    starlist.Remove(starlist[i]);
                }
            }
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
            public Star(Vector2 startPosition, Vector2 direction, float speed, Vector2 size)
            {
                Transform = new TransformComponent(startPosition, direction, speed);
                Collision = new CollisionComponent(size);
            }

            public void Update()
            {
                Transform.position += Transform.direction * Transform.speed * Raylib.GetFrameTime();
            }

            public void Draw()
            {
                Raylib.DrawRectangleV(Transform.position, Collision.size, Raylib.WHITE);
            }
        }
    }
}
