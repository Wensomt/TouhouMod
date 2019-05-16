using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.SmallShot
{
	public class SmallShotGreen : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Small Shot";
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 500;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            drawOffsetX = -1;
            drawOriginOffsetY = -1;
            drawOriginOffsetX = 1;
        }
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("GreenDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
			Main.dust[dust].scale = 0.5f;
		}
	}
}	