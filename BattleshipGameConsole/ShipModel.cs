using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGameConsole
{
    /// <summary>
    /// Represents a single ship model
    /// </summary>
    public class ShipModel
    {
        /// <summary>
        /// Represents the top left cell occupied by the ship
        /// </summary>
        public Cell StartPoint { get; }

        /// <summary>
        /// Represents the number of cells occupied by the ship
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Value is <see langword="true"/>  if the orientation of the ship is horizontal
        /// </summary>
        public bool IsHorizontal { get; set; }

        /// <summary>
        /// Value is <see langword="true"/>  if the opponent knows the orientation of the ship
        /// </summary>
        public bool OrientationKnown { get; set; }

        /// <summary>
        /// Value is <see langword="true"/>  if the ship was not sunk by the opponent
        /// </summary>
        public bool IsAfloat { get; set; } = true;

        /// <summary>
        /// Represents a list of all the cells occupied by the ship
        /// </summary>
        public List<Cell> ListOfShipCells { get; set; } = new List<Cell>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipModel"/> class.
        /// </summary>
        public ShipModel(Cell startPoint, int length, bool isHorisontal)
        {
            StartPoint = startPoint;
            Length = length;
            IsHorizontal = isHorisontal;
            OrientationKnown = true;

            for (int i=0;i< length;i++)
            {
                ListOfShipCells.Add(new Cell(startPoint.X + (isHorisontal?i:0), startPoint.Y + (isHorisontal ? 0 : i)));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipModel"/> class.
        /// </summary>
        public ShipModel()
        {
            
        }

        /// <summary>
        /// Returns a List of Cells with coordinantes of points located next to the ship
        /// </summary>
        public List<Cell> CreateListOfCellsInVicinity(int gridSize)
        {
            List<Cell> listOfCells = new List<Cell>();
            List<Cell> output = new List<Cell>();

            if (IsHorizontal)
            {
                for (int i = -1; i < Length+1; i++)
                {
                    listOfCells.Add(new Cell(StartPoint.X+i, StartPoint.Y + 1));
                    listOfCells.Add(new Cell(StartPoint.X+i, StartPoint.Y - 1));
                }
                listOfCells.Add(new Cell(StartPoint.X - 1, StartPoint.Y));
                listOfCells.Add(new Cell(StartPoint.X + Length, StartPoint.Y));
            }
            else
            {
                for (int i = -1; i < Length + 1; i++)
                {
                    listOfCells.Add(new Cell(StartPoint.X+1, StartPoint.Y + i));
                    listOfCells.Add(new Cell(StartPoint.X-1, StartPoint.Y + i));
                }
                listOfCells.Add(new Cell(StartPoint.X, StartPoint.Y-1));
                listOfCells.Add(new Cell(StartPoint.X, StartPoint.Y + Length));
            }


            for(int i=0;i< listOfCells.Count;i++)
            {
                if(listOfCells[i].X < gridSize && listOfCells[i].Y < gridSize && listOfCells[i].X >= 0 && listOfCells[i].Y >= 0)
                {
                    output.Add(listOfCells[i]);
                }
            }


            return output;
        }



    }
}
