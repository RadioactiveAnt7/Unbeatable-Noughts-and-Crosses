using System;
using System.Diagnostics;
namespace X_and_O
{
    class Program
    {
        static void draw_grid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    Console.Write("[ ");
                    Console.Write(grid[i, n]);
                    Console.Write(" ]");
                }

                Console.WriteLine("");
            }
        }

        static bool line_win(string type)
        {
            int type_amount = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int n = 0; n < 3; n++)
                    if (Convert.ToString(grid[i, n]) == type)
                        type_amount++;
                if (type_amount == 3) return true;
                type_amount = 0;
            }

            return false;
        }

        static bool vertical_win(string type)
        {
            int type_amount = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int n = 0; n < 3; n++)
                    if (Convert.ToString(grid[n, i]) == type)
                        type_amount++;
                if (type_amount == 3) return true;
                type_amount = 0;
            }

            return false;
        }

        static bool diag_win(string type)
        {
            char[,] diag_wins = { { grid[0, 0], grid[1, 1], grid[2, 2] }, { grid[0, 2], grid[1, 1], grid[2, 0] } };
            int type_amount = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int n = 0; n < 3; n++)
                    if (Convert.ToString(diag_wins[i, n]) == type)
                        type_amount++;
                if (type_amount == 3) return true;
                type_amount = 0;
            }

            return false;
        }


        static void add_to_grid(char type)
        {
            bool done = false;
            string num;
            while (!done)
            {
                num = Console.ReadLine();
                for (int i = 0; i < 3; i++)
                for (int n = 0; n < 3; n++)
                    if (Convert.ToChar(num) == grid[i, n])
                        if ((grid[i, n] != 'X' || grid[i, n] != 'O'))
                        {
                            grid[i, n] = type;
                            done = true;
                        }
            }
        }

        static bool draw()
        {
            int amount = 0;
            for (int i = 0; i < 3; i++)
            for (int n = 0; n < 3; n++)
                if (grid[i, n] == 'X' || grid[i, n] == 'O')
                    amount++;
            if (amount == 9) return true;
            return false;
        }


        public static char[,] grid = { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };

        static void place_random()
        {
            System.Random random = new System.Random();
            bool done = false; int ran; int count = 0;
            while (!done)
            {
                ran = random.Next(1, 9);
                for (int i = 0; i < 3; i++)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        if (grid[i, n] != 'X' || grid[i, n] != 'O') count++;
                        if (Convert.ToString(grid[i, n]) == Convert.ToString(ran))
                        {
                            done = true;
                            grid[i, n] = 'O';
                        }
                    }
                }
                if (count == 0) done = true;
            }
        }

        static bool place_horizontal(char type)
        {
            int free = 0; int x = 0; int y = 0; int taken = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    if (grid[i, n] == type) taken++;
                    if (grid[i, n] != 'X' && grid[i, n] != 'O')
                    { free++; x = i; y = n;
                    }
                }
                if (taken == 2 && free == 1)
                {
                    grid[x, y] = 'O';
                    return true;
                }

                free = 0; taken = 0;
            }

            return false;
        }

        static bool place_vertical(char type)
        { int free = 0; int x = 0; int y = 0; int taken = 0;
            for (int n = 0; n < 3; n++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (grid[i, n] == type) taken++;
                    if (grid[i, n] != 'X' && grid[i, n] != 'O')
                    {
                        free++; x = i; y = n;
                    }
                }
                if (taken == 2 && free == 1)
                {
                    grid[x, y] = 'O';
                    return true;
                }

                free = 0; taken = 0;
            } return false;
        }

        static bool place_diag(char type)
        {
            char[,] diag_wins = { { grid[0, 0], grid[1, 1], grid[2, 2] }, { grid[0, 2], grid[1, 1], grid[2, 0] } };
            int free = 0; int x = 0; int y = 0; int taken = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    if (diag_wins[i, n] == type) taken++;
                    if (diag_wins[i, n] != 'X' && diag_wins[i, n] != 'O')
                    {
                        free++; x = i; y = n;
                    }
                }

                if (taken == 2 && free == 1)
                {
                    place_point(diag_wins[x, y], 'O');
                    return true;
                }

                free = 0; taken = 0;
            } return false;
        }

        static void place_point(char num, char type)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    if (grid[i, n] == num) grid[i, n] = type;
                }
            }
        }

        static bool place_mid()
        {
            if (grid[1, 1] != 'X' && grid[1, 1] != 'O')
            {
                grid[1, 1] = 'O'; return true;
            }
            return false;
        }

        static bool palce_out()
        {
            if ((grid[0, 0] == 'X' && grid[2, 2] == 'X') || (grid[2, 0] == 'X' && grid[0, 2] == 'X'))
            {
                grid[1, 0] = 'O';
                return true;
            }

            if (grid[1, 1] != 'O' && grid[2, 2] != 'O' && grid[2, 2] != 'X')
            {
                grid[2, 2] = 'O';
                return true;
            }

            if (grid[2, 0] != 'X' && grid[2, 0] != 'O')
            {
                grid[2, 0] = 'O';
                return true;
            }

            return false;
        }

        static void run_AI()
        {
            if (!place_horizontal('O'))
                if (!place_vertical('O'))
                    if (!place_diag('O'))
                        if (!place_horizontal('X'))
                            if (!place_vertical('X'))
                                if (!place_diag('X'))
                                    if (!place_mid())
                                        if (!palce_out())
                                            place_random();
        }
        public static int fib(int num) {
            if (num <= 1) {
                return num;
            }

            return fib(num - 2) + fib(num - 1);
        }

  

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int result = fib(1);
            Console.WriteLine(result);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            Console.WriteLine(executionTime);
            
            
            bool not_won = true; int turn = 0; 
            Console.WriteLine("X goes first");
            while (not_won)
            {
                draw_grid();
                if (turn % 2 == 0)
                {
                    Console.WriteLine("---------------------");
                    add_to_grid('X');

                    if (diag_win("X") || line_win("X") || vertical_win("X"))
                    {
                        Console.WriteLine("X won");
                        not_won = false;
                    }
                    else if (draw())
                    {
                        Console.WriteLine("draw");
                        not_won = false;
                    }
                }

                if (turn % 2 == 1)
                {
                    Console.WriteLine("---------------------");
                    run_AI();

                    if (diag_win("O") || line_win("O") || vertical_win("O"))
                    {
                        Console.WriteLine("O won");
                        not_won = false;
                    }
                    else if (draw())
                    {
                        Console.WriteLine("draw");
                        not_won = false;
                    }
                }
                turn++;
            }
            draw_grid();
        }
    }
   
}