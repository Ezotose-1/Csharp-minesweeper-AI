using System;
using System.Collections.Generic;
using System.Threading;

namespace Mines
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board
            {
                MineValue   = 32,
                Size        = 16,
                Plate       = new Case[16,16],
                GameEnd     = false
            };
            
            board.Plate = Board.initPlate(board);

            Board.DrawPlate(board);

            Console.WriteLine("Auto Play : (N/Y)");
            string autoplay = Console.ReadLine();
                // init list of Bot Moves
            List<Move> plays = new List<Move>();
            // Rounds loop
            while (!board.GameEnd)
            {
                bool tagged = false;
                if (autoplay.ToLower() == "false" || autoplay == "0" || autoplay.ToLower() == "n")
                {
                    Console.WriteLine("Enter a letter X :");
                    string entry = Console.ReadLine();
                    if (entry[0] == '@')
                    {
                        tagged = true;
                        entry = entry.Substring(1);
                    }
                    char letterX = Convert.ToChar(entry);
                    int posX = (int)letterX - 65;
                    Console.WriteLine("Enter a position Y :");
                    int posY = Convert.ToInt32(Console.ReadLine());

                    Move roundMove = new Move
                    {
                        posX = posX,
                        posY = posY,
                        tagged = tagged
                    };
                    Board.Play(board, roundMove);
                } else
                {
                    //Console.WriteLine(Bot.getPlays(board).Count);
                        // Every start for the bot is click on each corner
                    if (board.Rounds == 0)
                        Board.updatePlate(board, 0, 0);
                    else if (board.Rounds == 1)
                        Board.updatePlate(board, 0, board.Size-1);
                    else if (board.Rounds == 2)
                        Board.updatePlate(board, board.Size - 1, 0);
                    else if (board.Rounds == 3)
                        Board.updatePlate(board, board.Size - 1, board.Size - 1);
                    else
                    {
                        Console.WriteLine(plays.Count + " plays possible");
                        if (plays.Count == 0)
                        {
                            plays.AddRange(Bot.getPlays(board));
                            Console.WriteLine("recalculating plays");
                        }
                        board = Board.Play(board, plays[0]);
                        plays.RemoveAt(0);
                    }
                    Thread.Sleep(400);
                }
                
                    // End of round
                board.Rounds += 1;
                Console.Clear();
                Board.DrawPlate(board);
                board = Board.CheckVictory(board);
            }
            Console.ReadLine();

        }
    }
}
