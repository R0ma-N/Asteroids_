using System;
using System.Drawing;

namespace Asteroids
{
    class MedPack : BaseObject
    {
        Image image = Image.FromFile("Pictures\\med.png");

        public MedPack(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, new Rectangle(pos, new Size(70, 60)));
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            Convert.ToDouble(pos.X);
            double a = Math.Sin(pos.X * 55)*8;
            pos.Y = pos.Y + Convert.ToInt32(a);

            if (pos.X < -20)
            {
                pos.X = Game.rnd.Next(Game.Width, Game.Width + 10);
                pos.Y = Game.rnd.Next(20, Game.Height-20);
            }
        }
    }
}
