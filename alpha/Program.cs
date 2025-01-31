namespace alpha
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
        int trapcount = 30;
        string LastTrapMessage = "";

        
        int[] DX = {0,-1,0,1};
        int[] DY = {1,0,-1,0};

        public void StartGame()
        {
            Token[] tokens = new Token [2];

            //!I used initials for the skills because different legths break the UI
            Token token1 = new Token("V",1,2,"VI",0,5,0,"LC"); //? there is a 1 in 1000 chance to be teleported to the exit
            Token token2 = new Token("X",(height/2)-1,(width/2),"Caitlyn",0,4,0,"MC"); //? there is a 50% chance that if health reaches 0 a health point is granted
            Token token3 = new Token("ث",(height/2)-2,(width/2),"Jayce",0,5,0,"HL"); //? gains +1 health every 5 turns
            Token token4 = new Token("Ж",(height/2)-3,(width/2),"Viktor",0,4,0,"SP"); //? gains +1 to dicethrow every 5 turns
            Token token5 = new Token("ώ",(height/2)+1,(width/2),"Jinx",0,4,0,"TD"); //? can disarm all traps


            ShowWelcomeScreen();
                              //?should be a better way to do this
            TokenSelection(tokens,token1,token2,token3,token4,token5);
            CurrentToken = tokens[0];

            board = BoardGeneration();
            PlaceTraps(trapcount, traps);
            
            BoardDisplay(tokens, board);

            while (!WinCondition(1,0)) //*gameloop
            {
                Console.Clear();
                BoardDisplay(tokens, board);
                System.Console.WriteLine(ShowUI()); 
                
                TurnManagement(tokens,random);
                   
                
            }
            Console.Clear();
            System.Console.WriteLine(asciiArt.YOUWON);

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
|  Moves left:{CurrentToken.movecount}                                  |
|                                                |
|  > Health:{CurrentToken.health}                                    |
|  > Deathcount:{CurrentToken.deathcount}                                |
|  > Skill:{CurrentToken.Skill}                                    |
|  >{LastTrapMessage}                                             |
|                                                |
|                                                |
+------------------------------------------------+
            ";
            return UI;
        }


        public void TurnManagement(Token[] tokens, Random random)
        {
            CurrentToken.DoSkill();

            if(CurrentToken.movecount > 0)
            {
                int newx;
                int newy;

                (newx,newy)= MoveToken(board,CurrentToken.x,CurrentToken.y);

                if(CanMove(board,newx,newy))
                {
                    CurrentToken.x = newx;
                    CurrentToken.y = newy;
                    CurrentToken.movecount--;
                    HandlePlayerDeath(CurrentToken, height, width);
                    TriggerTrapPosition(newx,newy,CurrentToken);  
                }
                
            }
            
            else
            {
                CurrentToken.movecount = 0;
                CurrentToken.Cool--;

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
                CurrentToken.movecount = DiceThrow(0,random);
                LastTrapMessage = "";
          }       
        }

        public int DiceThrow(int dado, Random random)
        {

            System.Console.WriteLine("Press R to roll the dice");
            var input = Console.ReadKey(true);

            while(input.Key != ConsoleKey.R)
            {   
                input = Console.ReadKey(true);
                System.Console.WriteLine("Wrong key, please press R to roll...");
            }
                
            dado = random.Next(1,7);
            return dado;
        }

        //!FIX
        public bool WinCondition(int EntranceX, int EntranceY)
        {
            //*Checks if player reached the entrance/exit of the maze
            if(CurrentToken.x == 1 && CurrentToken.y == 0)
            {
                return true;
            }
            return false;
        }
        

        public static void TokenSelection(Token[] tokens,Token token1,Token token2,Token token3,Token token4,Token token5)
        {
            //*Handles input for choosing a player, stores the selection in an 2 space array
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

                    
                    List<(int, int)> neighbors = UnvisitedNeigh(board,x,y);

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
                //* Checks if the position is valid (in bounds and inside the walls of the maze)
                return x >= 1 && x < m-1&& y >= 1 && y < n-1;   
            }

            static void RemWall(string[,] board, int x1, int x2, int y1, int y2)
            {
                int wallX = (x1 + x2) / 2;
                int wallY = (y1 + y2) / 2;

                board[wallX, wallY] = " "; //visited
            }


            public static (int x,int y) MoveToken(string[,] board,int x,int y)
            {
                //?I could change this and use a direction array but i did this when i still didn't know about it 

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

            public static void HandlePlayerDeath(Token CurrentToken, int height, int width)
            {
                //* Checks if player health is 0, changes the player position back to the start center square and sets their health according to each token
                if(CurrentToken.health == 0)
                {
                    CurrentToken.x = height/2;
                    CurrentToken.y = width/2;
                    CurrentToken.deathcount +=1;
                    if (CurrentToken.name == "Caitlyn" || CurrentToken.name == "Jinx" || CurrentToken.name == "Viktor" )
                    {
                        CurrentToken.health = 4;
                    }
                    else
                    {
                        CurrentToken.health = 5;
                    }
                    System.Console.WriteLine(asciiArt.deathmessage);

                }
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
                        LastTrapMessage = trap.Trigger(CurrentToken);
                    }
                }
            }

            public string GetRandomTrap()
            {
                string[] TrapTypes = {"dart", "sand", "wizard"};
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
        public int deathcount;
        public string Skill;

        public int Cool = 1;

        public Token(string symbol, int x, int y, string name, int movecount, int health, int deathcount, string Skill)
        {
            this.symbol = symbol;
            this.x = x; 
            this.y = y;
            this.name = name;
            this.movecount = movecount;
            this.health = health;
            this.deathcount = deathcount;
            this.Skill = Skill;
        }

        public void DoSkill()
        {
            //*Trap Disarm is implemented over at Trigger()

            if (Skill == "LC")
            {
                Random random = new();
                if (random.Next(1,1001) == 1)
                {
                    x = 2;
                    y = 0;
                    System.Console.WriteLine($"WOWOWOWOW IT'S A MIRACLE YOU SOMEHOW MADE IT NEXT TO THE EXIT,{name} you are free to go");
                }
            }
            else if (Skill == "MC" && health == 0)
            {
                Random random = new Random();
                if (random.Next(1,101) <= 50)
                {
                health = 1;
                System.Console.WriteLine($"You somehow keep getting your way, {name}, your life has been spared once again");  
                }
            }
            else if (Skill == "HL")
            {
                if (Cool == 0)
                {
                    health++;
                    Cool = 5;
                    System.Console.WriteLine($"You healed yourself, {name} lives another day");
                }
            }
            else if (Skill == "SP")
            {
                if(Cool == 0)
                {
                    movecount++;
                    Cool = 5;
                    System.Console.WriteLine($"{name} used Sprint, +1 has been added to your dicethrow");
                }
            }

        }
    }

    public class Trap
    {
        public string symbol;
        public string type;
        public int X;
        public int Y;

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
            else if(type == "wizard")
            {
                this.symbol = "W";
            }
            else
            {
                this.symbol = "X";
            }
        }

        public string Trigger(Token CurrentToken)
        {
            string message ="";
            if (CurrentToken.Skill == "TD")
            {
                System.Console.WriteLine("You disarmed the trap");
            }
            else
            {
                if (this.type == "dart")
                {
                    message = "You've stepped on a plate and a dart shot out a wall. Your HP is reduced by 1";
                    CurrentToken.health -= 1;
                }
                else if (this.type == "sand")
                {
                    message = "You were swallowed by moving sand, the rest of your turn is gone";
                    CurrentToken.movecount = 0;
                }
                else if (this.type == "wizard")
                {
                    message = "A wizard appeared, took a look and didn't like you, he waved around is staff, you have now traded positions with the other player";
                    //!AQUI IRIA LO QUE HAY QUE HACER SI SUPIERA COMO HACERLO POR AHORA ES:
                    //* TriggerWizardTrap();
                }     
                
            }
            return message;
        }
    }



    public static class asciiArt
    {
        //*Vault with the asciiart i use 
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

            public static string YOUWON = @"

▓██   ██▓ ▒█████   █    ██     █     █░ ▒█████   ███▄    █ 
 ▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▓█░ █ ░█░▒██▒  ██▒ ██ ▀█   █ 
  ▒██ ██░▒██░  ██▒▓██  ▒██░   ▒█░ █ ░█ ▒██░  ██▒▓██  ▀█ ██▒
  ░ ▐██▓░▒██   ██░▓▓█  ░██░   ░█░ █ ░█ ▒██   ██░▓██▒  ▐▌██▒
  ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░░██▒██▓ ░ ████▓▒░▒██░   ▓██░
   ██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒    ░ ▓░▒ ▒  ░ ▒░▒░▒░ ░ ▒░   ▒ ▒ 
 ▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░      ▒ ░ ░    ░ ▒ ▒░ ░ ░░   ░ ▒░
 ▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░      ░   ░  ░ ░ ░ ▒     ░   ░ ░ 
 ░ ░         ░ ░     ░            ░        ░ ░           ░ 
 ░ ░                                                       
You don't know exactly why you wanted to leave the maze in the first place, truth is you lived comfortably there, who knows the horrors that await outside...
";

            public static string CharacterSelectionArt = File.ReadAllText("CharacterArt.txt");
            
            public static string deathmessage = "You died, back to the beginning";
            
    }

}

