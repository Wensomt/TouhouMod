using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.YuyukoWeapons
{
	public class YuyukoFanShot : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Fan Shot";
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;   
            projectile.magic = true;
			projectile.light = 1.5f;
			projectile.timeLeft = 600;
			
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(250,100,100);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
		public override void AI(){
			if (Main.rand.NextFloat() < .10f)
				CreateDust();
		}
	}
}