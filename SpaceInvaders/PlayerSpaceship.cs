using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
	 class PlayerSpaceship: SpaceShip
	{
		public PlayerSpaceship(Vecteur Pos, int Vie, Bitmap Img) : base(Pos, Vie, Img)
		{
		}

		public override void Update(Game gameInstance, double deltaT)
		{
			if (gameInstance.keyPressed.Contains(Keys.Right) && Position.X < gameInstance.gameSize.Width - Image.Width)
			{
				Droite(deltaT);

			}
			if (gameInstance.keyPressed.Contains(Keys.Left) && Position.X > 0)
			{
				Gauche(deltaT);
			}


			if (gameInstance.keyPressed.Contains(Keys.Space))
			{

				Shoot(gameInstance, deltaT);


			}


		}
	}
}
