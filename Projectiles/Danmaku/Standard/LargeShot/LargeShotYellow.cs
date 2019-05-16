using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.LargeShot
{
	public class LargeShotYellow : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Large Shot";
            projectile.width = 30;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 500;
			drawOffsetX = -2;
            drawOriginOffsetY = -2;
            drawOriginOffsetX = 2;
			projectile.netUpdate = true;
		}
		public override void AI()
		{
			
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("BlueDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}	