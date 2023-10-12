using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket) => GetNextMoveAsync(rocket).Result;

        async Task<Rocket> GetNextMoveAsync(Rocket rocket)
        {
            // запускаем задачи
            var tasks = CreateTasks(rocket);
            // ожидаем завершения
            var bestMove = await tasks[0];
            foreach (var task in tasks)
            {
                var bm = await task;
                if (bm.Score > bestMove.Score) bestMove = bm;
            }
            return rocket.Move(bestMove.Turn, level);
        }

        public List<Task<(Turn Turn, double Score)>> CreateTasks(Rocket rocket)
        {
            var tCnt = threadsCount;
            var iCnt = iterationsCount / tCnt;
            return Enumerable.Range(1, tCnt)
            .Select(async (i) => SearchBestMove(rocket, new Random(i), iCnt))
            .ToList();
        }
    }
}