
using System.Security.Cryptography.X509Certificates;

namespace alpha
{
    public class Board
    {
        public static void Main()
        {
            string[,] board = BoardGeneration();
            string player1 = "8";
            int x = 0;
            int y = 1;
            BoardDisplay(player1, x, y, board);
            
            while (true) //gameloop
            {
                Console.Clear();
                CanMove(board,x,y);
                BoardDisplay(player1, x, y, board);
                (x,y)=MovePlayer(board,x,y);
            }
        }

        public static string[,] BoardGeneration()
        {
            string[,] board = new string[10,10]
            {
                {"+"," ","-","-","-","-","-","-","-","+"},
                {"|"," ","|"," "," "," "," "," "," ","|"},
                {"|"," ","|"," "," "," "," "," "," ","|"},
                {"|"," ","|"," "," "," "," "," "," ","|"},
                {"|"," ","|"," "," "," "," "," "," ","|"},
                {"|"," ","|"," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|","x"," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"+","-","-","-","-","-","-","-"," ","+"}
            };

            return board;
        }


        public static (int x,int y) MovePlayer(string[,] board,int x,int y)
        {
            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {   
                if(CanMove(board,x-1,y))
                {
                x--;
                }
                
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {   
                if(CanMove(board,x+1,y))
                {
                x++;
                }
                
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
            {   
                if(CanMove(board,x,y-1))
                {
                y--;
                }
                
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {   
                if(CanMove(board,x,y+1))
                {
                y++;
                }
                
            }
            return(x,y);
        }
        
        public static bool CanMove(string[,] board,int x,int y)
        {
            //*checks if the square i want to move the token to is taken by something (| x)
            if(board[x,y]== "|" || board[x,y]== "x" || board[x,y]== "-")
            {
                return false;
            }
            return true;
        }
        public static void BoardDisplay(string player1, int x, int y, string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1) ; j++)
                {
                    
                    if(i == x && j == y)
                    {
                        System.Console.Write(player1);
                    }
                    else
                    {
                    System.Console.Write(board[i,j]);
                    }
                    
                }
                System.Console.WriteLine();
            }
        }
    }
}

