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
    public class ShanghaiLaserEnd : ModProjectile
    {
        int timer = 0;
        Vector2 catchVelocity;
        public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 48;
			//projectile.name = "Shanghai Beam End";
			projectile.penetrate = -1;
			projectile.light = 1f;
			projectile.timeLeft = 50;
			projectile.friendly = true;
			projectile.tileCollide = false;
            projectile.scale = 0f;
		}
        public override void AI()
        {
            if (timer == 0)
            {
                catchVelocity.X = projectile.velocity.X;
                catchVelocity.Y = projectile.velocity.Y;
                projectile.velocity = new Vector2(0f,0f);
                projectile.scale = 1f;
            }
            projectile.rotation = (float)Math.Atan2((double)catchVelocity.Y, (double)catchVelocity.X) + 1.57f;
            if (Main.rand.Next(20) == 0)
			{
				Color? color = new Color(250,0,0);
				if (color.HasValue)
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 204, 0f, 0f, 0, color.Value, 0.75f);
					Main.dust[dust].velocity *= 1.5f;
				}
			}
            timer++;
        }
    }
}