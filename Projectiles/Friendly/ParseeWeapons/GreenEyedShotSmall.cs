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
	public class GreenEyedShotSmall : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Green Eyed Shot Small";
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 3;  
            projectile.tileCollide = false;   
			projectile.light = 0.25f;
			projectile.timeLeft = 720;
		}
		public override void AI()
		{
			for (int i = 0 ; i < Main.projectile.Length ; i++)
			{
				if (Main.projectile[i].hostile && Main.projectile[i].damage > 0 && Contains(Main.projectile[i].Center))
				{
					Main.projectile[i].Kill();
					projectile.Kill();
				}
			}
		}
		public bool Contains(Vector2 placement)
		{
			if (placement.X > projectile.position.X && placement.X < projectile.position.X + 10f && placement.Y > projectile.position.Y && placement.Y < projectile.position.Y + 10f)
				return true;
			return false;
		}
	}
}