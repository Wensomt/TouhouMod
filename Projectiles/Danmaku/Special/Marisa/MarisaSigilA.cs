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
	public class MarisaSigilA : ModProjectile
	{
        private int type = 0;
        private int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sigil");
	      }		
		public override void SetDefaults(){
            projectile.width = 32;
            projectile.height = 32;
            projectile.hostile = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = false;
			projectile.light = 1f;
			projectile.timeLeft = 550;
            projectile.alpha = 60;
		}
        public override void AI()
        {
            projectile.rotation += 0.1f;
            timer++;
            if (type == 0)
            {
                if (projectile.damage == 0)
                    type = mod.ProjectileType("RedStarShot");
                if (projectile.damage == 1)
                    type = mod.ProjectileType("OrangeStarShot");
                if (projectile.damage == 2)
                    type = mod.ProjectileType("YellowStarShot");
                if (projectile.damage == 3)
                    type = mod.ProjectileType("GreenStarShot");
                if (projectile.damage == 4)
                    type = mod.ProjectileType("BlueStarShot");
                if (projectile.damage == 5)
                    type = mod.ProjectileType("PurpleStarShot");
            }
            projectile.damage = 0;
            if (Main.netMode != 1)
            {
                if (timer % 36 == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.RotatedBy(MathHelper.ToRadians(90)).X / 2f, projectile.velocity.RotatedBy(MathHelper.ToRadians(90)).Y / 2f, type, 24, 0f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, type, 24, 0f);
                }
            }
            projectile.velocity = projectile.velocity.RotatedBy(-5f / 120f);
        }
    }
}