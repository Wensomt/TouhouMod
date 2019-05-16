using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Alice
{
	public class EerieBigLightLeft : ModProjectile
	{
        private int timer = 0;
        private Vector2 catchSpeed;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Big Light Shot");
		}			
		public override void SetDefaults(){
            projectile.width = 50;
            projectile.height = 50;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 600;
            drawOffsetX = -5;
            drawOriginOffsetY = -5;
            drawOriginOffsetX = 5;
        }
        public override void AI()
        {
            timer++;
            if (timer == 40)
            {
                catchSpeed.X = projectile.velocity.X;
                catchSpeed.Y = projectile.velocity.Y;
                projectile.velocity = new Vector2(0f,0f);
            }
            if (timer == 60)
            {
                projectile.velocity = catchSpeed.RotatedBy(MathHelper.ToRadians(-90));
            }
            if (timer > 60 && timer < 80)
            {
                projectile.velocity = projectile.velocity.RotatedBy(-4.5f/120f);
            }
        }
        public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("PurpleDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
            Main.dust[dust].scale = 2.0f;
		}
	}
}