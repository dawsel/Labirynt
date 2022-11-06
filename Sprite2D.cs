using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Labirynt
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Bitmap Sprite = null;
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

        public void DestroySelf()
        {
            Gra.unRegisterSprite(this);
        }
    }
}
