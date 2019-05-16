using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Parsee
{
	
	public class ScaleShotRandParsee : ModProjectile
	{
		private int timer = 0;
		private float speedX = 0f;
		private float speedY = 0f;
		
		private bool catchSpeed = false;
		private bool rotated = false;
		
		public override void SetDefaults(){
			//projectile.name = "Scale Shot";
            projectile.width = 25;
            projectile.height = 25;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 300;
			projectile.scale = 0.75f;
            drawOffsetX = -3;
            drawOriginOffsetY = -4;
            drawOriginOffsetX = 3;
        }
		public override void AI(){
				timer++;
			if(!rotated)
			{
				if(!catchSpeed)
				{
					speedX = projectile.velocity.X;
					speedY = projectile.velocity.Y;
					catchSpeed = true;
				}
				if(timer < 40)
				{
					projectile.rotation = (float)Math.Atan2((double)speedY, (double)speedX) + 1.57f;
				}
				if(timer > 20)
					projectile.velocity = new Vector2(0f,0f);
				if(timer > 40)
				{
					timer = 0;
					projectile.velocity = new Vector2(0.5f * speedX, 0.5f * speedY).RotatedByRandom(6.28);
					rotated = true;
				}
			}
			else
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("BlueDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}	