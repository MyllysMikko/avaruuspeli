using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class CollisionComponent
    {
        public Vector2 size;

        public CollisionComponent(Vector2 size)
        {
            this.size = size;
        }
    }
}
