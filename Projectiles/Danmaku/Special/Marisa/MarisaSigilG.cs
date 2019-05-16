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
	public class MarisaSigilG : ModProjectile
	{
        private static int type = 0;
        private int timer = 0;
        private float angle = 0f;
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
            if (timer == 0)
            {
                type = Main.rand.Next(5);
                angle = MathHelper.ToRadians(270 - 7 + Main.rand.Next(14));
            }
            if (Main.netMode != 1)
            {
                if (timer == 40)
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BEAM"), 0.3f);
                if (timer > 40 && timer <= 70)
                {
                    float laserSize = 0.4f + ((timer - 40) * 0.02f);
                    NewLaserWarning(projectile.Center.X, projectile.Center.Y, angle, type, laserSize, 40);
                }
            }
            timer++;
            if (Main.netMode != 1)
            {
                if (timer > 70 && timer < 180)
                    NewLaser(projectile.Center.X, projectile.Center.Y, angle, type, 28, 0f, 40);

                if (timer >= 180 && timer <= 190)
                {
                    float laserSize = 1f - ((timer - 180) * 0.06f);
                    NewLaserWarning(projectile.Center.X, projectile.Center.Y, angle, type, laserSize, 40);
                }
            }
        }
        public void NewLaser(float positionX, float positionY, float angle, int LaserType, int damage, float knockback, int laserLength)
        {
            Vector2 aim = new Vector2(36f,0f).RotatedBy(angle);
            int type = 0;
            switch(LaserType)
            {
                case 0:
                    type = mod.ProjectileType("LaserRed");
                    break;
                case 1:
                    type = mod.ProjectileType("LaserYellow");
                    break;
                case 2:
                    type = mod.ProjectileType("LaserGreen");
                    break;
                case 3:
                    type = mod.ProjectileType("LaserBlue");
                    break;
                case 4:
                    type = mod.ProjectileType("LaserPurple");
                    break;
				case 5:
					type = mod.ProjectileType("MasterSparkWarning");
					aim = new Vector2(-24f,0f).RotatedBy(angle);
					break;
                default:
                    type = mod.ProjectileType("LaserRed");
                    break;
            }
            for (int i = 2 ; i < laserLength ; i++)
            {
				if (i == 2)
                	Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , damage , 2f);
				else
					Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , damage , knockback);
            }

        }
		public void NewLaserWarning(float positionX, float positionY, float angle, int LaserType, float laserSize, int laserLength)
		{
			Vector2 aim = new Vector2(36f,0f).RotatedBy(angle);
            int type = 0;
            switch(LaserType)
            {
                case 0:
                    type = mod.ProjectileType("RedLaserWarning");
                    break;
                case 1:
                    type = mod.ProjectileType("YellowLaserWarning");
                    break;
                case 2:
                    type = mod.ProjectileType("GreenLaserWarning");
                    break;
                case 3:
                    type = mod.ProjectileType("BlueLaserWarning");
                    break;
                case 4:
                    type = mod.ProjectileType("PurpleLaserWarning");
                    break;
                default:
                    type = mod.ProjectileType("RedLaserWarning");
                    break;
            }
			for (int i = 2 ; i < laserLength ; i++)
            {
				if (i == 2)
                	Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , 1 , laserSize);
				else
					Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , 0 , laserSize);
            }

		}
    }
}