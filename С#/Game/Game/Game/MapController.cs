using Game.interfaces;
using Game.Models;
using System.Drawing;

namespace Game
{
    public class MapController
    {
        public ILevel currentLevel;
        public static int cellSize = 64;
        public static int spriteSize = 16;

        public MapController(ILevel level)
        {
            currentLevel = level;
        }

        public void UpdateCurrentLevel(ILevel newLevel, Player player)
        {
            currentLevel.entities.Remove(player);
            currentLevel = newLevel;
            currentLevel.entities.Add(player);
        }

        public void DrawMap(Graphics g)
        {
            for (int i = 0; i < currentLevel.mapWidth; i++)
            {
                for (int j = 0; j < currentLevel.mapHeight; j++)
                {
                    var rect = new Rectangle(new Point(i * cellSize, j * cellSize), new Size(66, 66));
                    var e = currentLevel.map[j, i];
                    switch (e)
                    {
                        case 0:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 1:
                        case 2:
                        case 3:
                            g.DrawImage(Textures.plainsSheet, rect, 16 * e, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 4:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 64, 80, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 5:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 64, 64, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 6:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 80, 64, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 7:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 80, 80, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 8:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 32, 96, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 9:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 48, 80, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 10:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 32, 64, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                        case 11:
                            g.DrawImage(Textures.grassSprite, rect, 0, 0, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            g.DrawImage(Textures.plainsSheet, rect, 16, 80, spriteSize, spriteSize, GraphicsUnit.Pixel);
                            break;
                    }
                }
            }
        }

        public int GetWidth()
        {
            return cellSize * currentLevel.mapWidth + 15;
        }

        public int GetHeight()
        {
            return cellSize * currentLevel.mapHeight + 14;
        }
    }
}
