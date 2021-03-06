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
	public class Snake : ModProjectile
	{
		
		public override void SetDefaults(){
            projectile.CloneDefaults(316);
            aiType = 316;
			//ProjectileID.Sets.Homing[projectile.type] = true;
			//projectile.name = "Wind Snake";
            projectile.width = 5;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 300;
			projectile.alpha = 100;
			
		}
		public override void AI(){

				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
				
		}
		
	}
}	