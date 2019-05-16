using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
	
	
	public class DollScale : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 25;
			projectile.height = 25;
			projectile.minion = true;
			//projectile.name = "Doll Scale";
			projectile.penetrate = 1;
			projectile.light = 0f;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.tileCollide = true;
            projectile.scale = 0.75f;
		}
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}