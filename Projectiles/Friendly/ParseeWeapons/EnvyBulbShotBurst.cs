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
	public class EnvyBulbShotBurst : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Bulb Shot Burst";
            projectile.width = 25;
            projectile.height = 25;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;   
			projectile.timeLeft = 200;
			projectile.scale = 0.33f;
			
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(100,250,100);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 44, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
		public override void AI(){
			projectile.velocity.Y += 0.1f;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
			projectile.Kill();
			for (int i = 0; i < 2; i++)
				CreateDust();
			return false;
		}
	}
}