using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.DarkAlchemistEasyWeapons
{
	public class HomingEnergy : ModProjectile
	{
		private int timer = 0;
		public override void SetDefaults(){
            projectile.CloneDefaults(316);
            aiType = 316;
			//projectile.name = "Homing Energy";
            projectile.width = 15;
            projectile.height = 15;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
			projectile.light = 0.1f;
			projectile.timeLeft = 300;
			projectile.alpha = 100;
            Main.projFrames[projectile.type] = 3;
			projectile.scale = 3f;
			
		}
		public override void AI()
        {
            if (timer % 10 == 0)
                projectile.frame = Main.rand.Next(3);	
		}
		
	}
}	