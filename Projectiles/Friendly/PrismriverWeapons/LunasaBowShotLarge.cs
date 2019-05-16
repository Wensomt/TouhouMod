using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.PrismriverWeapons
{
	public class LunasaBowShotLarge : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Lunasa Bow Shot Ex";
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.penetrate = 16;  
            projectile.tileCollide = true;   
            projectile.ranged = true;
			projectile.light = 1f;
			projectile.timeLeft = 200;
			
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(250,100,250);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 65, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
		public override void AI(){
			Vector2 perturbedSpeed = new Vector2(MathHelper.Lerp(-0.5f, 0.5f, (float)Main.rand.NextDouble()),MathHelper.Lerp(-0.5f,0.5f, (float)Main.rand.NextDouble()));
			Player owner = Main.player[projectile.owner];
			Projectile.NewProjectile(projectile.position.X + (projectile.width/2), projectile.position.Y + (projectile.height/2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("LunasaBowShotSmall"), 42, 6, owner.whoAmI);
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