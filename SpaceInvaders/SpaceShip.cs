using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
	class SpaceShip : SimpleObject
	{
		private double speedPixelPerSecond;
		private Missile missile;


		public SpaceShip(Vecteur Pos, int Vie, Bitmap Img) : base(Pos, Vie, Img)
		{
			speedPixelPerSecond = 250;
		}

		public override void Update(Game gameInstance, double deltaT) { 
		
		}
		public void Droite(double deltaT)
		{
			Position.X = deltaT * speedPixelPerSecond + Position.X;
		}
		public void Gauche(double deltaT)
		{
			Position.X -= deltaT * speedPixelPerSecond;
		}
		public void Shoot(Game gameInstance, double deltaT)
		{
			if (missile == null || missile.Lives == 0)
			{
				Vecteur pos3 = new Vecteur(Position.X + 10, Position.Y);
				missile = new Missile(pos3, 2, Properties.Resources.shoot1);
				gameInstance.gameObjects.Add(missile);
				gameInstance.Update(deltaT);
			}
		}
		protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
		{

			if (m != missile)
			{
				Console.WriteLine(Lives);
				if (m.Lives <= Lives)
				{
					Lives -= m.Lives;
					m.Lives = 0;


				}
				else
				{
					m.Lives -= Lives;
					Lives = 0;

				}
			}

		}

	}
}