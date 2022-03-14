using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BattleshipGameConsole
{
    /// <summary>
    /// Represents the grid on which the player place his ships
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Represents a list of all the cells of the grid
        /// </summary>
        public List<List<Cell>> Cells { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid(int size)
        {
            Cells = new List<List<Cell>>();

            for (int i = 0; i < size; i++)
            {
                List<Cell> row = new List<Cell>();
                for (int j = 0; j < size; j++)
                {
                   Cell c = new Cell(i, j);
                    row.Add(c);
                }
                Cells.Add(row);
            }
        }

        /// <summary>
        /// Places all the ships on the grid
        /// </summary>
        public List<ShipModel> PlaceShips(List<int> shipLengths)
        {
            shipLengths.OrderByDescending(n => n);

            List<ShipModel> output = new List<ShipModel>();

            for (int i = 0; i < shipLengths.Count; i++)
            {
                List<ShipModel> listOfPossibleStartPoints = CreateListofPossibleShipStartPoints(Cells, shipLengths[i]);

                Random rnd = new Random();

                ShipModel ship = listOfPossibleStartPoints[rnd.Next(0, listOfPossibleStartPoints.Count)];
                output.Add(ship);
                PlaceGivenShip(ship, true);
            }
            return output;
        }


        /// <summary>
        /// Places a single ship on the grid
        /// </summary>
        public void PlaceGivenShip(ShipModel ship, bool shipPlacedAtStart)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                if (shipPlacedAtStart) Cells[ship.ListOfShipCells[i].X][ship.ListOfShipCells[i].Y].OccupiedByShip = true;
                else Cells[ship.ListOfShipCells[i].X][ship.ListOfShipCells[i].Y].ShipCellShotByOpponent = true;
            }

            List<Cell> listOfCellsNearGrid = ship.CreateListOfCellsInVicinity(Cells[0].Count);
            for (int i = 0; i < listOfCellsNearGrid.Count; i++)
            {
                if (shipPlacedAtStart) Cells[listOfCellsNearGrid[i].X][listOfCellsNearGrid[i].Y].InShipVecenity = true;
                else Cells[listOfCellsNearGrid[i].X][listOfCellsNearGrid[i].Y].CanBeTargeted = false;

            }

        }

        private List<ShipModel> CreateListofPossibleShipStartPoints(List<List<Cell>> grid, int shipLength)
        {
            List<ShipModel> outputList = new List<ShipModel>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Tuple<bool, bool> isPossible = new Tuple<bool, bool>(true, true);

                    if (i < 10 - shipLength)
                    {
                        for (int k = 0; k < shipLength; k++)
                        {
                            if (grid[i + k][j].OccupiedByShip || grid[i + k][j].InShipVecenity)
                            {
                                isPossible = new Tuple<bool, bool>(false, isPossible.Item2);
                            }
                        }
                    }
                    else
                    {
                        isPossible = new Tuple<bool, bool>(false, isPossible.Item2);
                    }

                    if (j < 10 - shipLength)
                    {
                        for (int k = 0; k < shipLength; k++)
                        {
                            if (grid[i][j + k].OccupiedByShip || grid[i][j + k].InShipVecenity)
                            {
                                isPossible = new Tuple<bool, bool>(isPossible.Item1, false);
                            }
                        }
                    }
                    else
                    {
                        isPossible = new Tuple<bool, bool>(isPossible.Item1, false);
                    }

                    if (isPossible.Item1)
                        outputList.Add(new ShipModel(new Cell(i, j), shipLength, true));
                    if (isPossible.Item2)
                        outputList.Add(new ShipModel(new Cell(i, j), shipLength, false));

                }
            }
            return outputList;
        }

      

       


    }
}
