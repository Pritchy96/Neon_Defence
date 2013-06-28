using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tower_Defence_Tutorials
{
    public class Laser : Tower
    {

        int maxNumberOfHits;
        List<Enemy> enemies;

        //Constructor.
        public Laser(Texture2D baseTexture, Texture2D upgradedTexture, Texture2D bulletTexture, Vector2 position)
            : base(baseTexture, upgradedTexture, bulletTexture, position)    //Inheriting the Tower class & providing its constructors.
        {
            //setting range, cost, damage.
            this.damage = 1;
            this.cost = 15;
            this.range = 80;
            this.RoF = 0.1f;
            this.maxNumberOfHits = 3;
        }


        //Upgrading the towers values.
        public override void Upgrade()
        {
            damage *= 2f;
            range *= 1.1f;
            base.Upgrade();
        }

        public override void Update(GameTime gameTime)
        {
            
            //If enough time has passed for the gun to fire again
            //and we have a target..
            if (bulletTimer >= RoF && target != null)
            {
                //create a bullet at the centre of the tower.
                LaserBullet bullet = new LaserBullet(bulletTexture, Vector2.Subtract(center,
                    new Vector2(bulletTexture.Width / 2)), rotation, 20, damage, maxNumberOfHits);

                //Get list of enemies for removing hit enemies.
               // enemies = base.;


                //Add bullet to list.
                bulletList.Add(bullet);
                //Restart the turret 'reload' time
                bulletTimer = 0;
            }

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i]; 
                
                //'bending' bullets toward enemys.
                bullet.SetRotation(rotation);
                bullet.Update(gameTime);


                //Does the bullet get close enough to the enemy to consider it a hit?
                if(target != null && Vector2.Distance(bullet.Center, target.Center) < 12)
                {
                    //if so, damage the enemy and destroy the bullet.
                    target.CurrentHealth -= bullet.Damage;
                   // bullet.recalculate();
                }
                // Removing bullet from the game. But not really.
                if (bullet.isDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
            base.Update(gameTime);
        }
    }
}