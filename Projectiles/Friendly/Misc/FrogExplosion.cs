using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.Misc
{
	public class FrogExplosion : ModProjectile
	{
		
		public override void SetDefaults(){
			//projectile.name = "Wily Toad Explosion";
            projectile.width = 160;
            projectile.height = 160;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 10;
			projectile.alpha = 255;
			
		}
		public override void AI(){
            for (int i = 0; i < 3; i++)
            {
                CreateDust();
            }
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(100,250,100);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 18, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 1.2f;
			}
		}
	}
}