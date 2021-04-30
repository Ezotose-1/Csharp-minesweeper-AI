using System;

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

            // Rounds loop
            while (!board.GameEnd)
            {
                bool tagged = false;
                Console.WriteLine("Auto Play the round : (N/[Any key])");
                string autoplay = Console.ReadLine();
                if (autoplay.ToLower() == "false" || autoplay == "0" || autoplay.ToLower() == "n")
                {
                    Console.WriteLine("Enter a letter X :");
                    string entry = Console.ReadLine();
                    if (entry[0] == '@')
                    {
                        board.MineCount += 1;
                        tagged = true;
                        entry = entry.Substring(1);
                    }
                    char letterX = Convert.ToChar(entry);
                    int posX = (int)letterX - 65;
                    Console.WriteLine("Enter a position Y :");
                    int posY = Convert.ToInt32(Console.ReadLine());
                    if (tagged)
                    {
                        board.Plate[posX, posY].IsTagged = !board.Plate[posX, posY].IsTagged;
                    }
                    else
                    {
                        board = Board.updatePlate(board, posX, posY);
                    }
                } else
                {
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
                        board = Bot.getPlays(board);
                   }
                }
                
                    // End of round
                board.Rounds += 1;
                Console.Clear();
                Board.DrawPlate(board);
                board = Board.CheckVictory(board);
            }

        }
    }
}
