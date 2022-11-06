using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirynt
{
    public class Ksztalt2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Tag = "";
        public Ksztalt2D(Vector2 Position, Vector2 Scale, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            Log.Info($"[KSZTALT2D]({Tag}) - Has been registered");
            Gra.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[KSZTALT2D]({Tag}) - Has been destroyed");

            Gra.unRegisterShape(this);
        }
    }
}
