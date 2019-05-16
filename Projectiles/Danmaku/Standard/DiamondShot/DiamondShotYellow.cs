using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.DiamondShot
{
	public class DiamondShotYellow : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Diamond Shot";
            projectile.width = 10;
            projectile.height = 20;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 500;
            drawOffsetX = -1;
            drawOriginOffsetY = -2;
            drawOriginOffsetX = 1;
        }
		public override void AI(){
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("YellowDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}	