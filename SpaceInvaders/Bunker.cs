using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
        public Bunker(Vecteur Position) : base(Position, 1, Properties.Resources.bunker)
        {
        }

        public override void Update(Game gameInstance, double deltaT)
        {


        }

        public override void Collision(Missile m) {


            for (int i = 0; i < m.Image.Width; i++)
            {
                for (int j = 0; j < m.Image.Height; j++)
                {
                    int positionX = (int)m.Position.X + i - (int)Position.X;
                    int positionY = (int)m.Position.Y + j - (int)Position.Y;
                    if (Contact(positionX, positionY))
                    {

                        Color point = Image.GetPixel(positionX, positionY);


                        if (point.A == 255)
                        {


                            Image.SetPixel(positionX, positionY, Color.FromArgb(0, 255, 255, 255));
                            m.Lives = 0;
                            
                            
                        }
                    }
                }

            }
        }
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            m.Lives--;
        }
    }
}
