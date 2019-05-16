using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Marisa
{
	public class StardustReverie : ModProjectile
	{
        private int timer = 0;
        private Vector2 catchSpeed;
        private int dustType = 0;
		public override void SetDefaults(){
			//projectile.name = "Star Shot";
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 900;
            Main.projFrames[projectile.type] = 6;
			
		}
        public override void AI()
        {
            timer++;
            if (timer == 1)
            {
                projectile.frame = projectile.damage;
                switch (projectile.damage)
                {
                    case 0:
                        dustType = mod.DustType("RedDanmakuKillDust");
                        break;
                    case 1:
                        dustType = mod.DustType("OrangeDanmakuKillDust");
                        break;
                    case 2:
                        dustType = mod.DustType("YellowDanmakuKillDust");
                        break;
                    case 3:
                        dustType = mod.DustType("GreenDanmakuKillDust");
                        break;
                    case 4:
                        dustType = mod.DustType("BlueDanmakuKillDust");
                        break;
                    case 5:
                        dustType = mod.DustType("PurpleDanmakuKillDust");
                        break;
                }
                projectile.damage = 23;
                catchSpeed = projectile.velocity;
                projectile.velocity = new Vector2(0f,0f);
            }
            if (timer == 120)
                projectile.velocity = catchSpeed / 2f;
            if (timer == 140)
                projectile.velocity = catchSpeed / 4f * 3f;
            if (timer == 160)
                projectile.velocity = catchSpeed / 8f * 7f;
            if (timer == 180)
                projectile.velocity = catchSpeed;
            if (timer == 900)
            {
                projectile.Kill();
            }
            projectile.rotation += 0.2f;
        }
        public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
    }
}