namespace alpha
{
    public class Board
    {
        public static void Main()
        {
            ShowWelcomeScreen();
            //TokenSelection();

            string[,] board = BoardGeneration();
            Token token1 = new Token("8",0,1,"Gimli",0,5);
            Token token2 = new Token("8",9,8,"Vi",0,5);
            Token token3 = new Token("8",9,8,"Galadriel",0,5);
            Token token4 = new Token("8",9,8,"Legolas",0,5);
            Token token5 = new Token("8",9,8,"Boromir",0,5);

            Token[] tokens = {token1,token2};
            bool IsPlayer2Turn = false;
            int dado = 1;

            //Trap placement
            Trap trapdamage = new Trap("*");
            
            BoardDisplay(tokens, board);
            
            while (true) //gameloop
            {
                Console.Clear();
                BoardDisplay(tokens, board);

                int newx;
                int newy;

                (newx,newy)= MoveToken(board,token1.x,token1.y);
                if(CanMove(board,newx,newy) && !IsPlayer2Turn)
                {
                token1.x = newx;
                token1.y = newy;
                token1.movecount++;
                if(dado == token1.movecount){IsPlayer2Turn = true; token1.movecount = 0;}
                
                }

                Console.Clear();
                BoardDisplay(tokens, board);

                (newx,newy)= MoveToken(board,token2.x,token2.y);
                if(CanMove(board,newx,newy) && IsPlayer2Turn)
                {
                token2.x = newx;
                token2.y = newy;
                token2.movecount++;
                if(dado== token2.movecount){IsPlayer2Turn = false; token2.movecount = 0;}
                
                }

                
            }
        }
        public static void ShowWelcomeScreen()
        {

        Console.WriteLine(asciiArt.WelcomeScreen);

        var enterpressed = Console.ReadKey(true);

        }

        // public static void TokenSelection()
        // {
            //System.Console.WriteLine(asciiArt.CharacterSelectionArt);

           // var teclamomentanea = Console.ReadKey(true);
        // }
        

        public static string[,] BoardGeneration()
        {
            string[,] board = new string[17,27]
            {
                {"═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","╗"},
                {" "," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║"," ","║","","║","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║","","","","║","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║","","","","║","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║","","","","","","","","","","","","","","","","","","","","","","","","","","║"},
                {"║","","","","","","","","","","","","","","","","","","","","","","","","",""," "},
                {"╚","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═"}
                
            };

            return board;
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
            //*checks if the square i want to move the token to is taken by something (| x)
            if(board[x,y]== "|" || board[x,y]== "x" || board[x,y]== "-" || board[x,y]=="8")
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



        //constructor de Trap
        public Trap(string symbol)
        {
            this.symbol = symbol;
        }
    }



    public static class asciiArt
    {// es static para que cuando vaya para alla arriba no tener que hacer NEW ASCIIART etc... funciona sin tener que crear instancias, un almacen de cosas
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
            ---------------------------ARCANE VERSION-------------------------------

            Press any key to continue...
            ";                    
    }

}

