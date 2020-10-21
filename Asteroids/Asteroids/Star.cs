using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Star: BaseObject
    {
        Image image = Image.FromFile("Pictures\\star_b.png");

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            //this.image = image;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, new Rectangle(pos, size));
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < -30)
            {
                pos.X = Game.rnd.Next(Game.Width, Game.Width + 10);
                pos.Y = Game.rnd.Next(0, Game.Height);
            }
        }
    }
}
