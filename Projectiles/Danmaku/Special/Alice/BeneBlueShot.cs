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
	public class BeneBlueShot : ModProjectile
	{
        private int timer = 0;
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
			projectile.timeLeft = 300;
			projectile.scale = 0.75f;
            drawOffsetX = -3;
            drawOriginOffsetY = -4;
            drawOriginOffsetX = 3;
        }
		public override void AI(){
            timer++;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (timer == 60)
            {
                int numberofProjectiles = 7;
                float angle = MathHelper.ToRadians(120);
                float deltaAngle = angle / numberofProjectiles;
                Vector2 speed = projectile.velocity.RotatedBy(-deltaAngle*3 + MathHelper.ToRadians(180));
                if (Main.netMode != 1)
                {
                    for (int i = 0; i < numberofProjectiles; i++)
                    {
                        speed = speed.RotatedBy(deltaAngle);

                        if (i % 2 == 1)
                            Projectile.NewProjectile(projectile.position, speed * 0.75f, mod.ProjectileType("BeneWhiteShot"), projectile.damage, 0f);
                        else
                            Projectile.NewProjectile(projectile.position, speed, mod.ProjectileType("BeneWhiteShot"), projectile.damage, 0f);
                    }
                }
                projectile.Kill();
            }
		}
        public override void Kill(int timeLeft)
		{
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TWINKLE"), 0.3f);
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("BlueDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
		}
	}
}	