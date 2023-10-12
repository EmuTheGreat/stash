using System;
using System.Collections.Generic;

namespace DominoLibrary
{
    public class Player
    {
        public static int CountOfDominos = 5;
        public List<DominoBone> dominoBones = new List<DominoBone>();
        public bool WinOrNot = false;

        // Убрать домино.(Ход игрока)
        public void RemoveDomino(DominoBone domino)
        {
            dominoBones.RemoveAt(dominoBones.IndexOf(domino));
        }

        // Добавить домино.
        public void AddDomino(DominoBone domino)
        {
            dominoBones.Add(domino);
        }
    }

    // Сущность костей.
    public class DominoBone
    {
        public int FirstSide;
        public int SecondSide;
    }

    // Сущность игры.
    public class Game
    {
        public List<Player> Players;
        public List<DominoBone> Bazaar = GenerateAndShuflleDominos();
        public DominoBone PlayingBoard;

        // Генерирует и перемешивает кости.
        public static List<DominoBone> GenerateAndShuflleDominos()
        {
            var dominoBones = new List<DominoBone>();
            var random = new Random();
            for (int i = 0; i < 7; i++)
                for (int j = i; j < 7; j++) dominoBones.Add(new DominoBone { FirstSide = i, SecondSide = j });

            for (int i = dominoBones.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (dominoBones[i], dominoBones[j]) = (dominoBones[j], dominoBones[i]);
            }
            return dominoBones;
        }

        // Определяет очередь игроков. Ищет наибольший дубль, если не находит дублей,
        // определяет последовательность по наибольшей кости.
        public void DefinePriority()
        {
            var twin = 0;
            int indexTwin = 0;
            var maxScore = 0;
            int indexMaxScore = 0;

            for (int i = 0; i < Players.Count; i++)
                foreach (var e in Players[i].dominoBones)
                {
                    if (e.FirstSide == e.SecondSide && e.FirstSide > twin) { twin = e.FirstSide; indexTwin = i; }
                    else if (e.FirstSide + e.SecondSide > maxScore) { maxScore = e.FirstSide + e.SecondSide; indexMaxScore = i; }
                }

            var index = twin != 0 ? indexTwin : indexMaxScore;
            var range = Players.GetRange(index, Players.Count - index);
            Players.RemoveRange(index, Players.Count - index);
            Players.InsertRange(0, range);
        }

        // Раздаёт домино игрокам из "Базара".
        public void DistributeDominos()
        {
            foreach (var player in Players)
                for (int i = 0; i < Player.CountOfDominos; i++)
                {
                    player.dominoBones.Add(Bazaar[Bazaar.Count - 1]);
                    Bazaar.RemoveAt(Bazaar.Count - 1);
                }
        }

        // Обновляет значения границ.
        public void UpdatePlayingBoard(int firstSide, int secondSide)
        {
            PlayingBoard.FirstSide = firstSide;
            PlayingBoard.SecondSide = secondSide;
        }
    }
}
