using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
	class Missile : SimpleObject
	{
	
		

		public Missile(Vecteur Pos, int Vie, Bitmap Img) : base(Pos, Vie, Img)
		{
			Position = Pos;
			Lives = Vie;
			Image = Img;

		}


		public override void Update(Game gameInstance, double deltaT)
		{
            
				foreach (GameObject gameObject in gameInstance.gameObjects)
			{
				gameObject.Collision(this);
			}
			Position.Y -= deltaT * 500;
			if (Position.Y < 0)
			{
				Lives = 0;
			}

		}
		protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
		{

			if (this != m)
			{
				m.Lives = 0;
				Lives = 0;

			}


		}

	}
}
