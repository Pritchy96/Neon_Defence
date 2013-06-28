using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Tower_Defence_Tutorials
{
    public static class Textures
    {
        private static ContentManager Content;

        private const int COUNT_TILES = 7;
        private static Texture2D[] Tiles;

        private const int COUNT_BACKGROUNDS = 1;
        private static Texture2D[] Backgrounds;


        /*This method is what loads the textures in this class from the main game class
        note that without calling this method this class will not work at all.
        Probably very bad programming design but meh. */
        public static void LoadContent(ContentManager Content)
        {
            Textures.Content = Content;

            Tiles = new Texture2D[COUNT_TILES];
            Backgrounds = new Texture2D[COUNT_BACKGROUNDS];

            LoadTile(0, "PlaceableTile");       //Tile 0 = Non Path
            LoadTile(1, "TopRight");       
            LoadTile(2, "BottomRight");      
            LoadTile(3, "TopLeft");        
            LoadTile(4, "BottomLeft");      
            LoadTile(5, "Horizontal");        
            LoadTile(6, "Vertical");      
        }

        public static void UnLoadContent()
        {
            
        }

        private static void LoadTile(int Index, string Name)
        {
            Tiles[Index] = Content.Load<Texture2D>("Tiles/" + Name);
        }

        public static Texture2D GetTile(int TileID)
        {
            if(TileID < 0 || TileID > COUNT_TILES)
            {
                return Tiles[0];
            }
            else
            {
                return Tiles[TileID];
            }
        }
    }
}
