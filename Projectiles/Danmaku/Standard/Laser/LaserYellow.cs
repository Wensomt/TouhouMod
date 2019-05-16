using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.Laser
{
	public class LaserYellow : ModProjectile
	{
		private bool rotated = false;
		public override void SetDefaults(){
			//projectile.name = "Laser";
            projectile.width = 16;
            projectile.height = 80;
            projectile.hostile = true;
            projectile.penetrate = 12;  
            projectile.tileCollide = false;
			projectile.light = 1.5f;
			projectile.timeLeft = 2;
            projectile.alpha = 0;
			Main.projFrames[projectile.type] = 2;
		}
        public override void AI()
        {
			if (!rotated)
			{
            	projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
				if (projectile.knockBack == 2f)
					projectile.frame = 1;
				else 
					projectile.frame = 0;

				projectile.knockBack = 0f;
			}
			
			projectile.velocity *= 0f;
			rotated = true;

			if (Main.rand.Next(40) == 0)
			{
				Color? color = new Color(250,250,0);
				if (color.HasValue)
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 204, 0f, 0f, 0, color.Value, 0.75f);
					Main.dust[dust].velocity *= 0.40f;
				}
			}
        }
    }
}