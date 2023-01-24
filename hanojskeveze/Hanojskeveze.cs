using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanojskeveze
{
    internal class Hanojskeveze
    {
        private bool hotovo = false;
        public const int pocetKotoucu = 9;
        int moves = 0;
       
        public Stack<int>[] towers = new Stack<int>[3];
        Stopwatch watch = new System.Diagnostics.Stopwatch();
        
        public void Napln()
        {
            for (int i = 0; i < towers.Length; i++)
            {
                towers[i] = new Stack<int>();
            }
            for (int i = pocetKotoucu; i > 0; i--)
            {
                towers[0].Push(i);

            }
        }

        public string VypisKotouc(int index)
        {
            string vypis = "|";
            if (towers[index].Count() > 0)
            {
                foreach (int item in towers[index].Reverse())
                {
                    vypis += + item + " ";
                }
            }
            return vypis;
        }

        public string VypisKotouce()
        {
            string vypis = "";
            for (int i = 0; i < towers.Length; i++)
            {
                vypis += VypisKotouc(i) + Environment.NewLine;
            }
            return vypis;
        }


        public void Game()
        {
            watch.Start();
            bool smerVlevo = pocetKotoucu % 2 != 0 ? true : false;

            while (!hotovo)
            {
                Console.WriteLine(VypisKotouce());
                
                int positionOf1 = -1;
                for (int i = 0; i < towers.Length; i++)
                {
                    if (towers[i].Count() > 0 && towers[i].Peek() == 1)
                    {
                        towers[i].Pop();
                        if (smerVlevo && i > 0)
                        {
                            towers[i - 1].Push(1);
                            positionOf1 = i - 1;
                        }
                        else if (smerVlevo)
                        {
                            towers[towers.Length - 1].Push(1);
                            positionOf1 = towers.Length - 1;
                        }

                        else if (!smerVlevo && i < towers.Length - 1)
                        {
                            towers[i + 1].Push(1);
                            positionOf1 = i + 1;
                        }
                        else if (!smerVlevo)
                        {
                            towers[0].Push(1);
                            positionOf1 = 0;
                        }
                        break;
                    }
                }
                moves++;
                Console.WriteLine(VypisKotouce());
                
                int positionOfBigger = -1;
                int positionOfSmaller = -1;

                for (int i = 0; i < towers.Length; i++)
                {
                    if (towers[i].Count() == pocetKotoucu)
                    {
                        watch.Stop();
                        hotovo = true;
                        Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
                        Console.WriteLine("Moves: " + moves);
                    }
                }

                if (!hotovo)
                {
                    for (int i = 0; i < towers.Length; i++)
                    {
                        if ((towers[i].Count() > 0 && i != positionOf1) && positionOfBigger == -1)
                        {
                            positionOfBigger = i;

                        }
                        else if ((towers[i].Count() > 0 && i != positionOf1) && (towers[i].Peek() > towers[positionOfBigger].Peek()))
                        {
                            positionOfSmaller = positionOfBigger;
                            positionOfBigger = i;
                        }
                        else if (towers[i].Count() > 0 && i != positionOf1 || i != positionOf1)
                        {
                            positionOfSmaller = i;
                        }
                    }
                    if (towers[positionOfSmaller].Count() == 0)
                    {
                        int x = towers[positionOfBigger].Pop();
                        towers[positionOfSmaller].Push(x);
                    }
                    else
                    {
                        int x = towers[positionOfSmaller].Pop();
                        towers[positionOfBigger].Push(x);
                    }
                }
                moves++;

            }
        }
    }
}
