using System;

namespace Mines
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board
            {
                MineCount   = 32,
                Size        = 16,
                Plate       = new Case[16,16],
                GameEnd     = false
            };
            
            board.Plate = Board.initPlate(board);

            Board.DrawPlate(board);

                // Rounds loop
            while (!board.GameEnd)
            {
                Console.WriteLine("Enter a letter X :");
                char letterX = Convert.ToChar(Console.ReadLine());
                int posX = (int) letterX - 65;
                Console.WriteLine("Enter a position Y :");
                int posY = Convert.ToInt32(Console.ReadLine());
                board = Board.updatePlate(board, posX, posY);
                Console.Clear();
                Board.DrawPlate(board);
                board = Board.CheckVictory(board);
            }

        }
    }
}
