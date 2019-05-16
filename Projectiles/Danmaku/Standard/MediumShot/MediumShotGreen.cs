using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.MediumShot
{
	public class MediumShotGreen : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Medium Shot";
            projectile.width = 20;
            projectile.height = 20;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 500;
            projectile.netImportant = true;
            projectile.netUpdate = true;
            drawOffsetX = -3;
            drawOriginOffsetY = -3;
            drawOriginOffsetX = 3;
        }
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("GreenDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}	