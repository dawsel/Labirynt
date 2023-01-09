using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Labirynt
{
    /// <summary>
    /// Canvas
    /// </summary>
    class Canvas : Form
    {
        /// <summary>
        /// Zapobiega migotaniu
        /// </summary>
        public Canvas()
        {
           
            this.DoubleBuffered = true;
            
        }

       
    }
    
    
    
    /// <summary>
    /// Glowna klasa
    /// </summary>
    public abstract class Gra
    {
        /// <summary>
        /// Rozmiar aplikacji 
        /// </summary>
        private Vector2 ScreenSize = new Vector2(512, 512);
        /// <summary>
        /// Tytuł aplikacji
        /// </summary>
        private string Title = "Labirynt";
        private Canvas Window = null;
        private Thread GameLoopThread = null;
        /// <summary>
        /// Numer poziomu
        /// </summary>
        public int level = 1;
        /// <summary>
        /// Czy zwyciestwo
        /// </summary>
        public bool zwyciestwo = false;
        /// <summary>
        /// Czy reset
        /// </summary>
        bool reset = false;
        TimeSpan ts;
        TimeSpan best;
        string s;
        string elapsedTime;
        string bestTime;
        /// <summary>
        /// Czas gry 
        /// </summary>
        string czas;
        Stopwatch stopWatch = new Stopwatch();
        /// <summary>
        /// Lista kształtów 
        /// </summary>
        public static List<Ksztalt2D> AllShapes = new List<Ksztalt2D>();
        /// <summary>
        /// Lista spritow 
        /// </summary>
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        /// <summary>
        /// Kolor tla
        /// </summary>
        public Color BackgroundColor = Color.Red;

        public Vector2 CameraPosition = Vector2.Zero();
        /// <summary>
        /// Glowna funkcja 
        /// </summary>
        /// <param name="ScreenSize"></param>
        /// <param name="Title"></param>
        public Gra (Vector2 ScreenSize, string Title)
        {
            Log.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.MouseDown += MouseDown;
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Window.FormClosing += Window_FormClosing;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();
           

            Application.Run(Window);
        }
        /// <summary>
        /// Zamykanie aplikacji 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterShape(Ksztalt2D shape)
        {
            AllShapes.Add(shape);
        }
        /// <summary>
        /// Dodanie spritea
        /// </summary>
        /// <param name="sprite"></param>
        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }
        public static void unRegisterShape(Ksztalt2D shape)
        {
            AllShapes.Remove(shape);
        }
        /// <summary>
        /// Usuniecie sprita
        /// </summary>
        /// <param name="sprite"></param>
        public static void unRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }
        /// <summary>
        /// Petla gry
        /// </summary>
        void GameLoop()
        {
           

            OnLoad();
            
            stopWatch.Start();
            while (GameLoopThread.IsAlive)
            {
                try
                { 
                
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                 
                    if (zwyciestwo == true)
                    {
                        stopWatch.Stop();

                    }
                    ts = stopWatch.Elapsed;
                    elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                    czas = elapsedTime;


                }
                catch
                {
                    Log.Error("Game has not been found...");
                }
            }
        }
       


        /// <summary>
        /// Funkcja rysujaca obiekty i napisy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);
            if (reset == true)
            {
                g.Clear(BackgroundColor);
                reset = false;
                zwyciestwo = false;
                
                
            }
            Font fnt = new Font("Arial", 30);
            Font fnt2 = new Font("Arial", 60);
            Font fnt3 = new Font("Arial", 20);
            if (zwyciestwo == true)
            {
                g.DrawString("Zwycięstwo!",
                fnt2, System.Drawing.Brushes.Blue, new Point(400, 400));
                czasy();
                g.DrawString("Najlepszy czas: "+ bestTime,
                fnt2, System.Drawing.Brushes.Blue, new Point(200, 500));
                
                
            }
            if (level < 3)
            {
                g.DrawString("Poziom: " + level,
                fnt, System.Drawing.Brushes.Blue, new Point(150, 925));
            }
            else
            {
                g.DrawString("Poziom: 3" ,
                fnt, System.Drawing.Brushes.Blue, new Point(150, 925));
            }
            g.DrawString("Czas: "+ czas,
               fnt, System.Drawing.Brushes.Blue, new Point(800, 925));

            g.FillRectangle(new SolidBrush(Color.Red), 500, 900, 200, 80);
            g.DrawString("RESTART", fnt3, System.Drawing.Brushes.Blue, new Point(535, 925));

            



            g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
            try
            {


                foreach (Ksztalt2D shape in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);

                }
                foreach (Sprite2D sprite in AllSprites)
                {
                    if (!sprite.IsReference)
                    {
                        g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
                    }
                }
            }

            catch { }
        }
        private void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Update the mouse path with the mouse information
            Point mouseDownLocation = new Point(e.X, e.Y);

            Console.WriteLine(mouseDownLocation);


            if (e.X >= 500 && e.X <= 700 && e.Y >=900 && e.Y<=980)
            {
                level = 1;
                AllSprites.Clear();
                reset = true;
                czas = null;
                
                OnLoad();
                
                stopWatch.Reset();
                stopWatch.Start();

            }
            
        
        }
        /// <summary>
        /// Funkcja zapisujaca i odczytujaca najlepszy czas
        /// </summary>
       private void czasy()
        {
            string path = @"czasy.txt";
            if (TimeSpan.Compare(best,ts)==1)
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    
                    sw.WriteLine(ts);
                    //Console.WriteLine(ts);
                }
            }

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                
                while ((s = sr.ReadLine()) != null)
                {
                    best = TimeSpan.Parse(s);
                    bestTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    best.Hours, best.Minutes, best.Seconds,
                    best.Milliseconds / 10);
                    Console.WriteLine(s);
                    //Console.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Ladowanie mapy 
        /// </summary>
        public abstract void OnLoad();
        /// <summary>
        /// Pozwala zmienic pozycje gracza
        /// </summary>
        public abstract void OnUpdate();
        
        public abstract void OnDraw();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
