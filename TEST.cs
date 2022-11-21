using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Labirynt
{
    class TEST :Gra
    {
        Sprite2D player;

        bool left, right, up, down;
        Vector2 lastPos = Vector2.Zero();

        string[,] Map1 =
        {
          {".",".",".",".",".",".","." },
          {".",".",".",".",".",".","." },
          {".",".",".",".",".",".","." },
          {".",".",".",".",".",".","." },
          {".",".",".",".",".",".","." },
          {"w","w","w","w","w","w","w" },
          {".",".",".",".",".",".","." },

        };

        public TEST(): base(new Vector2(615,515),"Labirynt Demo") { }


        public override void OnLoad()
        {
            BackgroundColor = Color.Beige;
            //CameraPosition.X = 100;


            //player = new Ksztalt2D(new Vector2(10, 10), new Vector2(10, 10), "Test");
            player = new Sprite2D(new Vector2(10, 10), new Vector2(32,32 ),"Tiles/tile_0097", "Player");

            for(int i = 0; i< Map1.GetLength(0); i++ )
            {
                for (int j = 0; j < Map1.GetLength(1); j++)
                {
                    if (Map1[i, j] == "w")
                    {

                        new Sprite2D(new Vector2(j*32, i*32), new Vector2(32, 32), "Tiles/tile_0040", "wall");

                    }
                }
            }

        }


        public override void OnDraw()
        {
            
        }




        int times;
   
        public override void OnUpdate()
        {
            if(up)
            {
                player.Position.Y -= 5f;
            }
            if (down)
            {
                player.Position.Y += 5f;
            }
            if (left)
            {
                player.Position.X -= 5f;
            }
            if (right)
            {
                player.Position.X += 5f;
            }
            if(player.IsColliding("wall"))
            {
                Log.Info($"COLLIDING! {times}");
                times++;

                player.Position.X = lastPos.X;
                player.Position.Y = lastPos.Y;

            }
            else
            {
                lastPos.X = player.Position.X;
                lastPos.Y = player.Position.Y;

            }
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.Right) { right = true; }

        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.Down) { down = false; }
            if (e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.Right) { right = false; }
        }
    }
}
