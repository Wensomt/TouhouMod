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
	public class RainbowLaser : ModProjectile
	{
		
		public override void SetDefaults(){
			//projectile.name = "Rainbow Laser";
            projectile.width = 2;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
			projectile.light = 0.1f;
			projectile.timeLeft = 100;
			projectile.alpha = 50;
			projectile.scale = 2f;
			
		}
		public override void AI(){
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;	
		}
		
	}
}	