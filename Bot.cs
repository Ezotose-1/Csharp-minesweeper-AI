using System;
using System.Collections.Generic;
using System.Text;

namespace Mines
{
    class Bot
    {
                // Return all case around that are not already clicked and not tagged as a bomb
        public static List<(int,int)> PossiblePlay(Board board, int x, int y)
        {
            List<(int, int)> Cases = new List<(int, int)>();
            Case[,] Plate = board.Plate;
            // direct
            if (x > 0 && !Plate[x-1, y].Exposed && !Plate[x - 1, y].IsTagged)
                Cases.Add((x - 1, y));
            if (y > 0 && !Plate[x, y - 1].Exposed && !Plate[x, y - 1].IsTagged)
                Cases.Add((x, y - 1));
            if (x < board.Size - 1 && !Plate[x + 1, y].Exposed && !Plate[x + 1, y].IsTagged)
                Cases.Add((x + 1, y));
            if (y < board.Size - 1 && !Plate[x, y + 1].Exposed && !Plate[x, y + 1].IsTagged)
                Cases.Add((x, y + 1));
            // angles
            if (x > 0 && y > 0 && !Plate[x - 1, y - 1].Exposed && !Plate[x - 1, y - 1].IsTagged)
                Cases.Add((x - 1, y - 1));
            if (x < board.Size - 1 && y < board.Size - 1 && !Plate[x + 1, y + 1].Exposed && !Plate[x + 1, y + 1].IsTagged)
                Cases.Add((x + 1, y + 1));
            if (x > 0 && y < board.Size - 1 && !Plate[x - 1, y + 1].Exposed && !Plate[x - 1, y + 1].IsTagged)
                Cases.Add((x - 1, y + 1));
            if (x < board.Size - 1 && y > 0 && !Plate[x + 1, y - 1].Exposed && !Plate[x + 1, y - 1].IsTagged)
                Cases.Add((x + 1, y - 1));
            return Cases;
        }

                // Return a list of all tagged bomb around the position
        public static List<(int, int)> CountBombExposed(Board board, int x, int y)
        {
            List<(int, int)> Cases = new List<(int, int)>();
            Case[,] Plate = board.Plate;
            // direct
            if (x > 0 && Plate[x - 1, y].IsTagged)
                Cases.Add((x - 1, y));
            if (y > 0 && Plate[x, y - 1].IsTagged)
                Cases.Add((x, y - 1));
            if (x < board.Size - 1 && Plate[x + 1, y].IsTagged)
                Cases.Add((x + 1, y));
            if (y < board.Size - 1 && Plate[x, y + 1].IsTagged)
                Cases.Add((x, y + 1));
            // angles
            if (x > 0 && y > 0 && Plate[x - 1, y - 1].IsTagged)
                Cases.Add((x - 1, y - 1));
            if (x < board.Size - 1 && y < board.Size - 1 && Plate[x + 1, y + 1].IsTagged)
                Cases.Add((x + 1, y + 1));
            if (x > 0 && y < board.Size - 1 && Plate[x - 1, y + 1].IsTagged)
                Cases.Add((x - 1, y + 1));
            if (x < board.Size - 1 && y > 0 && Plate[x + 1, y - 1].IsTagged)
                Cases.Add((x + 1, y - 1));
            return Cases;
        }

                // In case the bot is stuck : it play randomly
        public static (int, int) randomPlay(Board board)
        {
            int posX = new Random().Next(0, board.Size - 1);
            int posY = new Random().Next(0, board.Size - 1);
            if (board.Plate[posX, posY].Exposed)
                return randomPlay(board);
            return (posX, posY);
        }


                // Play a round and return the new plate
        public static Board getPlays (Board board)
        {
            Case[,] Plate = board.Plate;
            for (int y = 0; y < board.Size; y++)
            {
                for (int x = 0; x < board.Size; x++)
                {
                        // Select a good case for a move
                    if (Plate[x, y].Exposed && Plate[x, y].Value != 0)
                    {
                        List<(int, int)> possiblePlay = PossiblePlay(board, x, y);
                        List<(int, int)> bombAround = CountBombExposed(board, x, y);
                            // If all bomb around have been found : CLICK
                        if (Plate[x, y].Exposed && bombAround.Count >= Plate[x, y].Value && possiblePlay.Count > 0) {
                            int xPlay = possiblePlay[0].Item1;
                            int yPlay = possiblePlay[0].Item2;
                            Console.WriteLine(xPlay + " " + yPlay);
                            board  = Board.updatePlate(board, xPlay, yPlay);
                            return board;
                        }

                            // If there is obviously all case around that are bombs : tagged them
                        if (bombAround.Count < Plate[x, y].Value && possiblePlay.Count == Plate[x, y].Value - bombAround.Count)
                        {
                            int xPlay = possiblePlay[0].Item1;
                            int yPlay = possiblePlay[0].Item2;
                            board.Plate[xPlay, yPlay].IsTagged = !board.Plate[xPlay, yPlay].IsTagged;
                            board.MineCount += 1;
                            return board;
                        }
                    }
                }
            }
                // Here it play randomly if it didn't have found case to move before
            var rndPlay = randomPlay(board);
            int rndX = rndPlay.Item1;
            int rndY = rndPlay.Item2;
            board = Board.updatePlate(board, rndX, rndY);
            Console.WriteLine("I'm stuck, random play.");
            Console.ReadLine();
            return board;
        }
    }
}
