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
	
	
	public class HouraiScale : ModProjectile
	{
        private Vector2 catchSpeed;
        private int timer;
		public override void SetDefaults()
		{
			projectile.width = 25;
			projectile.height = 25;
			//projectile.name = "Hourai Scale";
			projectile.penetrate = 1;
			projectile.light = 0f;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.tileCollide = true;
            projectile.scale = 0.75f;
		}
        public override void AI()
        {
            timer++;
            if (timer < 30)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                catchSpeed.X = projectile.velocity.X;
                catchSpeed.Y = projectile.velocity.Y;
            }
            else if (timer == 30)
            {
                projectile.velocity = new Vector2(0f,0f);
            }
            else if (timer == 70)
            {
                projectile.velocity = catchSpeed.RotatedBy(MathHelper.ToRadians(90));
            }
            else if (timer > 70 && timer < 117)
            {
                projectile.velocity = projectile.velocity.RotatedBy(2f/30f);
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }

        }
    }
}