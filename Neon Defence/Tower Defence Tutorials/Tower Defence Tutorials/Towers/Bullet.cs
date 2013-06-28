using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tower_Defence_Tutorials
{
    public class Bullet : Sprite
    {
        private float damage;
        private int age;
        private int speed;


        //Getters and setters.
        public float Damage
        {
            get { return damage; }
        }

        //if age is > 100, dead = true
        public bool isDead()
        {
            return age > 100;
        }


        //Constructor
        public Bullet(Texture2D texture, Vector2 position, float rotation, int speed, float damage)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;
        }

        //Another constuctor, taking a velocity instead of getting one from a rotation.
        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity, int speed, float damage)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;

            this.velocity = velocity * speed;
        }

        public override void Update(GameTime gameTime)
        {
            //Ages the bullet, so it does not live forever, if it misses its target.
            age++;  

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


        //kills the bullet.
        public void Kill()
        {
            this.age = 200;
        }
    }
}
