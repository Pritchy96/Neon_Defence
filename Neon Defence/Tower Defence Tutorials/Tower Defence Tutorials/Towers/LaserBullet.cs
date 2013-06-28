using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tower_Defence_Tutorials
{
    public class LaserBullet : Bullet
    {
        private float damage;
        private int speed;
        int maxNumberOfHits;


        //Getters and setters.
        public float Damage
        {
            get { return damage; }
        }


        //Constructor
        public LaserBullet(Texture2D texture, Vector2 position, float rotation, int speed, float damage, int maxNumberOfHits)
            : base(texture, position, rotation, speed, damage)
        {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;
            this.maxNumberOfHits = maxNumberOfHits;
        }

        public override void Update(GameTime gameTime)
        {

            position += velocity; //Changes position by adding the calculated velocity to it.  

            base.Update(gameTime);
        }


        public void SetRotation(float value)
        {
            rotation = value;

            //.Transform rotates the vector speed to have the same rotation as out tower.
            velocity = Vector2.Transform(new Vector2(0, -speed),
                Matrix.CreateRotationZ(rotation));
        }
    }
}
