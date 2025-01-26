﻿namespace alpha
{
    class Program
    {
        public static void Main()
        {
            Board board = new Board();
            board.StartGame();
        }
    }
    public class Board
    {
        Token CurrentToken;
        string[,] board;
        int height = 25;
        int width = 50;
        int dado = 0;
        bool IsPlayer2Turn = true;
        private Random random = new Random();
        List<Trap> traps = new List<Trap>();
        int trapcount = 8;

        //array de direcciones
        int[] DX = {0,-1,0,1};
        int[] DY = {1,0,-1,0};

        public void StartGame()
        {
            Token[] tokens = new Token [2];

            Token token1 = new Token("V",(height/2),(width/2),"VI",0,5);
            Token token2 = new Token("X",(height/2)-1,(width/2),"Caitlyn",0,4);
            Token token3 = new Token("ث",(height/2)-2,(width/2),"Jayce",0,5);
            Token token4 = new Token("Ж",(height/2)-3,(width/2),"Viktor",0,3);
            Token token5 = new Token("ώ",(height/2)+1,(width/2),"Jinx",0,4);


            ShowWelcomeScreen();
            //*debe haber una manera mejor de hacer esto
            TokenSelection(tokens,token1,token2,token3,token4,token5);
            CurrentToken = tokens[1];

            board = BoardGeneration();
            PlaceTraps(trapcount, traps);
            
            BoardDisplay(tokens, board);
            while (true) //*gameloop
            {
                Console.Clear();
                BoardDisplay(tokens, board);
                System.Console.WriteLine(ShowUI());
                
                TurnManagement(tokens,random);    
                
            }
        }
        public static void ShowWelcomeScreen()
        {

        Console.WriteLine(asciiArt.WelcomeScreen);

        var enterpressed = Console.ReadKey(true);
        Console.Clear();

        }

        public string ShowUI()
        {
            string UI = $@"
+------------------------------------------------+
            Player Turn:{CurrentToken.name}         
+------------------------------------------------+
|                                                |
|  Moves left:{dado}                                  |
|                                                |
|  > Health:{CurrentToken.health}                                    |
|  > Habilities:                                 |
|                                                |
|                                                |
|  :)                                            |
|                                                |
+------------------------------------------------+
            ";
            return UI;
        }

        public void TurnManagement(Token[] tokens, Random random)
        {
            if(dado > 0)
            {
                int newx;
                int newy;

                (newx,newy)= MoveToken(board,CurrentToken.x,CurrentToken.y);

                if(CanMove(board,newx,newy))
                {
                    CurrentToken.x = newx;
                    CurrentToken.y = newy;
                    CurrentToken.movecount++;
                    dado--;
                    TriggerTrapPosition(newx,newy,CurrentToken);

                    
                }
            }

            if(dado == 0)
            {

                CurrentToken.movecount = 0;

                if(IsPlayer2Turn == false)
                {
                    CurrentToken = tokens[0];
                    IsPlayer2Turn = true;
                }

                else
                {
                    CurrentToken = tokens[1];
                    IsPlayer2Turn = false;
                }
                dado = DiceThrow(0,random);
          }

          
          
            
        }

        public int DiceThrow(int dado, Random random)
        {
            


                System.Console.WriteLine("Press R to roll the dice");
            
                var input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.R)
                {   
                    dado = random.Next(1,7);
                }
                else 
                {
                    System.Console.WriteLine($" {dado} Wrong key, please press R to roll...");
                }
            return dado;
            }

        

        public static void TokenSelection(Token[] tokens,Token token1,Token token2,Token token3,Token token4,Token token5)
        {


            for (int i = 0; i <= 1; i++)
            {

            System.Console.WriteLine(asciiArt.CharacterSelectionArt);
           var keyInfo = Console.ReadKey(true);

           if (keyInfo.Key == ConsoleKey.D1)
            {   
                tokens[i] = token1;
            }
            else if (keyInfo.Key == ConsoleKey.D2)
            {   
                tokens[i] = token2;
            }
            else if (keyInfo.Key == ConsoleKey.D3)
            {   
                 tokens[i] = token3;
            }
            else if (keyInfo.Key == ConsoleKey.D4)
            {   
                 tokens[i] = token4;
            }
            else if (keyInfo.Key == ConsoleKey.D5)
            {   
                tokens[i] = token5;
            }
            else 
            {
                throw new ArgumentException("esa tecla no porfavo");
            
            }
              System.Console.WriteLine("Confirm selection: ENTER");  
              var enterpressed = Console.ReadKey(true);

            }
        }
        

        public string[,] BoardGeneration()
        {
            

            string[,] board = new string[height,width];
            DFS(board,1,0,height,width);

            return board;
        }

            public void DFS(string[,] board, int EntranceX, int EntranceY, int height, int width)
            {
                //ponerlo todo bloqueado
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        board[i,j] = "█";
                    }
                }

                Stack<(int,int)> stack = new Stack<(int, int)>();
                

                stack.Push((EntranceX,EntranceY));
                

                board[EntranceX,EntranceY] = " ";  //visitadas
                Random random = new Random();

                while (stack.Count > 0)
                {
                    (int x, int y) = stack.Pop();

                    
                    List<(int, int)> neighbors = UnvisitedNeigh(board,x,y);

                    if (neighbors.Count > 0)
                    {
                        (int nx, int ny) = neighbors[random.Next(neighbors.Count)];
                        RemWall(board, x, nx, y, ny);
                        board[nx, ny] = " "; // visitada
                        stack.Push((x,y));
                        stack.Push((nx, ny));
                    
                        // foreach (var neigh in neighbors)
                        // {
                        //     stack.Push(neigh);
                        // }

                    }
                    
                }

                CarveCenterSquare(board, height, width);
            }

            List<(int,int)> UnvisitedNeigh(string[,] board, int x, int y)
            {
                List<(int, int)> neighbors = new List<(int, int)>();

                    int m = board.GetLength(0);
                    int n = board.GetLength(1);
                    int k = 2;

                for (int d = 0; d < this.DX.GetLength(0); d++)
                {

                    int newx = x + this.DX[d]*k;
                    int newy = y + this.DY[d]*k;

                    if (ValidPosition(m,n,newx,newy) && board[newx,newy] == "█")
                    {
                        neighbors.Add((newx,newy));
                    }
                }
                return neighbors;
            }

            private static bool ValidPosition(int m, int n, int x, int y)
            {
                return x >= 1 && x < m-1&& y >= 1 && y < n-1;   
            }

            static void RemWall(string[,] board, int x1, int x2, int y1, int y2)
            {
                // if (x1 < 0 || x1 >= board.GetLength(0) || y1 < 0 || y1 >= board.GetLength(1) || x2 < 0 || x2 >= board.GetLength(0) || y2 < 0 || y2 >= board.GetLength(1))
                // {
                //     return;  //cosa fuera de limites
                // }
                int wallX = (x1 + x2) / 2;
                int wallY = (y1 + y2) / 2;

                // if (wallX < 0 || wallX >= board.GetLength(0) || wallY < 0 || wallY >= board.GetLength(1))
                // {
                //     return; 
                // }
                board[wallX, wallY] = " "; 
            }


            public static (int x,int y) MoveToken(string[,] board,int x,int y)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {   
                    x--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {   
                    x++;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {   
                    y--;  
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {   
                    y++; 
                }
                return(x,y);
                
            }
            
            public static bool CanMove(string[,] board,int x,int y)
            {
                //*checks if the square i want to move the token to is taken by something
                if(board[x,y]== "║" || board[x,y]== "x" || board[x,y]== "═" || board[x,y]=="8")
                {
                    return false;
                }
                return true;
            }
            public static void BoardDisplay(Token[] tokens, string[,] board)
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1) ; j++)
                    {
                        bool CharacterWasPrinted = false;
                        for (int m = 0; m < 2; m++)
                        {
                            if(i == tokens[m].x && j == tokens[m].y )
                            {
                            System.Console.Write(tokens[m].symbol);
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
                
                for (int i = (height/2)-3; i < (height/2)+2; i++)
                {
                    for (int j = (width/2)-3; j < (width/2)+4; j++)
                    {
                        board[i,j]= ":";
                    }
                }

                return board;
            }

        public void PlaceTraps(int trapcount, List<Trap> traps)
        {
            Random rand = new Random();

            for (int i = 0; i < trapcount; i++)
            {
                int x = random.Next(1, board.GetLength(0)-1);
                int y = random.Next(1, board.GetLength(1)-1);

                if(board[x,y] == " ")
                {
                    string TrapType = GetRandomTrap();
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

        public void TriggerTrapPosition(int x, int y, Token CurrentToken)
        {
            foreach (var trap in traps)
            {
                if (trap.X == x && trap.Y == y)
                {
                    trap.Trigger(CurrentToken);
                }
            }
        }

        public string GetRandomTrap()
        {
            string[] TrapTypes = {"dart", "sand", "fog"};
            int index = random.Next(0, TrapTypes.Length);
            return TrapTypes[index];
        }
    }
    public class Token
    {
        public string symbol;
        public int x;
        public int y;
        public string name;
        public int movecount;
        public int health;

        //albañil
        public Token(string symbol, int x, int y, string name, int movecount, int health)
        {
            this.symbol = symbol;
            this.x = x; 
            this.y = y;
            this.name = name;
            this.movecount = movecount;
            this.health = health;
        }
    }

    public class Trap
    {
        public string symbol;
        public string type;
        public int X;
        public int Y;


        //constructor de Trap
        public Trap(string symbol, string type, int X, int Y)
        {
            this.type = type;
            this.X = X;
            this.Y = Y;

            if (type == "dart")
            {
                this.symbol = "D";
            }
            else if (type == "sand")
            {
                this.symbol = "S";
            }
            else if(type == "fog")
            {
                this.symbol = "F";
            }
            else
            {
                this.symbol = "X";
            }
        }

        public void Trigger(Token CurrentToken)
        {
            if (this.type == "dart")
            {
                System.Console.WriteLine("You've stepped on a plate and a dart shot out a wall. Your HP is reduced by 1");
                CurrentToken.health -= 1;
            }
            else if (this.type == "sand")
            {
                System.Console.WriteLine("You were swallowed by moving sand, the rest of your turn is gone");
                CurrentToken.movecount = 0;
            }
            else if (this.type == "fog")
            {
                System.Console.WriteLine("A thick fog surrounds you, you can't see");
                //!AQUI IRIA LO QUE HAY QUE HACER SI SUPIERA COMO HACERLO POR AHORA ES:
                //* TriggerFogTrap();
            }
        }
    }



    public static class asciiArt
    {// es static para que cuando vaya para alla arriba no tener que hacer NEW ASCIIART etc... funciona sin tener que crear instancias, un almacen de cosas
    //despues descubri que lo puedo poner en txt pero es que maze runners aqui me gusta XD
            public static string WelcomeScreen = @"
                                                            ███╗   ███╗ █████╗ ███████╗███████╗                          
                                                            ████╗ ████║██╔══██╗╚══███╔╝██╔════╝                          
                                                            ██╔████╔██║███████║  ███╔╝ █████╗                            
                                                            ██║╚██╔╝██║██╔══██║ ███╔╝  ██╔══╝                            
                                                            ██║ ╚═╝ ██║██║  ██║███████╗███████╗                          
                                                            ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝                          
                                                            ██████╗ ██╗   ██╗███╗   ██╗███╗   ██╗███████╗██████╗ ███████╗
                                                            ██╔══██╗██║   ██║████╗  ██║████╗  ██║██╔════╝██╔══██╗██╔════╝
                                                            ██████╔╝██║   ██║██╔██╗ ██║██╔██╗ ██║█████╗  ██████╔╝███████╗
                                                            ██╔══██╗██║   ██║██║╚██╗██║██║╚██╗██║██╔══╝  ██╔══██╗╚════██║
                                                            ██║  ██║╚██████╔╝██║ ╚████║██║ ╚████║███████╗██║  ██║███████║
                                                            ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝╚══════╝
                                                            ---------------------------ARCANE EDITION-------------------------------

                                                            Press any key to continue...
                                                            ";

            public static string CharacterSelectionArt = File.ReadAllText("CharacterArt.txt");
            
            
            
    }

}

