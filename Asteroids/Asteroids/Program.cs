﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 1920; // SystemInformation.PrimaryMonitorSize.Width;
            form.Height = 1080;// SystemInformation.PrimaryMonitorSize.Height;
            //form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
            Game.Init(form);
            Application.Run(form);
        }
    }
}