using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.RumiaWeapons
{
	public class MoonlightOrbEnhance : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Moonlight Orb Enhance";
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
			projectile.magic = true;
			projectile.light = 0.5f;
            projectile.damage = 0;
			projectile.timeLeft = 1680;
            projectile.alpha = 180;
			projectile.scale = 0.60f;
			
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(100,100,250);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 1.5f;
			}
		}
		public override void AI()
		{
			if (Main.rand.NextFloat() < .030f)
				CreateDust();
            Player owner = Main.player[projectile.owner];
            if (Math.Abs(projectile.position.X - owner.position.X) < 120f && Math.Abs(projectile.position.Y - owner.position.Y) < 120f)
			{
                owner.armorPenetration += 1;
				owner.AddBuff(mod.BuffType("MoonlightOrb"), 5, true);
			}
		}
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                CreateDust();
            }

        }
	}
}