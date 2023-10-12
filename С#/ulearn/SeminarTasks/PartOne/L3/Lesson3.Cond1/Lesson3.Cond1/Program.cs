using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

/*Дана начальная и конечная клетки на шахматной доске. 
 * Корректный ли это ход на пустой доске для: слона, коня, ладьи, ферзя, короля?
*/


namespace Lesson3.Cond1
{
    internal class Program
    {
        enum ChessMen
        {
            King,     //Король
            Queen,    //Ферзь
            Bishop,   //Слон
            Knight,   //Конь
            Castle    //Ладья
        }
        
        static void Main(string[] args)
        {
            var chess = ChessMen.King;
            
            string from = "a1";
            string to = "a2";

            if (IsCorrectMove(from, to, chess)) Console.WriteLine("YES");
                
            else Console.WriteLine("NO");
        }


        static bool IsCorrectMove(string from, string to, ChessMen chess)
        {
            int dx = Math.Abs(to[0] - from[0]);
            int dy = Math.Abs(to[1] - from[1]);

            if (from == to)
                return false;

            if (chess == ChessMen.King)
                return dx <= 1 && dy <= 1;

            if (chess == ChessMen.Queen)
                return dx == dy || dx == 0 || dy == 0;

            if (chess == ChessMen.Bishop)
                return dx == dy;

            if (chess == ChessMen.Knight)
                return dx == 2 && dy == 1 || dx == 1 && dy == 2;

            if (chess == ChessMen.Castle) 
                return dx == 0 || dy == 0;

            return false;
        }

        /*
        static bool KingMove(int dx, int dy)
        {
            return dx <= 1 && dy <= 1;
        }

        static bool QueenMove(int dx, int dy)
        {
            return dx == dy || dx == 0 || dy == 0;
        }

        static bool BishopMove(int dx, int dy)
        {
            return dx == dy;
        }

        static bool KnightMove(int dx, int dy)
        {
            return dx == 2 && dy == 1 || dx == 1 && dy == 2;
        }

        static bool CastleMove(int dx, int dy)
        {
            return dx == 0 || dy == 0;
        */

        
    }
}
