using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    abstract class SimpleObject : GameObject
    {

        public Vecteur Position { get; set; }
        public int Lives { get; set; }
        public Bitmap Image { get; set; }
        protected abstract void OnCollision(Missile m, int numberOfPixelsInCollision);


        public SimpleObject(Vecteur pos, int vie, Bitmap img)
        {
            Position = pos;
            Lives = vie;
            Image = img;
        }
        public override void Draw(Game gameInstance, Graphics graphics)
        {

            graphics.DrawImage(Image, (float)Position.X, (float)Position.Y, Image.Width, Image.Height);

        }

        public override bool IsAlive()
        {

            if (Lives > 0)
            {
                return true;
            }
            return false;

        }
        public bool Contact(int X, int Y)
        {
            if (X >= 0 && X < Image.Width && Y >= 0 && Y < Image.Height)
            {
                return true;
            }
            return false;



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

                            Color pixel = Image.GetPixel(positionX, positionY);


                            
                        }
                    }

                }
            }
        }



    }

