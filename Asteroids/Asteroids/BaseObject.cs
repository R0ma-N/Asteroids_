using System.Drawing;

namespace Asteroids
{
    abstract class BaseObject : ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        protected BaseObject(Point pos, Point dir,Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public abstract void Update();

        public abstract void Draw();

        public Rectangle Rect => new Rectangle(pos, size);

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(this.Rect);
        }
       
    }
}
