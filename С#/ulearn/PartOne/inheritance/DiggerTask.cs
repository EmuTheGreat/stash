using System.Drawing;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Player : ICreature
    {
        public static Point Location;

        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        private bool TryMove(int x, int y)
        {
            return !(Game.Map[x, y] is Sack || Game.Map[x, y] is Monster);
        }

        public CreatureCommand Act(int x, int y)
        {
            Keys key = Game.KeyPressed;

            if (key == Keys.Up && y > 0 && TryMove(x, y - 1))
                return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
            if (key == Keys.Down && y < Game.MapHeight - 1 && TryMove(x, y + 1))
                return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
            if (key == Keys.Right && x < Game.MapWidth - 1 && TryMove(x + 1, y))
                return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
            if (key == Keys.Left && x > 0 && TryMove(x - 1, y))
                return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold)
                Game.Scores += 10;
            return conflictedObject is Sack || conflictedObject is Monster;
        }
    }

    public class Sack : ICreature
    {
        public int CountFall = 0;
        public CreatureCommand Act(int x, int y)
        {
            int EdgeOfTheMap = Game.MapHeight - 1;

            while (y != EdgeOfTheMap)
            {
                var posInMap = Game.Map[x, y + 1];
                if (posInMap == null || (posInMap is Player || posInMap is Monster) && CountFall != 0)
                {
                    CountFall++;
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                }
                else if (CountFall > 1)
                    return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
                CountFall = 0;
                return new CreatureCommand { };
            }
            if (CountFall > 1)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            CountFall = 0;
            return new CreatureCommand { };
        }


        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        public static Point Location;

        private bool TryMove(int x, int y)
        {
            if (x == Game.MapWidth || y == Game.MapHeight) return false;
            return !(Game.Map[x, y] is Terrain || Game.Map[x, y] is Sack || Game.Map[x, y] is Monster);
        }

        public CreatureCommand Act(int x, int y)
        {
            int dx = 0;
            int dy = 0;
            if (IsDiggerAlive())
            {
                if (Player.Location.X == x)
                {
                    if (Player.Location.Y < y) dy = -1;
                    else if (Player.Location.Y > y) dy = 1;
                }
                else if (Player.Location.Y == y)
                {
                    if (Player.Location.X < x) dx = -1;
                    else if (Player.Location.X > x) dx = 1;
                }
                else
                {
                    if (Player.Location.X < x) dx = -1;
                    if (Player.Location.X > x) dx = 1;
                }
            }

            if (TryMove(x + dx, y + dy)) return new CreatureCommand { DeltaX = dx, DeltaY = dy };
            return new CreatureCommand { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public bool IsDiggerAlive()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                    if (Game.Map[i, j] is Player)
                    {
                        Player.Location = new Point { X = i, Y = j };
                        return true;
                    }
            return false;
        }
    }
}

