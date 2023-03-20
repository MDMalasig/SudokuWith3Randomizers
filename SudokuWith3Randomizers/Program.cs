using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SudokuWith3Randomizers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] numGrid = new int[9, 9];
            int[] guess = new int[] { 0, 0, 0 }; // row, column, cell
            List<int>[] rows = new List<int>[9];
            List<int>[] cols = new List<int>[9];
            List<int>[,] cells = new List<int>[3, 3];
            int screamAttempt = 1000;
            int genAttempt = 0;
            bool genFail = true;

            // statistics
            int sum = 0;
            double average = 0;
            int pool = 1000;

            Random rnd = new Random();

            for (int p = 0; p < pool; p++)
            {
                Console.WriteLine("Test {0}", p);
                genAttempt = 0;
                while (true)
                {
                    //genFail = true;
                    genAttempt++;
                    //Console.Write("Starting attempt {0}", genAttempt);
                    // initialize lists
                    // rows
                    for (int x = 0; x < rows.Length; x++)
                        rows[x] = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    // cols
                    for (int x = 0; x < cols.Length; x++)
                        cols[x] = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                    // cells
                    for (int x = 0; x < cells.GetLength(0); x++)
                    {
                        for (int y = 0; y < cells.GetLength(1); y++)
                        {
                            cells[x, y] = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        }
                    }

                    for (int x = 0; x < numGrid.GetLength(0); x++)// row
                    {
                        for (int y = 0; y < numGrid.GetLength(1); y++) // col
                        {
                            genFail = true;
                            for (int a = 0; a < screamAttempt; a++)
                            {
                                // initialize scream
                                guess[0] = rows[x][rnd.Next(0, rows[x].Count())];
                                guess[1] = cols[y][rnd.Next(0, cols[y].Count())];
                                guess[2] = cells[x / 3, y / 3][rnd.Next(0, cells[x / 3, y / 3].Count())];

                                if (guess[0] == guess[1] && guess[1] == guess[2])
                                {
                                    numGrid[x, y] = guess[0];
                                    rows[x].Remove(guess[0]);
                                    cols[y].Remove(guess[0]);
                                    cells[x / 3, y / 3].Remove(guess[0]);
                                    genFail = false;
                                    break;
                                }
                            }

                            if (genFail)
                            {
                                //Console.WriteLine("... Failed at {0}, {1}", x, y);
                                break;
                            }
                        }
                        if (genFail)
                            break;
                    }
                    if (!genFail)
                    {
                        //Console.WriteLine("... Success!");
                        sum += genAttempt;
                        break;
                    }
                }
            }

            //for (int x = 0; x < numGrid.GetLength(0); x++)// row
            //{
            //    for (int y = 0; y < numGrid.GetLength(1); y++) // col
            //    {
            //        Console.Write(numGrid[x,y] + "\t");
            //    }
            //    Console.WriteLine();
            //}
            average = sum / pool;
            Console.WriteLine("Out of {0} runs, the average is {1}", pool, average);
            Console.ReadKey();
        }
    }
}
