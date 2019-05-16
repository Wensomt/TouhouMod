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
	public class FallingStar : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "ThrowingStar";
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;   
            projectile.thrown = true;
            projectile.timeLeft = 400;
            projectile.alpha = 50;
		}
        public override void AI()
        {
            projectile.velocity.Y += 0.05f;
            projectile.rotation += 0.1f;
        }
    }
}