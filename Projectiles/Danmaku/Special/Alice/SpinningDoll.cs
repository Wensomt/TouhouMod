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
    public class SpinningDoll : ModProjectile
    {
        private bool attacking = false;
        private int catchDamage = 0;
        private int timer = 0;
 		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doll");
		}      
	   public override void SetDefaults()
		{
			projectile.width = 25;
			projectile.height = 25;
			projectile.penetrate = -1;
			projectile.light = 0f;
			projectile.timeLeft = 2;
			projectile.hostile = true;
			projectile.tileCollide = false;
            projectile.alpha = 100;
		}
        public override void AI()
        {
            if (attacking)
            {
                projectile.rotation += 0.1f;
                timer++;
                if (timer % 20 == 0 && timer > 59)
                    Attack();
            }
            else
                projectile.rotation = projectile.knockBack;
            if (projectile.damage > 0)
            {
                projectile.timeLeft = 100;
                catchDamage = projectile.damage;
                projectile.damage = 0;
                attacking = true;
            }
        }
        public void Attack()
        {
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
            for (int i = 0 ; i < 4 ; i++)
            {
                float offset = (float)Main.rand.NextDouble();
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(2.5f, 0f).RotatedBy(MathHelper.ToRadians(i * 90) + offset), mod.ProjectileType("RiceShotBlue"), catchDamage, 0f);
                }
            }
        }
    }
}