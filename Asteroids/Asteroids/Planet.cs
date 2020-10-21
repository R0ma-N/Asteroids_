using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Planet: BaseObject
    {
        Image image;
        public static Image imagePlanet = Image.FromFile("Pictures\\Planet.png");
        public static Image imageSun = Image.FromFile("Pictures\\sun.png");
        public static Image imageNeptune = Image.FromFile("Pictures\\neptune.png");

        public Planet(Point pos,Point dir, Size size, Image img):base(pos,dir,size)
        {
            image = img;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
        }
    }
}
