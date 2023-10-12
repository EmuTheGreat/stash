using System;
using System.Drawing;
using System.Windows.Forms;
using Game.Models;
using System.Linq;
using System.Drawing.Drawing2D;
using Game.Levels;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Game
{
    public class MVC
    {
        public static Player player;
        public static MapController mapController;
        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }

        public class Model
        {
            public static List<IEntity> entities;
            public Model()
            {
                WindowWidth = 1024;
                WindowHeight = 768;
                Textures.LoadContent();

                player = new Player(800, 800, new PlayerModel(), Textures.playerSheet);
                mapController = new MapController(new LevelStart());
                Player.SetBounds();
                mapController.currentLevel.entities.Add(player);
            }
        }

        public class Controller
        {
            public static void KeyPress(object sender, KeyEventArgs e)
            {
                if (player.isAlive && !player.isAttack)
                {
                    switch (e.KeyCode)
                    {
                        #region Move
                        case Keys.W:
                            player.dirY = -player.speed;
                            player.isMovingUp = true;
                            break;
                        case Keys.S:
                            player.dirY = player.speed;
                            player.isMovingDown = true;
                            break;
                        case Keys.A:
                            player.dirX = -player.speed;
                            player.isMovingLeft = true;
                            player.flip = -1;
                            break;
                        case Keys.D:
                            player.dirX = player.speed;
                            player.isMovingRight = true;
                            player.flip = 1;
                            break;
                        case Keys.J:
                            player.isAlive = false;
                            break;
                        #endregion

                        #region Attack
                        case Keys.Up:
                            player.isAttack = true;
                            player.SetAnimation(8);
                            player.currentAttack = player.attackUp;
                            player.Attack();
                            break;
                        case Keys.Down:
                            player.isAttack = true;
                            player.SetAnimation(6);
                            player.currentAttack = player.attackDown;
                            player.Attack();
                            break;
                        case Keys.Left:
                            player.isAttack = true;
                            player.flip = -1;
                            player.SetAnimation(7);
                            player.currentAttack = player.attackLeft;
                            player.Attack();
                            break;
                        case Keys.Right:
                            player.isAttack = true;
                            player.flip = 1;
                            player.SetAnimation(7);
                            player.currentAttack = player.attackRight;
                            player.Attack();
                            break;
                            #endregion
                    }
                }

                if (e.KeyCode == Keys.K)
                {
                    player.isAlive = true;
                    player.deathAnimationFlag = false;
                    player.SetAnimation(1);
                }
            }

            public static void KeyUp(object sender, KeyEventArgs e)
            {
                if (player.isAlive && !player.isAttack)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.W:
                            player.dirY = player.isMovingDown ? player.dirY : 0;
                            player.isMovingUp = false;
                            if (!player.IsMoving()) player.SetAnimation(2);
                            break;
                        case Keys.S:
                            player.dirY = player.isMovingUp ? player.dirY : 0;
                            player.isMovingDown = false;
                            if (!player.IsMoving()) player.SetAnimation(0);
                            break;
                        case Keys.A:
                            player.dirX = player.isMovingRight ? player.dirX : 0;
                            player.isMovingLeft = false;
                            if (!player.IsMoving()) player.SetAnimation(1);
                            break;
                        case Keys.D:
                            player.dirX = player.isMovingLeft ? player.dirX : 0;
                            player.isMovingRight = false;
                            if (!player.IsMoving()) player.SetAnimation(1);
                            break;
                    }
                }
            }
        }

        public class View
        {
            public static void Paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                int cameraX = player.posX - WindowWidth / 2;
                int cameraY = player.posY - WindowHeight / 2 + player.size / 2;

                cameraX = Math.Max(0, Math.Min(mapController.GetWidth() - WindowWidth, cameraX));
                cameraY = Math.Max(0, Math.Min(mapController.GetHeight() - WindowHeight + 25, cameraY));

                g.TranslateTransform(-cameraX, -cameraY);

                mapController.DrawMap(g);

                Model.entities = new List<IEntity>(mapController.currentLevel.entities) { player };

                foreach (var entity in Model.entities.OrderBy(x => x.posY + x.size + x.delta))
                {
                    entity.PlayAnimation(g);
                }

                //player.PlayAnimation(g);
                //slime.PlayAnimation(g);
            }
        }
    }

    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new MVC.Model();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1() { Size = new Size(MVC.WindowWidth, MVC.WindowHeight) });
        }
    }
}