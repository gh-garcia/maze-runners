using System.Security.Cryptography.X509Certificates;

public class Token
    {
        public string Symbol;
        public int X;
        public int Y;
        public string Name;
        public int Movecount;
        public int Health;
        public int DeathCount;
        public string Skill;

        public int Cool = 1;

        public Token(string symbol, int x, int y, string name, int movecount, int health, int deathcount, string Skill)
        {
            this.Symbol = symbol;
            this.X = x; 
            this.Y = y;
            this.Name = name;
            this.Movecount = movecount;
            this.Health = health;
            this.DeathCount = deathcount;
            this.Skill = Skill;
        }
        public static void TokenSelection(Token[] tokens,Token token1,Token token2,Token token3,Token token4,Token token5)
        {
            //*Handles input for choosing a player, stores the selection in an 2 space array
            for (int i = 0; i <= 1; i++)
            {

            System.Console.WriteLine(asciiArt.CharacterSelectionArt);
            var keyInfo = Console.ReadKey(true);

            while(keyInfo.Key != ConsoleKey.D1 && keyInfo.Key != ConsoleKey.D2 && keyInfo.Key != ConsoleKey.D3 && keyInfo.Key != ConsoleKey.D4 && keyInfo.Key != ConsoleKey.D5 )
            {   
                keyInfo = Console.ReadKey(true);
                System.Console.WriteLine("Wrong key...");
            }

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
    
              System.Console.WriteLine("Confirm selection: ENTER");  
              var enterpressed = Console.ReadKey(true);
              Console.Clear();
            }
        }


        public (int x,int y) MoveToken()
            {
                int newX = X;
                int newY = Y;
                //?I could change this and use a direction array but i did this when i still didn't know about it (nostalgia)

                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {   
                    newX--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {   
                    newX++;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {   
                    newY--;  
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {   
                    newY++; 
                }
                return(newX,newY);
                
            }

        public void DoSkill()
        {
            //*Trap Disarm is implemented over at Trigger()

            if (Skill == "LC")
            {
                Random random = new();
                if (random.Next(1,1001) == 1)
                {
                    X = 2;
                    Y = 0;
                    System.Console.WriteLine($"WOWOWOWOW IT'S A MIRACLE YOU SOMEHOW MADE IT NEXT TO THE EXIT,{Name} you are free to go");
                }
            }
            else if (Skill == "MC" && Health == 0)
            {
                Random random = new Random();
                if (random.Next(1,101) <= 50)
                {
                Health = 1;
                System.Console.WriteLine($"You somehow keep getting your way, {Name}, your life has been spared once again");  
                }
            }
            else if (Skill == "HL")
            {
                if (Cool == 0)
                {
                    Health++;
                    Cool = 5;
                    System.Console.WriteLine($"You healed yourself, {Name} lives another day");
                }
            }
            else if (Skill == "SP")
            {
                if(Cool == 0)
                {
                    Movecount++;
                    Cool = 5;
                    System.Console.WriteLine($"{Name} used Sprint, +1 has been added to your dicethrow");
                }
            }

        }

        public void HandlePlayerDeath(int height, int width)
            {
                //* Checks if player health is 0, changes the player position back to the start center square and sets their health according to each token
                if(Health == 0)
                {
                    X = height/2;
                    Y = width/2;
                    DeathCount +=1;
                    if (Name == "Caitlyn" || Name == "Jinx" || Name == "Viktor" )
                    {
                        Health = 4;
                    }
                    else
                    {
                        Health = 5;
                    }
                    System.Console.WriteLine(asciiArt.deathmessage);

                }
            }
    }