using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tower_Defence
{
    public class Level
    {
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        #region Map Array Code
        int[,] map = 
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,5,5,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,6,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,6,0},
            {5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,7,5,5,2,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0},
            {0,0,0,0,0,0,3,5,5,5,5,5,5,5,5,7,5,5,1,0},
            {0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,6,0,0,6,0},
            {0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,6,0,0,6,0},
            {5,5,5,5,5,5,2,0,0,0,0,0,0,0,0,4,5,5,2,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}

        };
        #endregion

        private const int TileWidth = 40;

        public Level()
        {
            waypoints.Enqueue(new Vector2(0, 4) * 40);
            waypoints.Enqueue(new Vector2(18, 4) * 40);
            waypoints.Enqueue(new Vector2(18, 1) * 40);
            waypoints.Enqueue(new Vector2(15, 1) * 40);
            waypoints.Enqueue(new Vector2(15, 10) * 40);
            waypoints.Enqueue(new Vector2(18, 10) * 40);
            waypoints.Enqueue(new Vector2(18, 7) * 40);
            waypoints.Enqueue(new Vector2(6, 7) * 40);
            waypoints.Enqueue(new Vector2(6, 10) * 40);
            waypoints.Enqueue(new Vector2(0, 10) * 40);
        }

        public int Width
        {
            get { return map.GetLength(1); }
        }
        public int Height
        {
            get { return map.GetLength(0); }
        }
        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //Draw the tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                        Texture2D tileTexture = Textures.GetTile(map[y, x]);
                        Rectangle tileRectangle = new Rectangle(x * TileWidth, y * TileWidth, TileWidth, TileWidth);
                        spriteBatch.Draw(tileTexture, tileRectangle, Color.White);
                }
            }
        }

        public int GetIndex(int cellX, int cellY)
        {
            //If the requested cell is out of bounds, return 0
            if (cellX < 0 || cellX > Width || cellY < 0 || cellY > Height)
                return 0;
            //Otherwise return the index of the cell.
            else
                try
                {
                    return map[cellY, cellX];
                }
                catch (IndexOutOfRangeException)
                {
                    return 0;
                }
        }
    }
}
