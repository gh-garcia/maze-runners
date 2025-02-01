
public class BoardGeneration
{
        public static string[,] BoardGen(int height, int width, int[]DX, int[]DY)
            {
                string[,] board = new string[height,width];
                DFS(board,1,0,height,width,DX,DY);

                return board;
            }

            public static void DFS(string[,] board, int EntranceX, int EntranceY, int height, int width, int[]DX,int[]DY)
            {
                //*start the maze blocked completely
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        board[i,j] = "█";
                    }
                }
                
                Stack<(int,int)> stack = new Stack<(int, int)>();
                stack.Push((EntranceX,EntranceY));  

                board[EntranceX,EntranceY] = " ";  //visited
                Random random = new Random();

                while (stack.Count > 0)
                {
                    (int x, int y) = stack.Pop();

                    
                    List<(int, int)> neighbors = UnvisitedNeigh(board,x,y,DX,DY);

                    if (neighbors.Count > 0)
                    {
                        (int nx, int ny) = neighbors[random.Next(neighbors.Count)];
                        RemWall(board, x, nx, y, ny);
                        board[nx, ny] = " "; // visited
                        stack.Push((x,y));
                        stack.Push((nx, ny));
                    }
                    
                }

                CarveCenterSquare(board, height, width);
            }

            public static List<(int,int)> UnvisitedNeigh(string[,] board, int x, int y, int[] DX, int[] DY)
            {
                List<(int, int)> neighbors = new List<(int, int)>();

                    int m = board.GetLength(0);
                    int n = board.GetLength(1);
                    int k = 2;

                for (int d = 0; d < DX.GetLength(0); d++)
                {

                    int newx = x + DX[d]*k;
                    int newy = y + DY[d]*k;

                    if (ValidPosition(m,n,newx,newy) && board[newx,newy] == "█")
                    {
                        neighbors.Add((newx,newy));
                    }
                }
                return neighbors;
            }

            private static bool ValidPosition(int m, int n, int x, int y)
            {   
                //* Checks if the position is valid (in bounds and inside the walls of the maze)
                return x >= 1 && x < m-1&& y >= 1 && y < n-1;   
            }

            static void RemWall(string[,] board, int x1, int x2, int y1, int y2)
            {
                int wallX = (x1 + x2) / 2;
                int wallY = (y1 + y2) / 2;

                board[wallX, wallY] = " "; //visited
            }
    
            
            public static bool CanMove(string[,] board,int x,int y)
            {
                //*Checks if the square i want to move the token to is taken by something
                if(board[x,y]== "█")
                {
                    return false;
                }
                return true;
            }
            public static void BoardDisplay(Token[] tokens, string[,] board)
            {
                //*Simple array printing, checks where the player is and instead of printing the board prints the character
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1) ; j++)
                    {
                        bool CharacterWasPrinted = false;
                        for (int m = 0; m < 2; m++)
                        {
                            if(i == tokens[m].X && j == tokens[m].Y )
                            {
                            System.Console.Write(tokens[m].Symbol);
                            CharacterWasPrinted = true;
                            }
                        }
                        if(!CharacterWasPrinted)
                        {
                            System.Console.Write(board[i,j]);
                        }
                        
                    }
                    System.Console.WriteLine();
                }
            }

            public static string[,] CarveCenterSquare(string[,] board, int height, int width)
            {
                //*Empty array that prints in the middle of the board according to its size
                for (int i = (height/2)-3; i < (height/2)+2; i++)
                {
                    for (int j = (width/2)-3; j < (width/2)+4; j++)
                    {
                        board[i,j]= ":";
                    }
                }

                return board;
            } 
            public static void PlaceTraps(int trapcount, List<Trap> traps, string[,] board)
            {
                
                Random rand = new Random();

                for (int i = 0; i < trapcount; i++)
                {
                    int x = rand.Next(1, board.GetLength(0)-1);
                    int y = rand.Next(1, board.GetLength(1)-1);

                    if(board[x,y] == " ")
                    {
                        string TrapType = GetRandomTrap(rand);
                        Trap newTrap = new Trap("X", TrapType, x, y);

                        traps.Add(newTrap);
                        board[x, y] = newTrap.symbol;

                    }
                    else
                    {
                        i--;
                    }
                }
            }
            public static string GetRandomTrap(Random rand)
            {
                string[] TrapTypes = {"dart", "sand", "wizard"};
                int index = rand.Next(0, TrapTypes.Length);
                return TrapTypes[index];
            }       
}