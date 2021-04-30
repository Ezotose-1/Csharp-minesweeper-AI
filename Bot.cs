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


                // Returns all possible move that IA can play
        public static List<Move> getPlays (Board board)
        {
            List<Move> moves = new List<Move>();
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
                            
                            foreach (var p in possiblePlay)
                            {
                                if (moves.Exists(m => m.posX == p.Item1 && m.posY == p.Item2))
                                    continue;
                                moves.Add(new Move
                                {
                                    posX = p.Item1,
                                    posY = p.Item2,
                                    tagged = false
                                });
                            }
                        }

                            // If there is obviously all case around that are bombs : tagged them
                        if (bombAround.Count < Plate[x, y].Value && possiblePlay.Count == Plate[x, y].Value - bombAround.Count)
                        {
                            
                            foreach (var p in possiblePlay)
                            {
                                if (moves.Exists(m => m.posX == p.Item1 && m.posY == p.Item2))
                                    continue;
                                moves.Add(new Move
                                {
                                    posX = p.Item1,
                                    posY = p.Item2,
                                    tagged = true
                                });
                            }
                        }
                    }
                }
            }
                // Here it play randomly if it didn't have found case to move before
            if (moves.Count == 0)
            {
                var rndPlay = randomPlay(board);
                moves.Add(new Move
                {
                    posX = rndPlay.Item1,
                    posY = rndPlay.Item2,
                    tagged = false
                });
                Console.WriteLine("I'm stuck, random play.");
                Console.ReadLine();
            }
            return moves;
        }
    }
}
