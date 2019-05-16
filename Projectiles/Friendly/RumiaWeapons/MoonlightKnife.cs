using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.RumiaWeapons
{
	public class MoonlightKnife : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Envy Knife";
            projectile.width = 14;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 3;  
            projectile.tileCollide = true;
            projectile.thrown = true;
			projectile.timeLeft = 100;
			
		}
		
		public override void AI()
		{
			Color? color = new Color(100,100,250);
			
			if (Main.rand.Next(4) == 1)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 0.5f;
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
	}
}