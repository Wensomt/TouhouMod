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
	public class Talisman : ModProjectile
	{
		private bool rotated = false;
		
		public override void SetDefaults(){
//projectile.name = "Talisman Card";
            projectile.width = 14;
            projectile.height = 19;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 100;
			projectile.alpha = 160;
			
		}
		public override void AI(){
			if (!rotated)
			{
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
				rotated = true;
			}
			else
			{
				projectile.rotation += 0.0982f;
			}
				
		}
		
	}
}	