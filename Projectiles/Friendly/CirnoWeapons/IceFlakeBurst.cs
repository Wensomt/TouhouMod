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
	public class IceFlakeBurst : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Ice Burst";
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;   
            projectile.ranged = true;
			projectile.timeLeft = 60;
			projectile.alpha = 80;
			
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(100,100,250);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 0.1f;
			}
		}
		public override void AI()
		{
				projectile.velocity.Y += 0.2f;
				if(Main.rand.Next(5) == 1)
					CreateDust();
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
	
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y;
			}
			Main.PlaySound(SoundID.Item10, projectile.position);
			
			return false;
		}
	}
}
		