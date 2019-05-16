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
	public class SpectralKiller : ModProjectile
	{
        private int timer = 0;
        private int catchDamage = 0;
        private float maxHealth = 0;
        private int target = 0;
		public override void SetDefaults(){
			//projectile.name = "Spectral Killer";
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 400;
            projectile.alpha = 255;
		}
        public override void AI()
        {
            if (timer == 0)
            {
                catchDamage = projectile.damage;
                projectile.damage = 0;
            }
            if (timer < 40)
            {
                Vector2 speed = new Vector2(10f,10f).RotatedBy(timer * (6.28f / 40f));
                Projectile.NewProjectile(projectile.Center + speed * 4f, speed, mod.ProjectileType("SpectralKnife"), catchDamage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
                Projectile.NewProjectile(projectile.Center + speed.RotatedBy(3.14f) * 4f, speed.RotatedBy(3.14f), mod.ProjectileType("SpectralKnife"), catchDamage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
                projectile.Center = Main.player[projectile.owner].Center;
            }
            if (timer == 40)
            {
                for (int i = 0 ; i < Main.npc.Length ; i++)
                {
                    if (!Main.npc[i].friendly && Distance(projectile.Center, Main.npc[i].Center) < 1200f)
                    {
                        if (Main.npc[i].life > maxHealth)
                        {
                            target = i;
                            maxHealth = Main.npc[i].life;
                        }
                    }
                }
            }
            if (timer > 40 && target != 0)
            {
                projectile.Center = Main.npc[target].Center;
            }
            timer++;

        }
        private float Distance(Vector2 v1, Vector2 v2)
        {
            float dx = v1.X - v2.X;
            float dy = v1.Y - v2.Y;
            dx = (float)Math.Pow(dx,2);
            dy = (float)Math.Pow(dy,2);
            return (float)(Math.Sqrt(dx + dy));
        }
    }
}