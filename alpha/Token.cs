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
                //?I could change this and use a direction array but i did this when i still didn't know about it (nostalgia)

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

        public void HandlePlayerDeath(int height, int width)
            {
                //* Checks if player health is 0, changes the player position back to the start center square and sets their health according to each token
                if(health == 0)
                {
                    x = height/2;
                    y = width/2;
                    deathcount +=1;
                    if (name == "Caitlyn" || name == "Jinx" || name == "Viktor" )
                    {
                        health = 4;
                    }
                    else
                    {
                        health = 5;
                    }
                    System.Console.WriteLine(asciiArt.deathmessage);

                }
            }
    }