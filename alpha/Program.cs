namespace alpha
{
    class Program
    {
        static void Main()
        {
            int WIDTH = 14;
            int HEIGHT = 14;

            string[,] grid = new string[HEIGHT,WIDTH];

            for (int i = 0; i < HEIGHT; i++ )
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    grid[i,j] = " ";
                }
            }

            for (int i = 0; i < HEIGHT; i++ )
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
            
            
        }
    }
}
