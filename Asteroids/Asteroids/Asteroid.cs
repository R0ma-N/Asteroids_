using System.Collections.Generic;
using System.Drawing;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        static List<Image> imgsAsteroid = new List<Image>(5)
        {
          Image.FromFile("Pictures\\asteroid.png"),
          Image.FromFile("Pictures\\ast1.png"),
          Image.FromFile("Pictures\\ast2.png"),
          Image.FromFile("Pictures\\ast3.png"),
          Image.FromFile("Pictures\\ast4.png"),
        };

        Image imgRnd = imgsAsteroid[Game.rnd.Next(0, 4)];

        public int Power { get; set; }

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(imgRnd, new Rectangle(pos, size));
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < -20)
            {
                pos.X = Game.rnd.Next(Game.Width, Game.Width + 20);
                pos.Y = Game.rnd.Next(0, Game.Height - size.Height);
            }

        }
    }
}
