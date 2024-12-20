
using System.Security.Cryptography.X509Certificates;

namespace alpha
{
    public class Board
    {
        public static void Main()
        {
            string[,] board = BoardGeneration();
            Token token1 = new Token("8",0,1);
            Token token2 = new Token("@",1,0);  
            BoardDisplay(token1,token2, board);
            
            while (true) //gameloop
            {
                Console.Clear();
                CanMove(board,token1.x,token1.y);
                BoardDisplay(token1,token2, board);
                (token1.x,token1.y)=MovePlayer(board,token1.x,token1.y);
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
            if(board[x,y]== "|" || board[x,y]== "x" || board[x,y]== "-" || board[x,y]=="8" || board[x,y]=="@")
            {
                return false;
            }
            return true;
        }
        public static void BoardDisplay(Token token1,Token token2, string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1) ; j++)
                {
                    
                    if(i == token1.x && j == token1.y)
                    {
                        System.Console.Write(token1.symbol);
                        
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
    public class Token
    {
        public string symbol;
        public int x;
        public int y;

        //albañil
        public Token(string symbol, int x, int y)
        {
            this.symbol = symbol;
            this.x = x;
            this.y = y;
        }
    }
}

