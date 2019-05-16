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
	public class FistoftheNorthStarTrail : ModProjectile
	{
        private float rotationSpeed;

		public override void SetDefaults(){
			//projectile.name = "Fist of the North Star - Trail";
            projectile.width = 35;
            projectile.height = 35;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.melee = true;
			projectile.light = 1f;
			projectile.timeLeft = 200;
		}
        public override void AI()
        {
            if (rotationSpeed == 0)
                rotationSpeed = (float)Main.rand.NextDouble();
                
            projectile.rotation += rotationSpeed;
        }

    }
}