using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.ParseeWeapons
{
	public class GreenEyedShotFollow : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Green Eyed Shot Follow";
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;   
			projectile.light = 0.5f;
			projectile.timeLeft = 180;
			projectile.damage = 0;
			projectile.alpha = 255;
		}
		public override void AI()
		{
			Player owner = Main.player[projectile.owner];
			projectile.position.X = (int)(Main.mouseX + Main.screenPosition.X);
			projectile.position.Y = (int)(Main.mouseY + Main.screenPosition.Y);
			
			if (Main.rand.Next(3) == 1)
				Projectile.NewProjectile(projectile.position.X + (projectile.width / 2) - 32 + Main.rand.Next(64), projectile.position.Y + (projectile.height / 2) - 32 + Main.rand.Next(64), 0f, 0f, mod.ProjectileType("GreenEyedShotLarge"), 48, 2 , owner.whoAmI);
			if (Main.rand.Next(3) == 1)
				Projectile.NewProjectile(projectile.position.X + (projectile.width / 2) - 32 + Main.rand.Next(64), projectile.position.Y + (projectile.height / 2) - 32 + Main.rand.Next(64), 0f, 0f, mod.ProjectileType("GreenEyedShotSmall"), 24, 2 , owner.whoAmI);
			
		}
	}
}