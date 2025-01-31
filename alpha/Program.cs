namespace alpha
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
            Token token1 = new Token("V",1,2,"VI",0,5,0,"LC"); //? there is a 1 in 1000 chance to be teleported to the exit
            Token token2 = new Token("C",(Height/2)-1,(Width/2),"Caitlyn",0,4,0,"MC"); //? there is a 50% chance that if health reaches 0 a health point is granted
            Token token3 = new Token("J",(Height/2)-2,(Width/2),"Jayce",0,5,0,"HL"); //? gains +1 health every 5 turns
            Token token4 = new Token("K",(Height/2)-3,(Width/2),"Viktor",0,4,0,"SP"); //? gains +1 to dicethrow every 5 turns
            Token token5 = new Token("X",(Height/2)+1,(Width/2),"Jinx",0,4,0,"TD"); //? can disarm all traps


            ShowWelcomeScreen();
                              //?should be a better way to do this
            Token.TokenSelection(tokens,token1,token2,token3,token4,token5);
            CurrentToken = tokens[0];
            OtherToken = tokens[1]; //used in Wizard trap
            board = BoardGeneration.BoardGen(Height, Width, DX, DY);
            BoardGeneration.PlaceTraps(TrapCount, traps, board);
            
            BoardGeneration.BoardDisplay(tokens, board);

            while (!WinCondition(1,0)) //*gameloop
            {
                Console.Clear();
                BoardGeneration.BoardDisplay(tokens, board);
                System.Console.WriteLine(ShowUI()); 
                
                TurnManagement(tokens,RRandom);
                   
                
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

                (newx,newy)= CurrentToken.MoveToken();

                if(BoardGeneration.CanMove(board,newx,newy))
                {
                    CurrentToken.x = newx;
                    CurrentToken.y = newy;
                    CurrentToken.movecount--;
                    CurrentToken.HandlePlayerDeath(Height, Width);
                    Trap.TriggerTrapPosition(newx,newy,CurrentToken, traps, OtherToken, LastTrapMessage);  
                }
                
            }
            
            else
            {
                CurrentToken.movecount = 0;
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

        
        public bool WinCondition(int EntranceX, int EntranceY)
        {
            //*Checks if player reached the entrance/exit of the maze
            return CurrentToken.x == 1 && CurrentToken.y == 0;
            
        }

            
    }
    
}

