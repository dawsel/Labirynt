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
        Ksztalt2D player;
        public TEST(): base(new Vector2(615,515),"Labirynt Demo") { }


        public override void OnLoad()
        {
            BackgroundColor = Color.Beige;

            player = new Ksztalt2D(new Vector2(10, 10), new Vector2(10, 10), "Test");
        }


        public override void OnDraw()
        {
            
        }



        int time = 0;
   
        public override void OnUpdate()
        {
         
        }
    }
}
