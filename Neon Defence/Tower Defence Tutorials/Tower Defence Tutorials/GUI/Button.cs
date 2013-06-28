using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defence_Tutorials
{

    // Describes the state of the button
    public enum ButtonStatus
    {
        Normal,
        MouseOver,
        Pressed,
    }

    public class Button : Sprite
    {
        //Mousestate of the last frame
        private MouseState previousState;
        //The different textures.
        private Texture2D hoverTexture;
        private Texture2D pressedTexture;
        //A rectangle to cover the button.
        private Rectangle bounds;
        //Status of button.
        private ButtonStatus state = ButtonStatus.Normal;
        //Event Handler for clicks.
        public event EventHandler Clicked;
        //Event Handler for when the button is held down.
        public event EventHandler OnPress;
        

        //Constructor
        public Button(Texture2D texture, Texture2D hoverTexture, Vector2 position)
            : base(texture, position)
        {
            this.hoverTexture = hoverTexture;

            //Initialize rectange to fit around the button.
            this.bounds = new Rectangle((int)position.X, (int)Position.Y,
            texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            //Determine if mouseover button.
            //Updating mouse and keys state.
            MouseState mouseState = Mouse.GetState();
            KeyboardState keysState = Keyboard.GetState();
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;

            //If the mouse is within the bounds rectangle, mouseover = true.
            bool isMouseOver = bounds.Contains(mouseX, mouseY);

            //Updating button state.
            if (isMouseOver && state != ButtonStatus.Pressed)
            {
                state = ButtonStatus.MouseOver;
            }
            else if (isMouseOver == false && state != ButtonStatus.Pressed)
            {
                state = ButtonStatus.Normal;
            }

            //Check if player presses and holds down button.
            if (mouseState.LeftButton == ButtonState.Pressed &&
                previousState.LeftButton == ButtonState.Released)
            {
                //If the player mouse is over the button..
                if (isMouseOver)
                {
                    if (OnPress != null)
                    {
                        //Fire the onPress event.
                        OnPress(this, EventArgs.Empty);
                    }
                }
                else if (state == ButtonStatus.Pressed)
                {
                    state = ButtonStatus.Normal;
                }
            }

            // Check if the player releases the button.
            if (mouseState.LeftButton == ButtonState.Released &&
             previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    // update the button state.
                    state = ButtonStatus.MouseOver;

                    // Firing an event, if the event has information.
                    if (Clicked != null)
                    {
                        Clicked(this, EventArgs.Empty);
                    }
                }
                    else if (state == ButtonStatus.Pressed)
                    {
                        state = ButtonStatus.Normal;
                    }
                }

                //Setting old state to current one, ready to update the current one.
                previousState = mouseState;


            }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ButtonStatus.Normal:
                    spriteBatch.Draw(texture, bounds, Color.White);
                    break;

                case ButtonStatus.MouseOver:
                    spriteBatch.Draw(hoverTexture, bounds, Color.White);
                    break;

                default:
                    spriteBatch.Draw(texture, bounds, Color.White);
                    break;
            }  
        }
    }
}
