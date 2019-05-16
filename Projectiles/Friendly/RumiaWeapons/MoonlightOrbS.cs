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
	public class MoonlightOrbS : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Moonlight Orb";
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;
			projectile.light = 0.5f;
			projectile.timeLeft = 300;
			
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(100,100,250);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
			projectile.Kill();
			for (int i = 0; i < 10; i++)
				CreateDust();
			return false;
		}
	}
}