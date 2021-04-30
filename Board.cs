using System;
using System.Collections.Generic;
using System.Text;

namespace Mines
{
    class Board
    {
        public Case[,] Plate { get; set; }
        public int Size { get; set; }
        public int MineCount { get; set; }
        public int MineValue { get; set; }
        public bool GameEnd { get; set; }
        public int Rounds { get; set; }


        // Return free position for mine
        static (int, int) generateMine(Board board)
        {
            int posX = new Random().Next(0, board.Size - 1);
            int posY = new Random().Next(0, board.Size - 1);
            if (board.Plate[posX, posY].IsMine)
                return generateMine(board);
            return (posX, posY);
        }


                // Initialise the base Plate of the board
        public static Case[,] initPlate(Board board)
        {
                // Create plate
            for (int y = 0; y < board.Size; y++)
            {
                for (int x = 0; x < board.Size; x++)
                {
                    board.Plate[x, y] = new Case
                    {
                        PosX = x,
                        PosY = y,
                        Exposed = false,
                        IsMine = false,
                        Value = 0,
                    };
                }
            }
                // Generate mines
            for (int k = 0; k < board.MineValue; k++)
            {
                (int, int) minePos = generateMine(board);
                int posX = minePos.Item1;
                int posY = minePos.Item2;
                board.Plate[posX, posY].IsMine = true;
                board.Plate[posX, posY].Value = -1;
                board.Plate[posX, posY].Exposed = false;
                foreach (var pos in caseAvailable(board, posX, posY))
                {
                    Case c = board.Plate[pos.Item1, pos.Item2];
                    c.Value = (c.Value == -1) ? -1 : c.Value + 1;
                }
            }
            return board.Plate;

        }
        
            // Draw the Plate
        public static void DrawPlate(Board board)
        {
            Console.Write("   ");
            for (int i = 0; i < board.Size; i++)
            {
                Console.Write(" | "+ (char) (65+i) );
            }
            Console.Write(" |\n");
            for (int y = 0; y < board.Size; y++)
            {
                Console.Write(y + " ");
                if (y < 10)
                {
                    Console.Write(" ");
                }
                for (int x = 0; x < board.Size; x++)
                {
                    if (board.Plate[x, y].Exposed && board.Plate[x, y].Value == 0)
                        Console.Write(" | -");
                    else if (board.Plate[x, y].Exposed && !board.Plate[x, y].IsMine)
                        Console.Write(" | " + board.Plate[x,y].Value);
                    else if (board.Plate[x, y].IsMine && board.GameEnd)
                        Console.Write(" | *");
                    else if (board.Plate[x, y].IsTagged)
                        Console.Write(" | @");
                    else
                        Console.Write(" |  ");
                }
                Console.Write(" |");
                if (y == board.Size / 2-2)
                    Console.Write("      Mines : " + board.MineCount + "/" + board.MineValue);
                if (y == board.Size / 2 -1)
                    Console.Write("      Round : " + board.Rounds);
                Console.Write("\n");
            }
        }

                // List all case available arount a position x y
        public static List<(int, int)> caseAvailable(Board board, int x, int y)
        {
            List<(int, int)> caseAv = new List<(int, int)>();
                // direct
            if (x > 0)
                caseAv.Add( (x - 1, y ) );
            if (y > 0)
                caseAv.Add((x, y - 1));
            if (x < board.Size - 1)
                caseAv.Add((x + 1, y));
            if (y < board.Size - 1)
                caseAv.Add((x, y + 1));
                // angles
            if (x > 0 && y > 0)
                caseAv.Add((x-1, y - 1));
            if (x < board.Size - 1  && y < board.Size - 1)
                caseAv.Add((x + 1, y + 1));
            if (x > 0 && y < board.Size - 1)
                caseAv.Add((x - 1, y + 1));
            if (x < board.Size - 1 && y > 0)
                caseAv.Add((x + 1, y - 1));

            return caseAv;
        }

                // Click on a case and update it position
        public static Board updatePlate(Board board, int x, int y)
        {
            Case c = board.Plate[x, y];
            if (c.IsMine)
                board.GameEnd = true;
            if (c.Exposed)
                return board;
            c.Exposed = true;
            if (c.Value == 0)
            {
                foreach (var pos in caseAvailable(board, x, y))
                {
                    board = updatePlate(board, pos.Item1, pos.Item2);
                }
            }
            return board;
        }


                // Check if the game is finished by victory.
        public static Board CheckVictory(Board board)
        {
            int countClear = 0;
            for (int y = 0; y < board.Size; y++)
            {
                for (int x = 0; x < board.Size; x++)
                {
                    countClear = (board.Plate[x, y].Exposed) ? countClear : countClear + 1;
                }
            }
            board.GameEnd = board.GameEnd || (countClear == board.MineValue);
            return board;
        }
    }

}
