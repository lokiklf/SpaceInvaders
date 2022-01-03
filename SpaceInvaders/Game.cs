using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents the entire game, it implements the singleton pattern
    /// </summary>
    /// 
    enum GameState { Play, Pause };
    class Game
    {
        private GameState state;
        public SpaceShip player;
        private EnemyBlock enemies;



        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }

        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            // Player

            Vecteur pos2 = new Vecteur(298, 550);
            player = new PlayerSpaceship(pos2, 3, SpaceInvaders.Properties.Resources.ship3);
            this.gameSize = gameSize;

            gameObjects.Add(player);

            // Bunker


            Vecteur pos4 = new Vecteur(gameSize.Width / 7 , 450);
            Bunker bunker1 = new Bunker(pos4);
            AddNewGameObject(bunker1);
            

            Vecteur pos5 = new Vecteur(gameSize.Width / 7 * 3, 450);
            Bunker bunker2 = new Bunker(pos5);
            AddNewGameObject(bunker2);

            Vecteur pos6 = new Vecteur(gameSize.Width / 7 * 5 , 450);
            Bunker bunker3 = new Bunker(pos6);
            AddNewGameObject(bunker3);


            // EnemyBlock 


            Vecteur posEnnemie = new Vecteur(200, 100);
            enemies = new EnemyBlock(650, posEnnemie);
            enemies.AddLine(9, 2, SpaceInvaders.Properties.Resources.ship6, this);


            AddNewGameObject(enemies);

        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {

            if (state == GameState.Pause)
            { 
                g.DrawString("PAUSE", defaultFont, blackBrush, new PointF((float)gameSize.Width /(float)2.4 , (float)gameSize.Height / (float)2.1));

            }
            else
            {
                           
               foreach (GameObject gameObject in gameObjects)
                    gameObject.Draw(this, g);


            }
        }
        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();
            if (keyPressed.Contains(Keys.P))
            {
                if (state == GameState.Play)
                {
                    state = GameState.Pause;
                    ReleaseKey(Keys.P);
                }
                else if (state == GameState.Pause)
                {
                    state = GameState.Play;
                    ReleaseKey(Keys.P);
                }
            }
        
            //    // create new BalleQuiTombe
            //    //GameObject newObject = new BalleQuiTombe(gameSize.Width / 2, 0);
            //    // add it to the game
            //    // AddNewGameObject(newObject);
            //    // release key space (no autofire)
            //}

            // update each game object
            if (state == GameState.Play)
            {
                foreach (GameObject gameObject in gameObjects.ToList())
                {
                    gameObject.Update(this, deltaT);
                }

                

                #endregion
            }
            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());

        }
    }
}