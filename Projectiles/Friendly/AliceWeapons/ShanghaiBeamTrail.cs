using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
 
namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
    public class ShanghaiBeamTrail : ModProjectile
    {
        int timer = 0;
        Vector2 catchVelocity;
        public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 48;
			//projectile.name = "Shanghai Beam Trail";
			projectile.penetrate = -1;
			projectile.light = 1f;
			projectile.timeLeft = 50;
			projectile.friendly = true;
			projectile.tileCollide = false;
            Main.projFrames[projectile.type] = 2;
            projectile.scale = 0f;
		}
        public override void AI()
        {
            if (timer == 0)
            {
                catchVelocity.X = projectile.velocity.X;
                catchVelocity.Y = projectile.velocity.Y;
                projectile.velocity = new Vector2(0f,0f);
            }
            if (timer < 2)
            {
                projectile.frame = 1;
                projectile.rotation = (float)Math.Atan2((double)catchVelocity.Y, (double)catchVelocity.X) + 4.71f;
                projectile.scale = 1f;
            }
            if (timer == 2)
            {
                projectile.frame = 0;
            }
            if (timer >= 48)
            {
                projectile.frame = 1;
                projectile.rotation = (float)Math.Atan2((double)catchVelocity.Y, (double)catchVelocity.X) + 1.57f;
            }
            timer++;
            if (Main.rand.Next(20) == 0)
			{
				Color? color = new Color(250,0,0);
				if (color.HasValue)
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 204, 0f, 0f, 0, color.Value, 0.75f);
					Main.dust[dust].velocity *= 1.5f;
				}
			}

        }
    }
}