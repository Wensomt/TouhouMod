using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.StarShot
{
	public class GreenStarShot : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Star Shot";
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 600;
            drawOffsetX = -2;
            drawOriginOffsetY = -2;
            drawOriginOffsetX = 2;
        }
        public override void AI()
        {
            projectile.rotation += 0.2f;
        }
        public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("GreenDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
    }
}