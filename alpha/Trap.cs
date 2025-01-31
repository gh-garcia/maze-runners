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

        public void TriggerWizardTrap(Token CurrentToken, Token OtherToken )
        {
            int tempX = CurrentToken.x;
            int tempY = CurrentToken.y;

            CurrentToken.x = OtherToken.x;
            CurrentToken.y = OtherToken.y;

            OtherToken.x = tempX;
            OtherToken.y = tempY;

        }

        public static void TriggerTrapPosition(int x, int y, Token CurrentToken, List<Trap> traps, Token OtherToken, string LastTrapMessage)
            {
                foreach (var trap in traps)
                {
                    if (trap.X == x && trap.Y == y)
                    {
                        LastTrapMessage = trap.Trigger(CurrentToken, OtherToken);
                    }
                }
            }

        public string Trigger(Token CurrentToken, Token OtherToken)
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
                    TriggerWizardTrap(CurrentToken, OtherToken);
                }     
                
            }
            return message;
        }
    }