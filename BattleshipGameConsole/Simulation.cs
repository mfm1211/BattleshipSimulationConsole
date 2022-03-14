using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipGameConsole
{
    public class Simulation
    {
        public PlayerModel[] Players { get; private set; } = new PlayerModel[2];

        private int indexOfNextPlayerToMove = 0;

        int gridSize = 10;
        List<int> ListOfShips = new List<int>() { 5, 4, 3, 3, 2 };

        public Simulation()
        {
            CreatePlayers();
        }

        /// <summary>
        /// Initializes models of two players
        /// </summary>
        public void CreatePlayers()
        {
            Players[0] = new PlayerModel(gridSize, ListOfShips);
            Players[1] = new PlayerModel(gridSize, ListOfShips);
        }

        /// <summary>
        /// Creates the next move of the simulation
        /// </summary>
        public void MakeNextMove()
        {
           int otherIndex = (indexOfNextPlayerToMove + 1) % 2;

            Players[indexOfNextPlayerToMove].MakeMove(Players[otherIndex]);

            indexOfNextPlayerToMove = otherIndex;
        }

    }
}
