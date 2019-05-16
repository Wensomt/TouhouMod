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
	public class MarisaSigilH : ModProjectile
	{
        private int type = 0;
        private int timer = 0;
		public override void SetDefaults(){
			//projectile.name = "Sigil";
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 200;
            projectile.alpha = 60;
		}
        public override void AI()
        {
            projectile.rotation += 0.1f;
            timer++;
            projectile.damage = 0;
            if (Main.netMode != 1)
            {
                if (timer % 7 == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.RotatedBy(MathHelper.ToRadians(140)).X * 3f, projectile.velocity.RotatedBy(MathHelper.ToRadians(140)).Y * 3f, mod.ProjectileType("YellowStarShot"), 24, 0f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.RotatedBy(MathHelper.ToRadians(0)).X * 3f, projectile.velocity.RotatedBy(MathHelper.ToRadians(0)).Y * 3f, mod.ProjectileType("YellowStarShot"), 24, 0f);
                }
            }
            projectile.velocity = projectile.velocity.RotatedBy(-1f / 20f);
        }
    }
}