using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.RiceShot
{
	public class RiceShotOrange : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Rice Shot";
            projectile.width = 10;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 500;
            projectile.netImportant = true;
            projectile.netUpdate = true;
            drawOffsetX = -2;
            drawOriginOffsetY = -3;
            drawOriginOffsetX = 2;
        }
		public override void AI(){
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("OrangeDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}	