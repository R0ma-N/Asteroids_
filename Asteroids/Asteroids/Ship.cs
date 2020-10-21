using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ship : BaseObject
    {
        Image image = Image.FromFile("Pictures\\ship.png");

        // событие на основе обобщенного делегата Action
        public static event Action<string> MessageDie;

        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n)
        {
            _energy -= n;
            if (Energy <= 0) MessageDie.Invoke("Ship crashed becouse");
        }

        public void EnergyHeight(int n)
        {
            _energy += n;
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, new Rectangle(pos, new Size(220, 150)));
        }

        public override void Update()
        {

        }

        public void Up()
        {
            if (pos.Y > 0)
            {
                pos.Y = pos.Y - dir.Y;
            }
        }

        public void Down()
        {
            if (pos.Y < Game.Height-size.Height)
            {
                pos.Y = pos.Y + dir.Y;
            }
        }

        public static void Message_Die(string obj)
        {
            Game.timer.Stop();
            Game.log(obj);
        }

    }
}
