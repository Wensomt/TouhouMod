using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.SakuyaWeapons
{
	public class SpectralKnife : ModProjectile
	{
        private int timer = 0;
        private Vector2 catchSpeed = new Vector2();
		public override void SetDefaults(){
			//projectile.name = "Spectral Knife";
            projectile.width = 10;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;
			projectile.light = 0.25f;
			projectile.timeLeft = 100;
            projectile.scale = 1.25f;
			
		}
		public override void AI(){
            if (timer == 0)
            {
                catchSpeed.X = projectile.velocity.X;
                catchSpeed.Y = projectile.velocity.Y;
                projectile.velocity = new Vector2(0f,0f);
            }
            if (timer < 20)
            {
			    projectile.rotation = (float)Math.Atan2((double)catchSpeed.Y, (double)catchSpeed.X) + 1.57f + (timer * (6.28f / 20f));
            }
            if (timer == 20)
            {
                projectile.rotation = (float)Math.Atan2((double)catchSpeed.Y, (double)catchSpeed.X) + 1.57f;
                projectile.velocity = catchSpeed;
            }
            for (int i = 0 ; i < Main.projectile.Length ; i++)
            {
                if (Main.projectile[i].hostile && Main.projectile[i].damage > 0 && Contains(Main.projectile[i].Center))
                    Main.projectile[i].Kill();
            }
            timer++;
		}
        public override void Kill(int timeLeft)
        {
            Vector2 target = new Vector2();
            for (int i = 0 ; i < Main.projectile.Length ; i++)
            {
                if (Main.projectile[i].type == mod.ProjectileType("SpectralKiller"))
                    target = Main.projectile[i].Center;
            }
            Vector2 speed = new Vector2(-28f,0f);
            speed = speed.RotatedBy((float)Math.Atan2((double)projectile.Center.Y - target.Y, (double)projectile.Center.X - target.X));
            Projectile.NewProjectile(projectile.Center, speed, mod.ProjectileType("SpectralKnifeReturn"), projectile.damage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
        }
        public bool Contains(Vector2 placement)
		{
			if (placement.X > projectile.position.X && placement.X < projectile.position.X + 30f && placement.Y > projectile.position.Y && placement.Y < projectile.position.Y + 30f)
				return true;
			return false;
		}
	}
}