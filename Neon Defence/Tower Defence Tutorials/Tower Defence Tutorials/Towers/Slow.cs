using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tower_Defence_Tutorials
{
    public class Slow : Tower
    {
        //How fast an enemy will move when hit.
        private float speedModifier;
        //How long the effect will last.
        private float modifierDuration;

        public Slow(Texture2D baseTexture, Texture2D upgradedTexture, Texture2D bulletTexture, Vector2 position)
            : base(baseTexture, upgradedTexture, bulletTexture, position)    //Inheriting the Tower class, providing it's constructors.
        {
            //setting range, cost, damage.
            this.damage = 0;
            this.cost = 30;
            this.range = 60;
            this.RoF = 0.5f;
            this.speedModifier = 0.2f;
            this.modifierDuration = 1f;
        }

        public override void GetClosestEnemy(List<Enemy> enemies)
        {
            target = null;
            float smallestRange = range;

            //Loops through the enemies, if the current enemy is closer to the tower
            //than the last closest enemy and has no modifier, that is set as the target. 
            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(center, enemy.Center) < smallestRange && enemy.SpeedModifier == 0)
                {
                    smallestRange = Vector2.Distance(center, enemy.Center);
                    target = enemy;
                }
                
            }
        }

        //Upgrading the towers values.
        public override void Upgrade()
        {
            range *= 1.2f;
            RoF *= 1.1f;
            base.Upgrade();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //If enough time has passed for the gun to fire again
            //and we have a target..
            if (bulletTimer >= RoF && target != null)
            {
                //create a bullet at the centre of the tower.
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center,
                    new Vector2(bulletTexture.Width / 2)), rotation, 20, damage);

                //Add bullet to list.
                bulletList.Add(bullet);
                //Restart the turret 'reload' time
                bulletTimer = 0;
            }
            

            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];  //?
                
                //'bending' bullets toward enemys.
                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                //If the bullet is out of the range of the tower, kill it.
                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                //Does the bullet get close enough to the enemy to consider it a hit?
                if(target != null && Vector2.Distance(bullet.Center, target.Center) < 12)
                {
                    //If the speed modifier is better than anything affecting the target, apply it.
                    if (target.SpeedModifier <= speedModifier)
                    {
                        target.SpeedModifier = speedModifier;
                        target.SpeedModifierDuration = modifierDuration;
                    }
                    //if so, damage the enemy and destroy the bullet.
                    target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }

                // Removing bullet from the game. But not really.
                if (bullet.isDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }

                try
                {
                    if (target.SpeedModifier > 0)
                    {
                        target = null;
                    }
                }
                catch (NullReferenceException)
                {
                }
                 
            }
        }
    }
}