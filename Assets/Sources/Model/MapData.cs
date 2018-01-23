using System.Collections.Generic;
using UnityEngine;

namespace TRPG {
    [CreateAssetMenu(fileName="New Map", menuName="Map", order = 1000)]
    public class MapData : ScriptableObject {
        public string Title;
        public Tile[] Tiles = new Tile[0];
        

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