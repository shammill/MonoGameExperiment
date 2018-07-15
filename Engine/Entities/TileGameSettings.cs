using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public abstract class TileGameSettings
    {
        // Tile generation variables
        public abstract int numberOfYTiles { get; set; }// = 5; // X axis tile number is generated after based on this tile size to ensure squares.
        public abstract bool randomlyRotateTiles { get; set; }// = false;
        public abstract bool randomlySwapTilePositions { get; set; } // = false;
        public abstract bool randomlyPlaceTiles { get; set; }//= true;

        public abstract TileGameMode tileGameType { get; set; }// = TileGameMode.Scatter;
    }
}
