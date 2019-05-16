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
	public class FloweringShotParsee : ModProjectile
	{
		private int flowerInterval;
		private int timer;
		
		public override void SetDefaults(){
			//projectile.name = "Big Light Shot";
            projectile.width = 50;
            projectile.height = 50;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 200;
            drawOffsetX = -5;
            drawOriginOffsetY = -5;
            drawOriginOffsetX = 5;
        }
		public override void AI()
		{
            if (Main.netMode != 1)
            {
                timer++;
                if (flowerInterval == 0)
                    flowerInterval = Main.rand.Next(20) + 4;

                if (timer == flowerInterval)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TWINKLE"), 0.2f);
                    Projectile.NewProjectile(projectile.position.X + (projectile.width / 2) - 12 + Main.rand.Next(24), projectile.position.Y + (projectile.height / 2) - 12 + Main.rand.Next(24), 0f, 0f, mod.ProjectileType("FlowerShotParsee"), 12, 0f);
                    timer = 0;
                    flowerInterval = Main.rand.Next(20) + 4;
                }
            }
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("GreenDanmakuKillDust"), 0f, 0f, 0);
			Main.dust[dust].velocity *= 0f;
			Main.dust[dust].scale = 2.0f;
		}
	}
}	