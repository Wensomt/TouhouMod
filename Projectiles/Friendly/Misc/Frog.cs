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
	public class Frog : ModProjectile
	{
		
		public override void SetDefaults(){
			//projectile.name = "Wily Toad";
            projectile.width = 16;
            projectile.height = 16;
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
        public override void Kill(int timeLeft)
        {
            Player owner = Main.player[projectile.owner];
            Projectile.NewProjectile(projectile.position.X + (projectile.width/2), projectile.position.Y + (projectile.height/2), 0f, 0f, mod.ProjectileType("FrogExplosion"), projectile.damage, 6, owner.whoAmI);
        }
		
	}
}