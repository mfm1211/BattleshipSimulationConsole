using System;
using System.Collections.Generic;


namespace BattleshipGameConsole
{
    class Program
    {
         
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Simulation simulation = new Simulation();

            int count = 1;
            string input = "";
            while (input!="z")
            {
               
                Console.Clear();
                simulation.MakeNextMove();
                if (simulation.Players[0].Won == true || simulation.Players[1].Won == true)
                    Console.WriteLine("player won");
                else
                    DrawBoard(count, simulation);
                count++;
                input = Console.ReadLine();
            }

         
        }


        static void DrawBoard(int n, Simulation simulation)
        {
            Console.WriteLine(n.ToString());

            foreach(PlayerModel player in simulation.Players)
            {
                for (int y = 0; y < 10; y++)
                {
                    string s = "";
                    for (int x = 0; x < 10; x++)
                    {
                        if (player.PrimaryGrid.Cells[x][y].OccupiedByShip == true)
                        {
                            s = s + "x ";
                        }
                        else
                        {
                            s = s + "- ";
                        }
                    }
                    s = s + "        ";

                    for (int x = 0; x < 10; x++)
                    {
                        if (player.PrimaryGrid.Cells[x][y].ShipCellShotByOpponent == true)
                        {
                            s = s + "x ";
                        }
                        else if (player.PrimaryGrid.Cells[x][y].CanBeTargeted == false)
                        {
                            s = s + "o ";
                        }
                        else
                        {
                            s = s + "- ";
                        }
                       
                    }

                    Console.WriteLine(s);
                }

                foreach(ShipModel ship in player.ListOfShips)
                {
                    string t = "";
                    for(int i=0; i< ship.ListOfShipCells.Count;i++)
                    {
                        t = t + "X: " + ship.ListOfShipCells[i].X + " Y: " + ship.ListOfShipCells[i].Y + "       ";
                    }
                    Console.WriteLine(t);

                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
          
            
        }



     

      




      


    }
}
