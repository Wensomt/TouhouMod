using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.GhostlyShot
{
	public class GhostlyShotGreen : ModProjectile
	{
		private int timer = 0;
		public override void SetDefaults(){
			//projectile.name = "Ghostly Shot";
            projectile.width = 20;
            projectile.height = 20;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 1200;
			projectile.alpha = 60;
            drawOffsetX = -2;
            drawOriginOffsetY = -2;
            drawOriginOffsetX = 2;
        }
		public override void AI(){
			timer++;
			if (timer % 10 == 0)
				projectile.rotation = (float)(Main.rand.NextDouble() * 6.28);
		}
	}
}	