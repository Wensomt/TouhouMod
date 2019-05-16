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
	public class Slash : ModProjectile
	{
		
		public override void SetDefaults(){
			//projectile.netImportant = true;
			//projectile.name = "Doll";
			projectile.width = 57;
			projectile.height = 19;
			Main.projFrames[projectile.type] = 4;
			projectile.friendly = true;
			//Main.projPet[projectile.type] = true;
			//projectile.minion = true;
			//projectile.minionSlots = 1;
			//projectile.penetrate = 1;
			projectile.timeLeft = 40;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.scale = 2f;
			//ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			//ProjectileID.Sets.Homing[projectile.type] = true;
			//inertia = 20f;
			//shoot = mod.ProjectileType("DollScale");
			//shootSpeed = 12f;
			projectile.alpha = 50;
			projectile.light = 0.1f;
			projectile.penetrate = 100;
		}
		public override void AI(){
			projectile.ai[0] += 1f;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;		
		    if (++projectile.frameCounter >= 10)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
			if (projectile.ai[0] >= 40f)
			{
				projectile.Kill();
			}
		}
		
	}
}	