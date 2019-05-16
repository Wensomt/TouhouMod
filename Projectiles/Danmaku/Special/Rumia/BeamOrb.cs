using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Rumia
{

	public class BeamOrb: ModNPC
	{
		private int attackTimer = 0;
		private float speedY = 0f;
		private float speedX = 0f;
		private float angle = 0f;
		public override void SetDefaults()
		{
			//npc.name = "BeamOrb";
			//npc.displayName = "Beam Orb";
			npc.aiStyle = -1;
			npc.lifeMax = 1;
			npc.damage = 0;
			npc.defense = 8;
			npc.knockBackResist = 0f;
			npc.height = 30;
			npc.height = 30;
			Main.npcFrameCount[npc.type] = 1;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 15f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.timeLeft = 200;
			npc.friendly = true;
			npc.alpha = 122;
            npc.netUpdate = true;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = 1;
			npc.damage = 0;
		}
		public override void AI()
		{
            
                npc.TargetClosest(true);
                Player target = Main.player[npc.target];
                Color? color = new Color(100, 100, 250);
            if (Main.netMode != 1)
            {
                if (attackTimer == 30)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BEAM"), 0.2f);
                    angle = (float)Math.Atan2((double)npc.Center.Y - target.Center.Y, (double)npc.Center.X - target.Center.X) + 3.14f;
                }
                if (attackTimer > 30 && attackTimer < 80)
                {
                    float size = 0.4f + ((attackTimer - 30) * 0.012f);
                    NewLaserWarning(npc.Center.X, npc.Center.Y, angle, 3, size, 40);
                }
                if (attackTimer > 80 && attackTimer < 180)
                {
                    NewLaser(npc.Center.X, npc.Center.Y, angle, 3, 20, 0f, 40);
                }
                if (attackTimer > 180)
                {
                    float size = 1.0f - ((attackTimer - 180) * 0.03f);
                    NewLaserWarning(npc.Center.X, npc.Center.Y, angle, 3, size, 40);
                }
            }
                if (attackTimer > 200)
                    npc.life = 0;

                attackTimer++;
            
			return;
		}
		public void NewLaser(float positionX, float positionY, float angle, int LaserType, int damage, float knockback, int laserLength)
        {
            Vector2 aim = new Vector2(30f,0f).RotatedBy(angle);
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
			Vector2 aim = new Vector2(30f,0f).RotatedBy(angle);
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