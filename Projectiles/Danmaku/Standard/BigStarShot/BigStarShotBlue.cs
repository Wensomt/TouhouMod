using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.BigStarShot
{
	public class BigStarShotBlue : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Big Star Shot";
            projectile.width = 42;
            projectile.height = 42;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 600;
            drawOffsetX = -5;
            drawOriginOffsetY = -5;
            drawOriginOffsetX = 5;
            

        }
        public override void AI()
        {
            projectile.rotation -= 0.2f;
        }
        public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("BlueDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
            Main.dust[dust].scale = 2.0f;
		}
    }
}