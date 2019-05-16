using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.CirnoWeapons
{
	public class IceFlake : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Ice Flake";
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;   
            projectile.melee = true;
			projectile.light = 0.5f;
			projectile.timeLeft = 100;
			
		}
		
		public virtual void CreateDust()
		{
			Color? color = new Color(100,100,250);
			if (color.HasValue)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 1f;
			}
		}
		public override void AI()
		{
				projectile.velocity.Y += 0.1f;
				if(Main.rand.Next(2) == 1)
					CreateDust();
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
			projectile.Kill();
			
			return false;
		}
		public override void Kill(int timeLeft)
		{
			Vector2 perturbedSpeed = new Vector2(MathHelper.Lerp(-8f, 8f, (float)Main.rand.NextDouble()),MathHelper.Lerp(-8f,8f, (float)Main.rand.NextDouble()));;
			Player owner = Main.player[projectile.owner];
			int x = Main.rand.Next(2) + 2;
			for (int i = 0; i < x; i++)
			{
				perturbedSpeed = new Vector2(MathHelper.Lerp(-8f, 8f, (float)Main.rand.NextDouble()),MathHelper.Lerp(-8f,8f, (float)Main.rand.NextDouble()));
				Projectile.NewProjectile(projectile.position.X + (projectile.width/2), projectile.position.Y + (projectile.height/2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("IceFlakeBurst"), 16, 6, owner.whoAmI);
			}	
		
		}
	}
}