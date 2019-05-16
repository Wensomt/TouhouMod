using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Marisa
{
	public class MasterSparkWarning : ModProjectile
	{
		private bool rotated = false;
		public override void SetDefaults(){
			//projectile.name = "Laser";
            projectile.width = 16;
            projectile.height = 48;
            projectile.hostile = true;
            projectile.penetrate = 12;
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 3;
            projectile.alpha = 200;

		}
        public override void AI()
        {
			if (!rotated)
            	projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			
			projectile.velocity *= 0f;
			rotated = true;
        }
    }
}