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
	
	
	public class LancingDoll : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 37;
			projectile.height = 91;
			//projectile.name = "Doll Lancer";
			projectile.penetrate = 1;
			projectile.light = 0f;
			projectile.timeLeft = 30;
			projectile.friendly = true;
			projectile.tileCollide = false;
            projectile.scale = 0.75f;
		}
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.damage != 0)
                Projectile.NewProjectile(projectile.Center, projectile.velocity, projectile.type, 0, 0f, Main.player[projectile.owner].whoAmI);
        }
    }
}