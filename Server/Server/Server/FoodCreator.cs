using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;

namespace Server
{
    public static class FoodCreator
    {
        public static Vector2 CreateFood()
        {
            Random r = new Random();
            float x = r.Next(-50, 50);
            float y = r.Next(-50, 50);
            return new Vector2(x, y);            
        }
    }
}
