using System.Collections.Generic;
using UnityEngine;

namespace TRPG {
    public class MapData : ScriptableObject {
        public Tile[] Tiles = new Tile[0];
        public string Title;

        public void AddTile(Tile tile){
            if(!Contains(tile) && !HasTileAtPosition(tile.Position)) {
                Tile[] temp = new Tile[Tiles.Length+1];

                for(int i = 0; i < Tiles.Length; i++) {
                    temp[i] = Tiles[i];
                }
                temp[Tiles.Length] = tile;
                
                Tiles = temp;
                temp = null;
            }
        }

        public bool Contains(Tile tile) {
            for(int i = 0; i < Tiles.Length; i++) {
                if(Tiles[i] == tile)
                    return true;
            }
            return false;
        }

        public bool HasTileAtPosition(IntVector2 position){
            for(int i = 0; i < Tiles.Length; i++) {
                if(Tiles[i].Position == position)
                    return true;
            }
            return false;
        }

    }
}