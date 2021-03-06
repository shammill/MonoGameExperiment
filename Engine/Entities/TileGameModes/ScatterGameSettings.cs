﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public class ScatterGameSettings : TileGameSettings
    {
        public override int numberOfYTiles { get; set; } // X axis tile number is generated after based on this tile size to ensure squares.
        public override bool randomlyRotateTiles { get; set; }
        public override bool randomlySwapTilePositions { get; set; }
        public override bool randomlyPlaceTiles { get; set; }
        public override TileGameMode tileGameType { get; set; }

        public ScatterGameSettings()
        {
            numberOfYTiles = 4;
            randomlyRotateTiles = false;
            randomlySwapTilePositions = false;
            randomlyPlaceTiles = true;
            tileGameType = TileGameMode.Scatter;
        }
    }
}
