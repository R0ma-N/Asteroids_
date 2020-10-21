using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Asteroids
{
    class Game
    {
        public static Timer timer;
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;
        static public int Width { get; set; }
        static public int Height { get; set; }
        static BaseObject[] objs;
        public static Random rnd = new Random();
        private static List<Bullet>  bullets = new List<Bullet>();
        private static List<Asteroid>  asteroids = new List<Asteroid>(asteroidsCount);
        static int asteroidsCount = 1;
        private static Ship ship;
        private static MedPack medPack;
        static string msg = "Log:";
        static int score = 0;
        public static event Action<string> MessageToLog;
        
        static public void Init(Form form)
        {         
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            
            
            // проверка на задание размера экрана 
            //if (Width > 1000 || Height > 1000 || Width < 0 || Height < 0)
            //{
            //    throw new ArgumentOutOfRangeException("Границы экрана привысили допустимые значения");
            //}

            MessageToLog += log;
            MessageToLog += LogToFile;
            Ship.MessageDie += Ship.Message_Die;
            Ship.MessageDie += LogToFile;

            form.KeyDown += Form_KeyDown;
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
            LogToFile_Clear("Start loging at ");
            MessageToLog.Invoke("Started new game");
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) bullets.Add (new Bullet(new Point(ship.Rect.X + 130, ship.Rect.Y + 30), new Point(4, 0), new Size(20, 15)));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        static public void Load()
        {
            ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(220, 150));

            medPack = new MedPack(new Point(Width+50, 700), new Point(-4, 0), new Size(50, 50));

            objs = new BaseObject[133];
            for (int i = 0; i < 100; i++)
                objs[i] = new Star(new Point(rnd.Next(0, Width + 20), rnd.Next(-20, Height + 20)), new Point(-1 * rnd.Next(1, 3), 0), new Size(20, 20));
            for (int i = 100; i < 130; i++)
                objs[i] = new Star(new Point(rnd.Next(0, Width + 20), rnd.Next(-20, Height + 20)), new Point(-1 * rnd.Next(2, 4), 0), new Size(45, 45));

            objs[130] = new Planet(new Point(1000, 300), new Point(-1, 0), new Size(100, 100), Planet.imagePlanet);
            objs[131] = new Planet(new Point(2700, 600), new Point(-2, 0), new Size(138, 138), Planet.imageSun);
            objs[132] = new Planet(new Point(2500, 120), new Point(-1, 0), new Size(110, 110), Planet.imageNeptune);

            AsteroidsLoad();
        }

        static public void Draw()
        {
            buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in objs)
            {
                obj.Draw();
            }

            ship.Draw();


            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i] != null)
                {
                    bullets[i].Draw();
                }
                else
                {
                    bullets.RemoveAt(i);
                }
            }

            foreach (Asteroid a in asteroids) a.Draw();

            medPack?.Draw();

            buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0,0);
            buffer.Graphics.DrawString("Score:" + score, SystemFonts.DefaultFont, Brushes.White, 0, 20);
            //buffer.Graphics.DrawString(msg, SystemFonts.DefaultFont, Brushes.White, Width - 500, 0);
            if (ship.Energy <= 0) Game.buffer.Graphics.DrawString("Game over", new Font(FontFamily.GenericMonospace, 100, FontStyle.Underline), Brushes.White, Width / 2 - 400, Height / 2 - 100);

            buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();

            if (asteroids.Count != 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    for (int j = 0; j < asteroids.Count; j++)
                    {
                        if (bullets[i] != null && bullets[i].Collision(asteroids[j]))
                        {
                            System.Media.SystemSounds.Asterisk.Play();                       
                            asteroids.RemoveAt(j);
                            bullets[i] = null;
                            score++;
                            MessageToLog.Invoke("Bullet hit an asteroid");
                            MessageToLog.Invoke("Asteroid crashed");
                        }
                    }
                }
            }
            else
            {
                asteroidsCount++;
                AsteroidsLoad();
            }

            for (int i=0; i<asteroids.Count; i++)
            if (ship.Collision(asteroids[i]))
            {
                ship.EnergyLow(50);
                asteroids.RemoveAt(i);
                MessageToLog.Invoke("Ship collided with an asteroid");
            }

            if (ship.Collision(medPack))
            {
                System.Media.SystemSounds.Hand.Play();
                ship.EnergyHeight(50);
                medPack = new MedPack(new Point(rnd.Next(Width, Width + 10), rnd.Next(20, Height-20)), new Point(-4, 0), new Size(50, 50));
                MessageToLog.Invoke("Ship got medpack");
            }
        
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i] != null)
                {
                    bullets[i].Update();
                }
                else
                {
                    bullets.RemoveAt(i);
                }
            }

            foreach (Asteroid a in asteroids) a.Update();
            medPack?.Update();
        }

        static void AsteroidsLoad()
        {
            for (int i = 0; i < asteroidsCount; i++)
            {
                int imgSize = rnd.Next(80, 120);
                asteroids.Add(new Asteroid(new Point(rnd.Next(Game.Width, Game.Width + 10), Game.rnd.Next(0, Game.Height - 300)), new Point(-1 * rnd.Next(5, 10), 0), new Size(imgSize, imgSize)));
            }
        }

        static public void log(string n)
        {
            msg += "\n" + DateTime.Now.ToLongTimeString() + "  " + n;
        }

        // пишутся в bin/Debug/log.txt
        static void LogToFile_Clear(string n)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", false, Encoding.Default))
            {
                sw.WriteLine(n + DateTime.Now.ToLongTimeString());
            }
        }

        static void LogToFile(string n)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true, Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() + "  " + n);
            }
        }

    }
}
