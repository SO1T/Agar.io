using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Server
{
    public class Player: Circle
    {
        public Vector2 velocity = new Vector2(0, 0);
        public float speed = 10f;
        public float calcTime = 0.1f;//we calculate time 100 times per second
        public int Id { get; set; }
        public Vector2 GetPosition(Vector2 direction)
        {
            velocity = new Vector2(direction.X , direction.Y);
            position += calcTime * velocity;
            return position;
        }

        public bool isCollision(Circle circle)
        {
            var dx = position.X - circle.position.X;
            var dy = position.Y - circle.position.Y;
            var distance = Math.Pow(dx * dx + dy * dy, 2);

            if (distance < radius + circle.radius)
            {
                return true;
            }
            return false;
        }
    }
}
