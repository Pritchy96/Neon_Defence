using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defence
{   
    public class Sprite
    {
        //Shared variables.
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 center;
        protected Vector2 origin;
        protected float rotation;

        private Rectangle bounds;

        //Getters & Setters.
        public Vector2 Center
        {
            get { return center; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Rectangle Bounds
        {
            get { return bounds; }
        } 

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;

            position = pos;
            velocity = Vector2.Zero;

            center = new Vector2(position.X + (texture.Width / 2), position.Y + (texture.Height / 2));
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            //Initialize rectange to fit around the Sprite.
            this.bounds = new Rectangle((int)position.X, (int)Position.Y,
            texture.Width, texture.Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            center = new Vector2(position.X + (texture.Width / 2), position.Y + (texture.Height / 2));
        }

        //Initial Draw method
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
           
        //'Tinted' Draw function, override.
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
