using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Parsee
{
	public class ShrineDiamondParsee : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Diamond Shot";
            projectile.width = 10;
            projectile.height = 20;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;
			projectile.light = 1f;
			projectile.timeLeft = 130;
            drawOffsetX = -1;
            drawOriginOffsetY = -2;
            drawOriginOffsetX = 1;
        }
		public override void AI(){
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
		public override void Kill(int timeLeft)
		{
            if (Main.netMode != 1)
            {
                if (Math.Abs((double)projectile.velocity.X) > Math.Abs((double)projectile.velocity.Y))
                {
                    if (projectile.velocity.X > 0f)
                        Projectile.NewProjectile(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2), (float)((Main.rand.NextDouble() * -1 - 0.25) * 2), (float)((Main.rand.NextDouble() - 0.5) * 2), mod.ProjectileType("GhostlyShotBlue"), 17, 0f);
                    if (projectile.velocity.X < 0f)
                        Projectile.NewProjectile(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2), (float)((Main.rand.NextDouble() + 0.25) * 2), (float)((Main.rand.NextDouble() - 0.5) * 2), mod.ProjectileType("GhostlyShotBlue"), 17, 0f);

                }
                else
                {
                    if (projectile.velocity.Y > 0f)
                        Projectile.NewProjectile(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2), (float)((Main.rand.NextDouble() - 0.5) * 2), (float)((Main.rand.NextDouble() * -1 - 0.25) * 2), mod.ProjectileType("GhostlyShotBlue"), 17, 0f);
                    if (projectile.velocity.Y < 0f)
                        Projectile.NewProjectile(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2), (float)((Main.rand.NextDouble() - 0.5) * 2), (float)((Main.rand.NextDouble() - 0.25) * 2), mod.ProjectileType("GhostlyShotBlue"), 17, 0f);

                }
            }
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("YellowDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
			
		}
	}
}	