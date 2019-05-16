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
	public class EnvyBulbShot : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Bulb Shot";
            projectile.width = 25;
            projectile.height = 25;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;
			projectile.light = 0.5f;
			projectile.timeLeft = 200;
			
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
			if (Main.rand.Next(20) == 1)
				CreateDust();
			
			projectile.velocity.Y += 0.1f;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
			projectile.Kill();
			for (int i = 0; i < 3; i++)
				CreateDust();
			return false;
		}
		
		public override void Kill(int timeLeft)
		{
			if (projectile.velocity.Y > 0)
			{
				Player owner = Main.player[projectile.owner];
				int numberOfProjectiles = Main.rand.Next(2) + 3;
				for (int i = 0 ; i < numberOfProjectiles ; i++)
					Projectile.NewProjectile(projectile.position.X + (projectile.width/2), projectile.position.Y + (projectile.height/4), (float)(Main.rand.NextDouble() * 4 - 2.0), (float)((Main.rand.NextDouble() - 2) * 2), mod.ProjectileType("EnvyBulbShotBurst"), (projectile.damage / 2), 2, owner.whoAmI);
			}
			else
			{
				Player owner = Main.player[projectile.owner];
				int numberOfProjectiles = Main.rand.Next(2) + 3;
				for (int i = 0 ; i < numberOfProjectiles ; i++)
					Projectile.NewProjectile(projectile.position.X + (projectile.width/2), projectile.position.Y + (projectile.height/4), (float)(Main.rand.NextDouble() * 4 - 2.0), (float)((Main.rand.NextDouble() + 1)), mod.ProjectileType("EnvyBulbShotBurst"), (projectile.damage / 2), 2, owner.whoAmI);
			}
		}
	}
}