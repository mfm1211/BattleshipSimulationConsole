using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGameConsole
{
    /// <summary>
    /// Represents a single cell of a player's grid
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// X coordinate of the cell
        /// </summary>
        public int X { get;}

        /// <summary>
        /// Y coordinate of the cell
        /// </summary>
        public int Y { get;}

        /// <summary>
        /// Value is <see langword="true"/>  if a ship is located on the cell
        /// </summary>
        public bool OccupiedByShip { get; set; }

        /// <summary>
        /// Value is <see langword="true"/> if a ship is located next to the cell (horizontally, vertically or diagonally)
        /// </summary>
        public bool InShipVecenity { get; set; } = false;

        /// <summary>
        /// Value is <see langword="true"/> if a the cell is a potential target for the opponent
        /// </summary>
        public bool CanBeTargeted { get; set; } = true;


        /// <summary>
        /// Value is <see langword="true"/> if a ship is located on the cell and it was shot by the opponent
        /// </summary>
        public bool ShipCellShotByOpponent { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
