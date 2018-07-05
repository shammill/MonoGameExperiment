using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public class ScatterGameSettings : TileGameSettings
    {
        // Tile generation variables
        public int numberOfYTiles = 4; // X axis tile number is generated after based on this tile size to ensure squares.
        public bool randomlyRotateTiles = false;
        public bool randomlySwapTilePositions = false;
        public bool randomlyPlaceTiles = true;

        public TileGameMode tileGameType = TileGameMode.Scatter;

        public void HandleInput()
        {

        }
    }
}
