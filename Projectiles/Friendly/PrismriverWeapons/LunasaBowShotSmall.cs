using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.PrismriverWeapons
{
	public class LunasaBowShotSmall : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Lunasa Bow Shot";
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;   
            projectile.ranged = true;
			projectile.light = 0.5f;
			projectile.timeLeft = 200;
			
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
			projectile.Kill();
			return false;
		}
	}
}