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
	public class MarisaSigilB : ModProjectile
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
			projectile.timeLeft = 240;
            projectile.alpha = 60;
		}
        public override void AI()
        {
            projectile.rotation += 0.1f;
            timer++;
            if (type == 0)
            {
                int x = Main.rand.Next(6);
                switch (x)
                {
                    case 0:
                        type = mod.ProjectileType("RedStarShot");
                        break;
                    case 1:
                        type = mod.ProjectileType("OrangeStarShot");
                        break;
                    case 2:
                        type = mod.ProjectileType("YellowStarShot");
                        break;
                    case 3:
                        type = mod.ProjectileType("GreenStarShot");
                        break;
                    case 4:
                        type = mod.ProjectileType("BlueStarShot");
                        break;
                    case 5:
                        type = mod.ProjectileType("PurpleStarShot");
                        break;
                }
            }
            projectile.damage = 0;
            if (Main.netMode != 1)
            {
                if (timer % 5 == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.RotatedBy(MathHelper.ToRadians(160)).X / 2.5f, projectile.velocity.RotatedBy(MathHelper.ToRadians(160)).Y / 2.5f, type, 24, 0f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.RotatedBy(MathHelper.ToRadians(-160)).X / 2.5f, projectile.velocity.RotatedBy(MathHelper.ToRadians(-160)).Y / 2.5f, type, 24, 0f);
                    //Projectile.NewProjectile(projectile.position.X + projectile.width/2 , projectile.position.Y + projectile.height/2, projectile.velocity.RotatedBy(MathHelper.ToRadians(120)).X / 2.5f, projectile.velocity.RotatedBy(MathHelper.ToRadians(120)).Y / 2.5f , type, 24, 0f);
                    Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, projectile.velocity.RotatedBy(MathHelper.ToRadians(-120)).X / 2.5f, projectile.velocity.RotatedBy(MathHelper.ToRadians(-120)).Y / 2.5f, type, 24, 0f);
                }
            }
            projectile.velocity = projectile.velocity.RotatedBy(-8f / 120f);
        }
    }
}