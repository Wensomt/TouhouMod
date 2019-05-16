using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.ParseeWeapons
{
	public class EnvyKnife : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Envy Knife";
            projectile.width = 14;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;   
            projectile.thrown = true;
			projectile.timeLeft = 200;
			projectile.aiStyle = 2;
			
		}
		
		public override void AI()
		{
			Color? color = new Color(100,250,100);
			
			if (Main.rand.Next(24) == 1)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 44, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 0.5f;
			}
			
		}
	}
}