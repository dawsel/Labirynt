using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Labirynt
{
/// <summary>
/// Definiuje sprita
/// </summary>
    public class Sprite2D
    {
        /// <summary>
        /// Pozycja 
        /// </summary>
        public Vector2 Position = null;
        /// <summary>
        /// Rozmiar
        /// </summary>
        public Vector2 Scale = null;
        /// <summary>
        /// Folder w ktorym sie znajduje 
        /// </summary>
        public string Directory = "";
        /// <summary>
        /// Tag
        /// </summary>
        public string Tag = "";
        public Bitmap Sprite = null;
        public bool IsReference = false;
        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag )
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[KSZTALT2D]({Tag}) - Has been registered");
            Gra.RegisterSprite(this);
        }
        
        /// <summary>
        /// Zaladowanie sprita do pamieci
        /// </summary>
        /// <param name="Directory"></param>
        public Sprite2D( string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;
          
            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[KSZTALT2D]({Tag}) - Has been registered");
            Gra.RegisterSprite(this);
        }
        /// <summary>
        /// Odwolanie do sprita z pamieci
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Scale"></param>
        /// <param name="reference"></param>
        /// <param name="Tag"></param>
        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D reference, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = reference.Sprite;

            Log.Info($"[KSZTALT2D]({Tag}) - Has been registered");
            Gra.RegisterSprite(this);
        }
        /// <summary>
        /// Funkcja realizujaca kolizje z przeszkodami 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsColliding(Sprite2D a, Sprite2D b)
        {
            if(a.Position.X < b.Position.X + b.Scale.X &&
                a.Position.X + a.Scale.X > b.Position.X &&
                a.Position.Y < b.Position.Y + b.Scale.Y &&
                a.Position.Y + a.Scale.Y > b.Position.Y)
              
            {
                return true;
            }
            
            
            return false;

        }
        /// <summary>
        /// Funkcja realizujaca kolizje z przeszkodami 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsColliding(string tag)
        {
    
           foreach(Sprite2D b in Gra.AllSprites)
            {
                if (b.Tag == tag)
                {
                    if (
                    Position.X < b.Position.X + b.Scale.X &&
                    Position.X + Scale.X > b.Position.X &&
                    Position.Y < b.Position.Y + b.Scale.Y &&
                    Position.Y + Scale.Y > b.Position.Y)

                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public void DestroySelf()
        {
            Gra.unRegisterSprite(this);
        }
    }
}
