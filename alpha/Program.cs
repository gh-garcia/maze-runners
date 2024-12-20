namespace alpha
{
    public class Board
    {
        public static void Main()
        {
            string[,] board2 = BoardGeneration();
            string player1 = "8";
            int x = 0;
            int y = 1;
            BoardDisplay(player1, x, y, board2);
            
            while (true)
            {
                Console.ReadLine();
                Console.Clear();
                y++;
                BoardDisplay(player1, x, y, board2);
            }
        }

        public static string[,] BoardGeneration()
        {
            string[,] board = new string[10,10]
            {
                {"+"," ","_","_","_","_","_","_","_","+"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"|"," "," "," "," "," "," "," "," ","|"},
                {"+","_","_","_","_","_","_","_"," ","+"}
            };

            return board;
        }

        public static void MovePlayer()
        {
            
        }

        public static void BoardDisplay(string player1, int x, int y, string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1) ; j++)
                {
                    if(i == x && j == y)
                    {
                        System.Console.Write(player1);
                    }
                    else
                    {
                    System.Console.Write(board[i,j]);
                    }
                    
                }
                System.Console.WriteLine();
            }
        }
    }
}

