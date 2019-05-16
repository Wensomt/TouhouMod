using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.BigLightShot
{
	public class BigLightShotGreen : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Big Light Shot";
            projectile.width = 50;
            projectile.height = 50;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 600;
            drawOffsetX = -8;
            drawOriginOffsetY = -8;
            drawOriginOffsetX = 8;

        }
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("GreenDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
			Main.dust[dust].scale = 2.0f;
		}
	}
}	