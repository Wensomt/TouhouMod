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
	public class EerieScaleYellow : ModProjectile
	{
        private int timer = 0;
        private Vector2 catchSpeed;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scale Shot");
		}		
		public override void SetDefaults(){
            projectile.width = 25;
            projectile.height = 25;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 600;
            projectile.scale = 0.75f;
            drawOffsetX = -3;
            drawOriginOffsetY = -4;
            drawOriginOffsetX = 3;
        }
        public override void AI()
        {
            timer++;
            if (timer == 1)
            {
                catchSpeed.X = projectile.velocity.X;
                catchSpeed.Y = projectile.velocity.Y;
                projectile.rotation = (float)Math.Atan2((double)catchSpeed.Y, (double)catchSpeed.X) + 1.57f;
            }
            if (timer == 60)
            {
                projectile.velocity = new Vector2(0f,0f);
            }
            if (timer == 70)
            {
                projectile.velocity = catchSpeed.RotatedBy(MathHelper.ToRadians(160));
                catchSpeed.X = projectile.velocity.X;
                catchSpeed.Y = projectile.velocity.Y;
                projectile.rotation = (float)Math.Atan2((double)catchSpeed.Y, (double)catchSpeed.X) + 1.57f;
            }
            if (timer == 90)
            {
                projectile.velocity = new Vector2(0f,0f);
            }
            if (timer == 100)
            {
                projectile.velocity = catchSpeed.RotatedBy(MathHelper.ToRadians(120));
                catchSpeed.X = projectile.velocity.X;
                catchSpeed.Y = projectile.velocity.Y;
                projectile.rotation = (float)Math.Atan2((double)catchSpeed.Y, (double)catchSpeed.X) + 1.57f;
            }
        }
        public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("YellowDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}