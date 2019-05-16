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
	public class MarisaSigilC : ModProjectile
	{
        private int timer = 0;
        private int type;
		public override void SetDefaults(){
			//projectile.name = "Sigil";
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 800;
            projectile.alpha = 60;
		}
        public override void AI()
        {
            projectile.rotation += 0.1f;
            timer++;
            if (type == 0)
            {
                type = projectile.damage;
                projectile.damage = 0;
            }
            if (Main.netMode != 1)
            {
                if (timer % 30 == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.RotatedBy(MathHelper.ToRadians(-90)).X / 2.25f, projectile.velocity.RotatedBy(MathHelper.ToRadians(-90)).Y / 2.25f, mod.ProjectileType("StardustReverie"), type, 0f);
                }
            }
            projectile.velocity = projectile.velocity.RotatedBy(-2f / 120f);
            projectile.velocity *= 1.0012f;
        }
    }
}