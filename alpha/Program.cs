﻿namespace alpha
{
    class Program
    {
        public static void Main()
        {
            Game game = new Game();
            game.StartGame();
        }
    }
    public class Game
    {
        //? Does not affect anything
        Token CurrentToken;
        Token OtherToken; //used in Wizard Trap
        string[,] board;
        int Height = 25;
        int Width = 50;
        bool IsPlayer2Turn = true;
        private Random RRandom = new Random();
        List<Trap> traps = new List<Trap>();
        int TrapCount = 30;
        string LastTrapMessage = "";

        
        int[] DX = {0,-1,0,1};
        int[] DY = {1,0,-1,0};

        public void StartGame()
        {
            Token[] tokens = new Token [2];

            //!I used initials for the skills because different legths break the UI
            Token token1 = new Token("V",(Height/2),(Width/2),"VI",0,5,0,"LC");
            Token token2 = new Token("C",(Height/2)-1,(Width/2),"Caitlyn",0,4,0,"MC");
            Token token3 = new Token("J",(Height/2)-2,(Width/2),"Jayce",0,5,0,"HL"); 
            Token token4 = new Token("K",(Height/2)-3,(Width/2),"Viktor",0,4,0,"SP"); 
            Token token5 = new Token("X",(Height/2)+1,(Width/2),"Jinx",0,4,0,"TD");  


            ShowWelcomeScreen();
                              //?should be a better way to do this
            Token.TokenSelection(tokens,token1,token2,token3,token4,token5);
            CurrentToken = tokens[0];
            OtherToken = tokens[1]; //used in Wizard trap
            board = BoardGeneration.BoardGen(Height, Width, DX, DY);
            BoardGeneration.PlaceTraps(TrapCount, traps, board);
            
            BoardGeneration.BoardDisplay(tokens, board);
            CurrentToken.Movecount = RRandom.Next(1,7);

            while (!WinCondition(1,0)) //*gameloop
            {
                Console.Clear();
                BoardGeneration.BoardDisplay(tokens, board);
                System.Console.WriteLine(ShowUI()); 
                
                TurnManagement(tokens,RRandom);
                   
                
            }
            Console.Clear();
            System.Console.WriteLine(asciiArt.YOUWON);
            Console.ReadKey(true);
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
            Player Turn:{CurrentToken.Name}         
+------------------------------------------------+
|                                                |
|  Moves left:{CurrentToken.Movecount}                                  |
|                                                |
|  > Health:{CurrentToken.Health}                                    |
|  > Deathcount:{CurrentToken.DeathCount}                                |
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

            if(CurrentToken.Movecount > 0)
            {
                int newx;
                int newy;

                (newx,newy)= CurrentToken.MoveToken();

                if(BoardGeneration.CanMove(board,newx,newy))
                {
                    CurrentToken.X = newx;
                    CurrentToken.Y = newy;
                    CurrentToken.Movecount--;
                    CurrentToken.HandlePlayerDeath(Height, Width);
                    LastTrapMessage = Trap.TriggerTrapPosition(newx,newy,CurrentToken, traps, OtherToken, LastTrapMessage);  
                }
                
            }
            
            else
            {
                CurrentToken.Movecount = 0;
                CurrentToken.Cool--;

                if(IsPlayer2Turn == false)
                {
                    CurrentToken = tokens[0];
                    OtherToken = tokens[1];   //used in Wizard Trap
                    IsPlayer2Turn = true;
                }

                else
                {
                    CurrentToken = tokens[1];
                    OtherToken = tokens[0]; //used in Wizard Trap
                    IsPlayer2Turn = false;
                }
                CurrentToken.Movecount = DiceThrow(0,random);
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

        
        public bool WinCondition(int EntranceX, int EntranceY)
        {
            //*Checks if player reached the entrance/exit of the maze
            return CurrentToken.X == 1 && CurrentToken.Y == 0;
            
        }

            
    }
    
}

