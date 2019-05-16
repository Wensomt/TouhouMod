using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.Misc
{
	public class MagicMissile : ModProjectile
	{
		
		public override void SetDefaults(){
			//projectile.name = "Magic Missle";
            projectile.width = 20;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 100;
			projectile.alpha = 50;
			
		}
		public override void AI(){
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                projectile.velocity.X *= 1.05f;
                projectile.velocity.Y *= 1.05f;
		}
		
	}
}	