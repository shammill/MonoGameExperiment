using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public class ShuffleGameSettings : TileGameSettings
    {
        // Tile generation variables
        new public int numberOfYTiles = 4; // X axis tile number is generated after based on this tile size to ensure squares.
        new public bool randomlyRotateTiles = false;
        new public bool randomlySwapTilePositions = true;
        new public bool randomlyPlaceTiles = false;

        new public TileGameMode tileGameType = TileGameMode.Shuffle;
    }
}
