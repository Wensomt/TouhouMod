using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.MarisaWeapons
{
	public class Hakkero : ModProjectile
	{
		
		private int timer = 0;
		private bool b = false;
		private int d = 0;
        private Vector2 catchSpeed;
		
		public override void SetDefaults(){
			//projectile.name = "Hakkero";
            projectile.width = 20;
            projectile.height = 44;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 2;
		}
        public override void AI()
        {
            if (projectile.frame == 0 && timer % 5 == 0)
				projectile.frame = 1;
			else if (timer % 5 == 0)
				projectile.frame = 0;

            
            if (d == 0)
			{
				d = projectile.damage;
                catchSpeed = projectile.velocity;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
				projectile.velocity.Y = 0f;
                projectile.velocity.X = 0f;
                projectile.damage = 0;
			}

            timer++;
            Player owner = Main.player[projectile.owner];
			int numberOfProjectiles = 2;
            Vector2 newSpeed;
			for (int i = 0 ; i < numberOfProjectiles ; i++)
            {
                if (timer < 90)
                {
                    numberOfProjectiles = 2;
                    newSpeed = catchSpeed.RotatedByRandom(MathHelper.ToRadians(timer / 5));
                }
                else
                {
                    numberOfProjectiles = 4;
                    newSpeed = catchSpeed.RotatedByRandom(MathHelper.ToRadians(18));
                }

                	Projectile.NewProjectile(projectile.position.X + (projectile.width/2) + newSpeed.X * 3f, projectile.position.Y + newSpeed.Y * 3f + (projectile.height/2), newSpeed.X, newSpeed.Y, mod.ProjectileType("MasterSpark"), d, 12, owner.whoAmI);
            }
            Main.PlaySound(SoundID.Item9, projectile.position);
            if (timer == 250)
            {
                projectile.Kill();
            }

        }
    }
}