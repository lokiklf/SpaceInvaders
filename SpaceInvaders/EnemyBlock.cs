using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EnemyBlock : GameObject
    {
        private bool Droit;
        private bool Gauche;
        private HashSet<SpaceShip> lives = new HashSet<SpaceShip>();
        private int baseWidth;
        public Size Size { get; set; }
        public Vecteur Position { get; set; }

        int getRight()
        {
            int max = -1;
            foreach (SpaceShip ship in lives)
            {
                if (ship.Position.X + ship.Image.Width > max)
                {
                    max = (int)ship.Position.X + ship.Image.Width;
                }
            }
            return max;
        }
        int getLeft()
        {
            int min = 1000000;
            foreach (SpaceShip ship in lives)
            {
                if (ship.Position.X < min)
                {
                    min = (int)ship.Position.X;
                }
            }
            return min;
        }

        int getBottom()
        {
            int max = -1;
            foreach (SpaceShip ship in lives)
            {
                if (ship.Position.Y + ship.Image.Height > max)
                {
                    max = (int)ship.Position.Y + ship.Image.Height;
                }
            }
            return max;
        }
        int getTop()
        {
            int min = 100000;
            foreach (SpaceShip ship in lives)
            {
                if (ship.Position.Y < min)
                {
                    min = (int)ship.Position.Y;
                }
            }
            return min;
        }

        public EnemyBlock(int larg, Vecteur Pos){

            Droit = true;
            Gauche = false;

            baseWidth = larg;
            Position = Pos;

        }
        public void AddLine(int nbShips, int nbLives, Bitmap shipImage, Game gameinstance){

             Vecteur position = new Vecteur(100,100);


            for (int i = 0; i < 10; i++)
            {
                    SpaceShip mechant = new SpaceShip(position, 3, SpaceInvaders.Properties.Resources.ship6);
                    mechant.Image = shipImage;
                    gameinstance.AddNewGameObject(mechant);
                    lives.Add(mechant);
                   
            }
            UpdateSize();
        }
        public void UpdateSize(){
            Position.X = getLeft();
            Position.Y = getTop();
            Size size = new Size(getRight() - getLeft(), 30 + getBottom() - getTop());
            Size = size;
        }
        public override void Collision(Missile m){

            foreach (SpaceShip enemy in lives)
            {
                enemy.Collision(m);
            }


        }
        public override void Draw(Game gameInstance, Graphics graphics){
            foreach (SpaceShip enemy in lives)
            {

                enemy.Draw(gameInstance, graphics);

            }

            Pen couleur = new Pen(Color.Red);
            Rectangle rec = new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
            graphics.DrawRectangle(couleur, rec);

        }
        public void goRight(double deltaT)
        {

            foreach (SpaceShip mechant in lives)
            {
                mechant.Position.X += 5* deltaT + 0.1;
            }
            Position.X += 5 * deltaT + 0.1;

        }
        public void goLeft(double deltaT)
        {

            foreach (SpaceShip mechant in lives)
            {
                mechant.Position.X -= 5 * deltaT + 0.1;
            }

            Position.X -= 5 * deltaT + 0.1;
        }
        public void goDown(double deltaT)
        {
            foreach (SpaceShip ship in lives)
            {
                ship.Position.Y += 5 * deltaT * 10;
            }

            Position.Y += 5 * deltaT * 10;
        }
        public void changementDirection(double deltaT)
        {
            if (Droit)
            {
                Droit = false;
                Gauche = true;
            }
            else if (Gauche)
            {
                Gauche = false;
                Droit = true;
            }
            goDown(deltaT);
        }
        public override void Update(Game gameInstance, double deltaT){
            lives.RemoveWhere(gameObject => !gameObject.IsAlive());
            UpdateSize();


            if (Droit)
            {
                goRight(deltaT);
            }
            if (Gauche)
            {
                goLeft(deltaT);
            }

            if (getLeft() <= 0 || getRight() >= gameInstance.gameSize.Width)
            {
                changementDirection(deltaT);
            }

        }
   
  
        public override bool IsAlive()
        {
            if (lives.Count() > 0)
            {
                return true;
            }
            return false;


        }
    }
}

