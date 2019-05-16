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
	public class MoonlightOrb : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Moonlight Orb";
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;
			projectile.magic = true;
			projectile.light = 0.5f;
			projectile.timeLeft = 600;
			
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
		public override void AI(){
			if (Main.rand.NextFloat() < .10f)
				CreateDust();
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