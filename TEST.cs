using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Labirynt
{
    class TEST :Gra
    {
        Sprite2D player;


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



    

   
        public override void OnUpdate()
        {
          
        }
    }
}
