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
	public class FlowerShotParsee : ModProjectile
	{
		private bool rotated = false;
		
		public override void SetDefaults(){
			//projectile.name = "Flower Shot";
            projectile.width = 25;
            projectile.height = 25;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 400;
            drawOffsetX = -2;
            drawOriginOffsetY = -2;
            drawOriginOffsetX = 2;
        }
		public override void AI()
		{
			if (!rotated)
				projectile.rotation = (float)(Main.rand.NextDouble() * 6.28);
		
			rotated = true;
		}
		public override void Kill(int timeLeft)
		{
			for(int i = 0 ; i < 4 ; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("PinkDanmakuKillDust"), 0f, 0f, 0);
				Main.dust[dust].velocity *= 0f;
			}
		}
	}
	
}	