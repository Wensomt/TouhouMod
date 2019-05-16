using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Rumia
{
	
	public class RotatingRiceRedR : ModProjectile
	{
		private int timer = 0;
		private float speedX = 0f;
		private float speedY = 0f;
		
		public override void SetDefaults(){
			//projectile.name = "Rice Shot";
            projectile.width = 10;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 300;
            drawOffsetX = -2;
            drawOriginOffsetY = -3;
            drawOriginOffsetX = 2;
        }
		public override void AI(){
			timer++;
			projectile.rotation = (float)Math.Atan2((double)speedY, (double)speedX) + 1.57f;
			if (speedX == 0f && speedY == 0f)
			{
				speedX = projectile.velocity.X;
				speedY = projectile.velocity.Y;
			}
			if (timer == 50)
			{
				projectile.velocity.X = 0;
				projectile.velocity.Y = 0;
			}
			if (timer == 80)
			{
				projectile.velocity = new Vector2(speedX,speedY).RotatedBy(MathHelper.ToRadians(-60));
				speedX = 0f;
				speedY = 0f;
			}
			
			
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("RedDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}	