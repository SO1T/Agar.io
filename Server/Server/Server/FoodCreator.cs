using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;

namespace Server
{
    public static class FoodCreator
    {
        public static ConcurrentBag<Circle> spawnedFood = new ConcurrentBag<Circle>();
        public static Vector2 CreateFood()
        {
            Random r = new Random();
            float x = r.Next(-50, 50);
            float y = r.Next(-50, 50);
            Vector2 foodPosition = new Vector2(x, y);
            spawnedFood.Add(new Circle { position = foodPosition, radius = 0.2f });
            return foodPosition;
        }
    }
}
