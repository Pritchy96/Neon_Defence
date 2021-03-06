﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tower_Defence
{
    public class Sniper : Tower
    {
        //Constructor.
        public Sniper(Texture2D baseTexture, Texture2D upgradedTexture, Texture2D bulletTexture, Vector2 position)
            : base(baseTexture, upgradedTexture, bulletTexture, position)    //Inheriting the Tower class & providing it's constructors.
        {
            //setting range, cost, damage.
            this.damage = 15;
            this.cost = 50;
            this.range = 110;
            this.RoF = 1f;
        }


        //Upgrading the towers values.
        public override void Upgrade()
        {
            base.damage *= 1.5f;
            base.range *= 1.5f;
            base.RoF *= 1.2f;
            base.Upgrade();
        }

        public override void Update(GameTime gameTime)
        {

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
                Bullet bullet = bulletList[i];

                //'bending' bullets toward enemys.
                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                //If the bullet is out of the range of the tower, kill it.
                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                //Does the bullet get close enough to the enemy to consider it a hit?
                if (target != null && Vector2.Distance(bullet.Center, target.Center) < 12)
                {
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
            }
            base.Update(gameTime);
        }
    }
}