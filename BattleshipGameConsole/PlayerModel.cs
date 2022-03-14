using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BattleshipGameConsole
{
    public class PlayerModel
    {
        /// <summary>
        /// Represents the grid on which the player places his own ships
        /// </summary>
        public Grid PrimaryGrid { get; set; }

        /// <summary>
        /// Represents a list of ships owned by the player
        /// </summary>
        public List<ShipModel> ListOfShips { get; set; }

        /// <summary>
        /// Value is <see langword="true"/> if player shot all opponent's ships
        /// </summary>
        public bool Won { get; set; }

        private List<Cell> ListOfPossibleTargets { get; set; }
        private int gridSize = 10;
        private ShipModel targetedOpponentShip = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModel"/> class.
        /// </summary>
        public PlayerModel(int size, List<int> listOfShips)
        {
            gridSize = size;
            PrimaryGrid = new Grid(gridSize);      
            ListOfShips = PrimaryGrid.PlaceShips(listOfShips);
        }

        /// <summary>
        /// Simulates player's move
        /// </summary>
        public void MakeMove(PlayerModel opponent)
        {
            CreateListOfPossibleTargets(opponent);
            Cell shotCell;

            if (targetedOpponentShip != null)
            {
                shotCell = TryToHitTargetedOpponentShip();
            }
            else
            {
                shotCell = MakeRandomShot(opponent);
            }
            CheckResultOfShot(shotCell, opponent);
        }



        private Cell TryToHitTargetedOpponentShip()
        {
            Cell shotCell;
            int currentlength = targetedOpponentShip.ListOfShipCells.Count;

            List<Cell> horizontalCells = ListOfPossibleTargets.Where(c => c.Y == targetedOpponentShip.ListOfShipCells[0].Y).ToList();
            List<Cell> verticalCells = ListOfPossibleTargets.Where(c => c.X == targetedOpponentShip.ListOfShipCells[0].X).ToList();

            if (targetedOpponentShip.OrientationKnown && targetedOpponentShip.IsHorizontal)
            {
                shotCell = horizontalCells.First(c => c.X == targetedOpponentShip.ListOfShipCells.Min(c => c.X) - 1 ||
                c.X == targetedOpponentShip.ListOfShipCells.Max(c => c.X) + 1);
            }
            else if (targetedOpponentShip.OrientationKnown && !targetedOpponentShip.IsHorizontal)
            {
                shotCell = verticalCells.OrderBy(c => c.Y).First(c => c.Y == targetedOpponentShip.ListOfShipCells.Min(c => c.Y) - 1 ||
                c.Y == targetedOpponentShip.ListOfShipCells.Max(c => c.Y) + 1);
            }
            else
            {
                horizontalCells.AddRange(verticalCells);
                shotCell = horizontalCells.First(c => c.X == targetedOpponentShip.ListOfShipCells[0].X - 1 ||
                c.X == targetedOpponentShip.ListOfShipCells[currentlength - 1].X + 1 ||
                c.Y == targetedOpponentShip.ListOfShipCells[0].Y - 1 ||
                c.Y == targetedOpponentShip.ListOfShipCells[currentlength - 1].Y + 1);
            }
            return shotCell;
        }


        private Cell MakeRandomShot(PlayerModel opponent)
        {
            Random rnd = new Random();

            Cell shotCell = ListOfPossibleTargets[rnd.Next(0, ListOfPossibleTargets.Count)];

            return shotCell;
        }


        private void CheckResultOfShot(Cell shotCell, PlayerModel opponent)
        {
            if (opponent.PrimaryGrid.Cells[shotCell.X][shotCell.Y].OccupiedByShip == true)
            {
                opponent.PrimaryGrid.Cells[shotCell.X][shotCell.Y].ShipCellShotByOpponent = true;
                if (targetedOpponentShip == null)
                    targetedOpponentShip = new ShipModel();
                targetedOpponentShip.ListOfShipCells.Add(shotCell);
              
                if(targetedOpponentShip.OrientationKnown == false && targetedOpponentShip.ListOfShipCells.Count == 2)
                {
                    targetedOpponentShip.OrientationKnown = true;
                    targetedOpponentShip.IsHorizontal = CheckIfOrientationHorizontal(targetedOpponentShip.ListOfShipCells);
                }
                CheckIfTargetedShipSunk(opponent, shotCell);
            }
            else
            {
                opponent.PrimaryGrid.Cells[shotCell.X][shotCell.Y].CanBeTargeted = false;
            }

        }

        private void CheckIfTargetedShipSunk(PlayerModel opponent, Cell shotCell)
        {
            foreach (ShipModel ship in opponent.ListOfShips)
            {
                Cell cell = ship.ListOfShipCells.FirstOrDefault(c => (c.X == shotCell.X && c.Y == shotCell.Y));

                if (cell != null)
                {
                    if (targetedOpponentShip.ListOfShipCells.Count == ship.Length)
                    {
                        opponent.PrimaryGrid.PlaceGivenShip(ship,false);
                        ship.IsAfloat = false;
                        Won = CheckIfPlayerWon(opponent.ListOfShips);
                        targetedOpponentShip = null;
                    }
                    break;
                }
            }
        }

        private bool CheckIfOrientationHorizontal(List<Cell> points)
        {
            if (points[0].X == points[1].X) return false;
            else return true;
        }


        private void CreateListOfPossibleTargets(PlayerModel opponent)
        {
            ListOfPossibleTargets = new List<Cell>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (opponent.PrimaryGrid.Cells[i][j].CanBeTargeted == true && opponent.PrimaryGrid.Cells[i][j].ShipCellShotByOpponent == false)
                    {
                        ListOfPossibleTargets.Add(opponent.PrimaryGrid.Cells[i][j]);
                    }
                }
            }
        }

        private bool CheckIfPlayerWon(List<ShipModel> listOfShips)
        {
            foreach (ShipModel ship in listOfShips)
            {
                if (ship.IsAfloat == true) return false;
            }
            return true;
        }

    }
}
