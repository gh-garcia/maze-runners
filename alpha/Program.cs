﻿
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
        bool IsPlayer2Turn = true;
        private Random random = new Random();
        //int dado = 1;

        public Board()
        {
            board = BoardGeneration();
        }

        public void StartGame()
        {
            Token[] tokens = new Token [2];

            Token token1 = new Token("1",2,1,"VI",0,5);
            Token token2 = new Token("2",10,10,"Caitlyn",0,4);
            Token token3 = new Token("3",0,2,"Jayce",0,5);
            Token token4 = new Token("4",10,10,"Viktor",0,3);
            Token token5 = new Token("5",0,2,"Jinx",0,4);

            ShowWelcomeScreen();
            //debe haber una manera mejor de hacer esto
            TokenSelection(tokens,token1,token2,token3,token4,token5);
            CurrentToken = tokens[0];

            board = BoardGeneration();

            //Trap placement
            //Trap trapdamage = new Trap("*");
            
            BoardDisplay(tokens, board);
            while (true) //gameloop
            {
                Console.Clear();
                BoardDisplay(tokens, board);

                
                TurnManagement(tokens,random);    
                
            }
        }
        public static void ShowWelcomeScreen()
        {

        Console.WriteLine(asciiArt.WelcomeScreen);

        var enterpressed = Console.ReadKey(true);
        Console.Clear();

        }

        public void TurnManagement(Token[] tokens, Random random)
        {
            int dadoX  = DiceThrow(0,random);

            while (dadoX > 0)
            {
                int newx;
                int newy;

                (newx,newy)= MoveToken(board,CurrentToken.x,CurrentToken.y);

                if(CanMove(board,newx,newy))
                {
                    CurrentToken.x = newx;
                    CurrentToken.y = newy;
                    CurrentToken.movecount++;
                    dadoX--;

                    System.Console.WriteLine($"{dadoX} moves left");
                }
            }

            if(dadoX == 0)
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
          }

          
          
            
        }

        public int DiceThrow(int dado, Random random)
        {
            System.Console.WriteLine($"It's {CurrentToken.name}'s turn");

            while(dado == 0)
            {
                System.Console.WriteLine("Press R to roll the dice");
            
                var input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.R)
                {   
                    dado = random.Next(1,7);
                    System.Console.WriteLine($"Player has {dado} remaining moves");
                }
                else 
                {
                    System.Console.WriteLine($" {dado} Wrong key, please press R to roll...");
            
                }
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
        

        public static string[,] BoardGeneration()
        {
            string[,] board = new string[17,27]
            {
                {"═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","═","╗"},
                {" "," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," ","║"," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," "," "," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," "," "," ","║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," ","║"},
                {"║"," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "," "},
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

