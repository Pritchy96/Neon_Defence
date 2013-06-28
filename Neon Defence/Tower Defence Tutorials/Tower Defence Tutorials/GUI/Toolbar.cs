using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defence_Tutorials
{
    public class Toolbar
    {
        private Texture2D texture;
        private SpriteFont font;

        //Position of toolbar & font.
        private Vector2 position;
        private Vector2 textPosition;

        public Toolbar(Texture2D texture, SpriteFont font, Vector2 position)
        {
            this.texture = texture;
            this.font = font;

            this.position = position;
            //Offset text to bottom right corner (Random values which work nicely are used).
            textPosition = new Vector2(20, position.Y + 15);
        }

        public void Draw(SpriteBatch spriteBatch, Player player, WaveManager waveManager)
        {
            spriteBatch.Draw(texture, position, Color.White);

            //The new String(' ', 125) is just inserting 125 spaces.
            string text = string.Format("Gold: {0} {1} Lives: {2} {3}  Wave: {4}", player.Money, new String(' ', 10), player.Lives, new String(' ', 115), waveManager.Round);
            spriteBatch.DrawString(font, text, textPosition, Color.White);
        }
    }
}
