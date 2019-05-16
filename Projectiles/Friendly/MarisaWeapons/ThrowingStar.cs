using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.MarisaWeapons
{
	public class ThrowingStar : ModProjectile
	{
		private int timer;
		public override void SetDefaults(){
			//projectile.name = "ThrowingStar";
            projectile.width = 40;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.penetrate = 16;
            projectile.tileCollide = true;   
            projectile.thrown = true;
			projectile.light = 1f;
            projectile.timeLeft = 400;
		}
        public override void AI()
		{
			timer++;
			if (timer % 5 == 0)
			{
				Player owner = Main.player[projectile.owner];
				Vector2 speed = new Vector2(0.5f, 0f).RotatedByRandom(3.14f) * (Main.rand.NextFloat() / 4f + 1f);
				Projectile.NewProjectile(projectile.Center, speed, mod.ProjectileType("FallingStar"), projectile.damage / 2, 0f, owner.whoAmI);
			}
			projectile.rotation += 0.1f;

			projectile.velocity.Y += 0.05f;
		}
    }
}