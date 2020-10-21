using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids 
{
    class Bullet : BaseObject
    {
        Image imgBullet = Image.FromFile("Pictures\\Bullet.png");

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(imgBullet, new Rectangle(pos.X, pos.Y, size.Width, size.Height));
        }

        public override void Update()
        {
            pos.X = pos.X + 15;
        }

        public static void message_shot(string obj)
        {
            Game.log(obj);
        }
    }
}
