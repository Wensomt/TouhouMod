using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.NPCs.Bosses.IN.Marisa
{
[AutoloadBossHead]
	public class Marisa : ModNPC
	{

        private bool plus = false;
        private int moveType = 0;
		private int moveTimer = 0;
		private int attackType = 0;
		private int attackTimer = 0;
		private int attackTimerB = 0;
		private float targetPosX;
		private float targetPosY;
		private float lastPlayerPosX = 0;
		private float lastPlayerPosY = 0;
		private float intialDistance;
		private bool moving = true;
		private bool attacking = false;
		private bool moveComplete;
		private bool attackComplete = true;
		private bool[] startedStage = new bool[8];
		private bool AAA = false;
		private bool AAB = false;
		private int aType = 0;
        private bool CMS = false; //Custom Movement Stage
		private float Loffset = 0; //Laser Offset
		private float catchOffset; //temp value for Laser offset
		private float catchOffsetB;
		private bool trueShiftCenter = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marisa");
			//Tooltip.SetDefault("Ordinary Western Magician, Marisa");
	    }
        public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 72000;
			npc.damage = 0;
			npc.defense = 35;
			npc.knockBackResist = 0f;
			npc.width = 104;
			npc.height = 118;
			Main.npcFrameCount[npc.type] = 5;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 30f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.scale = 0.75f;
            npc.netUpdate = true;
			if (Main.expertMode)
					{
                    music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/MarisaEx");
					}
					else
					{
					music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Marisa");
					}
			//music = MusicID.Boss2;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = 64000 + numPlayers * 4000;
			npc.damage = 0;
		}
        public override void HitEffect(int hitDirection, double damage)
		{
				Color? color = new Color(100,100,250);
				for (int i = 0 ; i < 3 ; i++)
				{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 21, 0f, 0f, 0, color.Value);
					Main.dust[dust].velocity *= 4f;
				}
				if (npc.life <= 0)
				{
					NPC.NewNPC((int)npc.position.X + (npc.width/2), (int)npc.position.Y + npc.height, mod.NPCType("MarisaDeath"));
				}
		}
        public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = mod.ItemType("GreaterHarukeiBlessing");

			if (Main.expertMode)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MarisaTreasureBag"));
			else
			{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MarisaTrophy"));
			}
			int x = Main.rand.Next(5);
			for (int i = 0; i < 2; i++)
			{
				if (x == 0)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("FistoftheNorthStar"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 1)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("StarDuster"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 2)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("StarBurstStaff"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 3)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("StarryNight"));
					x += Main.rand.Next(4) + 1;
					if (x > 4)
						x -= 5;
				}
				else if (x == 4)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("ThrowingStar"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
				}
			}
			x = (Main.rand.Next(6) + 4);
			for (int i = 0 ; i <= x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("StarDust"));
			}
		}
		private float Distance(Vector2 v1, Vector2 v2)
        {
            float dx = v1.X - v2.X;
            float dy = v1.Y - v2.Y;
            dx = (float)Math.Pow(dx,2);
            dy = (float)Math.Pow(dy,2);
            return (float)(Math.Sqrt(dx + dy));
        }
        public override void AI()
		{
			
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			
			player.AddBuff(mod.BuffType("MarisaPresence"), 60, true);
			if (Distance(npc.Center , player.Center) > 960f && npc.life <= (npc.lifeMax / 10000) * 9999)
				player.AddBuff(mod.BuffType("faithPunish"), 50, true);
			
			
			
			if (player.dead /*|| Main.dayTime*/)
			{
				npc.TargetClosest(false);
				player = Main.player[npc.target];
				if (player.dead || !player.active || Main.dayTime)
				{
					npc.velocity = new Vector2(0f, -24f);
					if (npc.timeLeft > 10)
					{
						npc.timeLeft = 10;
					}
					return;
				}
			}
			
			if (Main.expertMode)
			{
				if (npc.life <= (npc.lifeMax / 9)) // 1/9 life on expert
				{
					EarthLightRay(player);
					if(startedStage[7] == false)
					{
						StartStage(player);
						startedStage[7] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 2)) // 2/9 life on expert
				{
					MasterSpark(player);
					if(startedStage[6] == false)
					{
						StartStage(player);
						startedStage[6] = true;
					}
					
				}
				else if (npc.life <= (npc.lifeMax / 3)) // 3/9 life on expert
				{
					StandardAttackD(player);
					if(startedStage[5] == false)
					{
						StartStage(player);
						startedStage[5] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 4)) // 4/9 life on expert
				{
					NondirectionalLaser(player);
					if(startedStage[4] == false)
					{
						StartStage(player);
						startedStage[4] = true;
						attackTimer = 11;
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 5)) // 5/9 life on expert
				{
					StandardAttackC(player);
					if(startedStage[3] == false)
					{
						StartStage(player);
						startedStage[3] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 3 * 2)) // 6/9 life on expert
				{
					StardustReverie(player);
					if(startedStage[2] == false)
					{
						trueShiftCenter = true;
						StartStage(player);
						startedStage[2] = true;
					
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 7)) // 7/9 life on expert
				{
					StandardAttackB(player);
					if(startedStage[1] == false)
					{
						StartStage(player);
						startedStage[1] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 8)) // 8/9 life on expert
				{
					MilkyWay(player);
					if(startedStage[0] == false)
					{
						StartStage(player);
						startedStage[0] = true;
					}
				}
				else // over 8 / 9 life on expert
				{
					StandardAttackA(player);
				}
				
				
			}
			else
			{
				if (npc.life <= (npc.lifeMax / 8)) // 1/8 life on normal
				{
					EarthLightRay(player);
					if(startedStage[6] == false)
					{
						StartStage(player);
						startedStage[6] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 4)) // 2/8 life on normal
				{
					StandardAttackD(player);
					if(startedStage[5] == false)
					{
						StartStage(player);
						startedStage[5] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 8 * 3)) // 3/8 life on normal
				{
					NondirectionalLaser(player);
					if(startedStage[4] == false)
					{
						StartStage(player);
						startedStage[4] = true;
						attackTimer = 11;
					}
				}
				else if (npc.life <= (npc.lifeMax / 2)) // 4/8 life on normal
				{
					StandardAttackC(player);
					if(startedStage[3] == false)
					{
						StartStage(player);
						startedStage[3] = true;
					}
				}		
				else if (npc.life <= (npc.lifeMax / 8 * 5)) // 5/8 life on normal
				{
					StardustReverie(player);
					if(startedStage[2] == false)
					{
						trueShiftCenter = true;
						StartStage(player);
						startedStage[2] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 4 * 3)) // 6/8 life on normal
				{
					StandardAttackB(player);
					if(startedStage[1] == false)
					{
						StartStage(player);
						startedStage[1] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 8 * 7)) // 7/8 life on normal
				{
					MilkyWay(player);
					if(startedStage[0] == false)
					{
						StartStage(player);
						startedStage[0] = true;
					}
				}
				else // over 7/8 life normal
				{
					StandardAttackA(player);
				}
			
			}
			
			
			return;
			
		}
        //Movement and Attack Animation
		public void ShiftCenter(float offset , Player player)
		{

            if (plus)
            {
                targetPosX = player.position.X + (float)0.5 * offset;
                plus = false;
            }
            else
            {
                targetPosX = player.position.X - (float)0.5 * offset;
                plus = true;
            }
            if (plus)
            {
                targetPosY = player.position.Y + (float)0.5 * 64f - 360f;
                plus = false;
            }
            else
            {
                targetPosY = player.position.Y - (float)0.5 * 64f - 360f;
                plus = true;
            }

            if (trueShiftCenter)
			{
				targetPosY = player.position.Y - (float)Main.rand.NextDouble() * 32f - 330f;
				trueShiftCenter = false;
			}

			
			intialDistance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY),2) + Math.Pow((npc.position.X - targetPosX),2)));
			
			npc.velocity.X = (targetPosX - npc.position.X) / 50f;
			npc.velocity.Y = (targetPosY - npc.position.Y) / 50f;
			
			moveComplete = false;
		}
		public void ShiftLeft(float mag, float offset, Player player)
		{
			if (plus)
            {
                targetPosX = player.position.X + (float)0.5 * offset - mag;
                plus = false;
            }
            else
            {
                targetPosX = player.position.X - (float)0.5 * offset - mag;
                plus = true;
            }
            if (plus)
            {
                targetPosY = player.position.Y + (float)0.5 * 64f - 360f;
                plus = false;
            }
            else
            {
                targetPosY = player.position.Y - (float)0.5 * 64f - 360f;
                plus = true;
            }
			
			intialDistance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY),2) + Math.Pow((npc.position.X - targetPosX),2)));
			
			npc.velocity.X = (targetPosX - npc.position.X) / 50f;
			npc.velocity.Y = (targetPosY - npc.position.Y) / 50f;
			
			moveComplete = false;
		}
		public void ShiftRight(float mag, float offset, Player player)
		{
            if (plus)
            {
                targetPosX = player.position.X + (float)0.5 * offset + mag;
                plus = false;
            }
            else
            {
                targetPosX = player.position.X - (float)0.5 * offset + mag;
                plus = true;
            }
            if (plus)
            {
                targetPosY = player.position.Y + (float)0.5 * 64f - 360f;
                plus = false;
            }
            else
            {
                targetPosY = player.position.Y - (float)0.5 * 64f - 360f;
                plus = true;
            }

            intialDistance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY),2) + Math.Pow((npc.position.X - targetPosX),2)));
			
			npc.velocity.X = (targetPosX - npc.position.X) / 50f;
			npc.velocity.Y = (targetPosY - npc.position.Y) / 50f;
				
			moveComplete = false;
			
		}
		public void Move()
		{
			float distance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY),2) + Math.Pow((npc.position.X - targetPosX),2)));

			if (npc.position.X >= targetPosX - 16f && npc.position.X <= targetPosX + 16f && npc.position.Y >= targetPosY - 16f && npc.position.Y <= targetPosY + 16f)
			{
				npc.velocity.X = 0;
				npc.velocity.Y = 0;
				moveComplete = true;
			}
			
		}
		public void AA() //Attack Animation
		{
			if (aType == 0 || aType == 2)
			{
				AAA = true;
				AAB = false;
				aType++;
			}
			else if (aType == 1)
			{
				AAA = false;
				AAB = true;
				aType++;
			}
			else if (aType == 3)
			{
				AAA = false;
				AAB = false;
				aType = 0;
			}
		}
        public void StartStage(Player player)
		{
			Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SPELLCARD"), 0.25f);

			moving = false;
			attacking = true;
			moveComplete = true;
			attackComplete = false;
			attackTimer = 0;
			attackType = 0;
			moveType = 0;
			moveTimer = 0;
			AAA = false;
			AAB = false;
            lastPlayerPosX = 0;
            lastPlayerPosY = 0;
			aType = 0;

			int x = Main.rand.Next(2) + 3;
			if (Main.expertMode)
				x+= Main.rand.Next(3);

			for (int i = 0 ; i < x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PointItemD"));

			x = Main.rand.Next(2) + 3;
			if (Main.expertMode)
				x+= Main.rand.Next(3);

			for (int i = 0 ; i < x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PowerItemD"));

			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePointItemD"));
			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePowerItemD"));
			
			Color? color = new Color(100,100,250);
			for (int i = 0 ; i < 8 ; i++)
			{
				int dust = Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value , 4f);
				Main.dust[dust].velocity *= 12f;
			}
			
			ShiftCenter(0f , player);
			for (int k = 0 ; k < Main.projectile.Length ; k++)
			{
				if (Main.projectile[k].hostile)
				{
					Main.projectile[k].Kill();
				}
			}


		}
		public void NewLaser(float positionX, float positionY, float angle, int LaserType, int damage, float knockback, int laserLength)
        {
            Vector2 aim = new Vector2(32f,0f).RotatedBy(angle);
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
            for (int i = 3 ; i < laserLength ; i++)
            {
				if (i == 3)
                	Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , damage , 2f);
				else
					Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , damage , knockback);
            }

        }
		public void NewLaserWarning(float positionX, float positionY, float angle, int LaserType, float laserSize, int laserLength)
		{
			Vector2 aim = new Vector2(32f,0f).RotatedBy(angle);
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
			for (int i = 3 ; i < laserLength ; i++)
            {
				if (i == 3)
                	Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , 1 , laserSize);
				else
					Projectile.NewProjectile(positionX + aim.X * i, positionY + aim.Y * i, aim.X , aim.Y , type , 0 , laserSize);
            }

		}
        //Attack Stages
        public void StandardAttackA(Player target)
		{
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 3)
				{
					ShiftLeft(100f,16f, target);
					moveType = -1;
					moving = false;
					attacking = true;
				}
				else if (moveType == 2)
				{
					ShiftCenter(16f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 1)
				{
					ShiftRight(100f,16f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					ShiftCenter(16f, target);
					moving = false;
					attacking = true;
				}
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 600 || attackTimer == 605)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 25)
                    {
                        Vector2 radius = new Vector2(120f, 0f);
                        Vector2 speed = new Vector2(0f, -5f);
                        for (int i = 0; i != 6; i++)
                        {
                            Projectile.NewProjectile(npc.position.X + npc.width / 2 + radius.X, npc.position.Y + npc.height / 2 + radius.Y, speed.X, speed.Y, mod.ProjectileType("MarisaSigilA"), i, 0f);

                            radius = radius.RotatedBy(MathHelper.ToRadians(60));
                            speed = speed.RotatedBy(MathHelper.ToRadians(60));
                        }
                    }
                }
				if (attackTimer == 620)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
			}	
        }
        public void MilkyWay(Player target)
        {
			Move();
			attackTimer++;
            if (Main.netMode != 1)
            {
                if (attackTimer % 40 == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);

                    Vector2 firstShot = new Vector2(7f, 7f).RotatedBy(MathHelper.ToRadians((float)(attackTimer / 1.66)));
                    float speedX = firstShot.X;
                    float speedY = firstShot.Y;
                    float numberProjectiles = 7;
                    float rotation = MathHelper.ToRadians(180);
                    if (attackType == 0)
                    {
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotRed"), 28, 0f);
                        }
                        attackType = 1;
                    }
                    else if (attackType == 1)
                    {
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotPurple"), 28, 0f);
                        }
                        attackType = 0;
                    }
                }
                if (attackTimer % 60 == 0)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                    Projectile.NewProjectile(target.position.X + 900, target.position.Y + 128 - Main.rand.Next(128 * 3), -2.5f, 0f, mod.ProjectileType("GreenStarShot"), 24, 0f);
                }
                else if (attackTimer % 60 == 40)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                    Projectile.NewProjectile(target.position.X - 900, target.position.Y + 128 - Main.rand.Next(128 * 3), 2.5f, 0f, mod.ProjectileType("YellowStarShot"), 24, 0f);
                }
                if (attackTimer % 150 == 0)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.3f);
                    if (Main.rand.Next(2) == 0)
                        Projectile.NewProjectile(target.position.X + 900, target.Center.Y, -2.5f, 0f, mod.ProjectileType("GreenStarShot"), 24, 0f);
                    else
                        Projectile.NewProjectile(target.position.X - 900, target.Center.Y, 2.5f, 0f, mod.ProjectileType("YellowStarShot"), 24, 0f);
                }
            }

        }
        public void StandardAttackB(Player target)
        {
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 3)
				{
					ShiftLeft(300f,16f, target);
					moveType = -1;
					moving = false;
					attacking = true;
				}
				else if (moveType == 2)
				{
					ShiftCenter(16f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 1)
				{
					ShiftRight(300f,16f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					ShiftCenter(16f, target);
					moving = false;
					attacking = true;
				}
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 300 || attackTimer == 305)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 25)
                    {
                        Vector2 radius = new Vector2(120f, 0f);
                        Vector2 speed = new Vector2(0f, -8f);
                        Projectile.NewProjectile(npc.position.X + npc.width / 2 + radius.X, npc.position.Y + npc.height / 2 + radius.Y, speed.X, speed.Y, mod.ProjectileType("MarisaSigilB"), 2, 0f);
                    }
                    if (attackTimer % 80 == 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                        Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), new Vector2(-3f, 0f).RotatedBy((float)Math.Atan2((double)npc.Center.Y - target.Center.Y, (double)npc.Center.X - target.Center.X)).X, new Vector2(-3f, 0f).RotatedBy((float)Math.Atan2((double)npc.position.Y - target.position.Y, (double)npc.position.X - target.position.X)).Y, mod.ProjectileType("BigStarShotBlue"), 28, 0f);
                    }
                }
				if (attackTimer == 320)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
			}
        }
		public void StardustReverie(Player target)
		{
			Move();
			if (moveComplete)
			{
				attackTimer++;
			}
			if (attackTimer == 10 || attackTimer == 15)
				AA();
            if (Main.netMode != 1)
            {
                if (attackTimer % 900 == 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                    Vector2 radius = new Vector2(120f, 0f);
                    Vector2 speed = new Vector2(0f, -4f);
                    for (int i = 0; i != 6; i++)
                    {
                        Projectile.NewProjectile(npc.position.X + npc.width / 2 + radius.X, npc.position.Y + npc.height / 2 + radius.Y, speed.X, speed.Y, mod.ProjectileType("MarisaSigilC"), i, 0f);

                        radius = radius.RotatedBy(MathHelper.ToRadians(60));
                        speed = speed.RotatedBy(MathHelper.ToRadians(60));
                    }
                }
            }
		}
		public void StandardAttackC(Player target)
		{
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 3)
				{
					ShiftLeft(200f,14f, target);
					moveType = -1;
					moving = false;
					attacking = true;
				}
				else if (moveType == 2)
				{
					ShiftCenter(14f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 1)
				{
					ShiftRight(200f,14f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					ShiftCenter(14f, target);
					moving = false;
					attacking = true;
				}
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 600 || attackTimer == 605)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 25)
                    {
                        Vector2 radius = new Vector2(80f, 0f);
                        Vector2 speed = new Vector2(0f, -12f);
                        for (int i = 0; i != 6; i++)
                        {
                            Projectile.NewProjectile(npc.position.X + npc.width / 2 + radius.X, npc.position.Y + npc.height / 2 + radius.Y, speed.X, speed.Y, mod.ProjectileType("MarisaSigilD"), i, 0f);

                            radius = radius.RotatedBy(MathHelper.ToRadians(60));
                            speed = speed.RotatedBy(MathHelper.ToRadians(60));
                        }
                    }
                }
				if (attackTimer == 620)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
			}
		}
		public void NondirectionalLaser(Player target)
		{
			Move();
			if (moveComplete)
			{
                if (Main.netMode != 1)
                {
                    float angle = 0;

                    float laserSize = 0f;
                    if (attackTimer % 360 == 329)
                    {
                        catchOffset = Loffset;
                        Loffset = MathHelper.ToRadians(Main.rand.Next(5) * 72);
                    }
                    if (attackTimer % 360 == 0)
                    {
                        catchOffsetB = catchOffset;
                        catchOffset = Loffset;

                    }
                    attackTimer++;
                    for (int i = 0; i < 5; i++)
                    {
                        //Main Lasers
                        if (attackTimer % 360 < 180)
                            angle = catchOffset + MathHelper.ToRadians((float)(125 + i * 72 - attackTimer / 3.60));
                        else if (attackTimer % 360 > 180)
                            angle = catchOffset + MathHelper.ToRadians((float)(55 + (i - 1) * 72 + attackTimer / 3.60));
                        NewLaser(npc.position.X + npc.width / 2, npc.position.Y + npc.height / 2, angle, i, 28, 1f, 36);

                        if (attackTimer == 150 || attackTimer == 330)
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BEAM"), 0.3f);

                        //Laser Warnings
                        if (attackTimer % 360 >= 150 && attackTimer % 360 <= 180)
                        {
                            angle = catchOffset + MathHelper.ToRadians((float)(123.0 - 8.333333333 + i * 72.0 - 90.0 + (attackTimer - 150) / 3.60));
                            laserSize = 0.4f + ((attackTimer - 150) * 0.02f);
                            NewLaserWarning(npc.Center.X, npc.Center.Y, angle, i, laserSize, 36);
                        }
                        if (attackTimer % 360 >= 330)
                        {
                            angle = Loffset + MathHelper.ToRadians((float)(53 + 8.3333333333 + (i + 1) * 72 - (attackTimer - 330) / 3.60));
                            laserSize = 0.4f + ((attackTimer - 330) * 0.02f);
                            NewLaserWarning(npc.Center.X, npc.Center.Y, angle, i, laserSize, 36);
                        }
                        //Laser Diminish
                        if (attackTimer % 360 >= 180 && attackTimer % 360 <= 190)
                        {
                            angle = catchOffset + MathHelper.ToRadians((float)(75 + (i) * 72 - (attackTimer - 180) / 3.60));
                            laserSize = 1f - ((attackTimer - 180) * 0.06f);
                            NewLaserWarning(npc.Center.X, npc.Center.Y, angle, i, laserSize, 36);
                        }
                        if (attackTimer % 360 <= 10)
                        {
                            angle = catchOffsetB + MathHelper.ToRadians((float)(83 + (i) * 72 + attackTimer / 3.6));
                            laserSize = 1f - (attackTimer * 0.06f);
                            NewLaserWarning(npc.Center.X, npc.Center.Y, angle, i, laserSize, 36);
                        }
                    }
                }
				if (attackTimer == 359)
					attackTimer = 0;
				if (attackTimer % 32 == 0)
				{
                    if (Main.netMode != 1)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);

                        Vector2 firstShot = new Vector2(8f, 8f).RotatedBy(MathHelper.ToRadians((float)(attackTimer * 1.2)));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 7;
                        float rotation = MathHelper.ToRadians(180);
                        if (attackType == 0)
                        {
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RedStarShot"), 24, 0f);
                            }
                            attackType = 1;
                        }
                        else if (attackType == 1)
                        {
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("PurpleStarShot"), 24, 0f);
                            }
                            attackType = 0;
                        }
                    }
					
				}
                if (Main.netMode != 1)
                {
                    attackTimerB++;
                    if (attackTimerB % 200 == 0)
                    {
                        //Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.2f);
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 placement = new Vector2(60f, 0f).RotatedBy(MathHelper.ToRadians(120 * i));
                            placement.X += npc.position.X + npc.width / 2;
                            placement.Y += npc.position.Y + npc.height / 2;
                            Vector2 speed = new Vector2(-2f, 0f).RotatedBy((float)Math.Atan2((double)placement.Y - target.position.Y, (double)placement.X - target.position.X)).RotatedByRandom(MathHelper.ToRadians(60));
                            Projectile.NewProjectile(placement.X, placement.Y, speed.X, speed.Y, mod.ProjectileType("BlueStarShot"), 24, 0f);
                        }
                    }
                    else if (attackTimerB % 200 == 100)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 placement = new Vector2(60f, 0f).RotatedBy(MathHelper.ToRadians(120 * i + 60));
                            placement.X += npc.position.X + npc.width / 2;
                            placement.Y += npc.position.Y + npc.height / 2;
                            Vector2 speed = new Vector2(-2f, 0f).RotatedBy((float)Math.Atan2((double)placement.Y - target.position.Y, (double)placement.X - target.position.X)).RotatedByRandom(MathHelper.ToRadians(60));
                            Projectile.NewProjectile(placement.X, placement.Y, speed.X, speed.Y, mod.ProjectileType("YellowStarShot"), 24, 0f);
                        }
                    }
                }
				
			}
		}
		public void StandardAttackD(Player target)
		{
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 3)
				{
					ShiftLeft(250f,16f, target);
					moveType = -1;
					moving = false;
					attacking = true;
				}
				else if (moveType == 2)
				{
					ShiftCenter(16f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 1)
				{
					ShiftRight(250f,16f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					ShiftCenter(16f, target);
					moving = false;
					attacking = true;
				}
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 300 || attackTimer == 305)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 25 && attackType == 0)
                    {
                        Vector2 radius = new Vector2(120f, 0f);
                        Vector2 speed = new Vector2(0f, -16f);
                        for (int i = 0; i != 6; i++)
                        {
                            Projectile.NewProjectile(npc.position.X + npc.width / 2 + radius.X, npc.position.Y + npc.height / 2 + radius.Y, speed.X, speed.Y, mod.ProjectileType("MarisaSigilE"), i, 0f);

                            radius = radius.RotatedBy(MathHelper.ToRadians(60));
                            speed = speed.RotatedBy(MathHelper.ToRadians(60));
                        }
                        attackType = 1;
                    }
                    else if (attackTimer == 25 && attackType == 1)
                    {
                        Vector2 radius = new Vector2(120f, 0f);
                        Vector2 speed = new Vector2(0f, 16f);
                        for (int i = 0; i != 6; i++)
                        {
                            Projectile.NewProjectile(npc.position.X + npc.width / 2 + radius.X, npc.position.Y + npc.height / 2 + radius.Y, speed.X, speed.Y, mod.ProjectileType("MarisaSigilF"), i, 0f);

                            radius = radius.RotatedBy(MathHelper.ToRadians(60));
                            speed = speed.RotatedBy(MathHelper.ToRadians(60));
                        }
                        attackType = 0;
                    }
                }
				if (attackTimer == 320)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
			}
		}
		public void MasterSpark(Player target)
		{
			Move();
			if (moveComplete)
			{
				attackTimer++;
                if (Main.netMode != 1)
                {
                    if (attackTimer == 5)
                    {
                        lastPlayerPosX = target.position.X;
                        lastPlayerPosY = target.position.Y;
                    }
                    if (attackTimer >= 5 && attackTimer < 80)
                    {
                        NewLaser(npc.position.X + npc.width / 2, npc.position.Y + npc.height / 2, (float)Math.Atan2((double)npc.position.Y - lastPlayerPosY, (double)npc.position.X - lastPlayerPosX), 5, 0, 0f, 50);
                    }
                    if (attackTimer == 60)
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MASTER SPARK"), 0.75f);

                    if (attackTimer >= 60 && attackTimer < 300)
                    {
                        int numberOfProjectiles = 2;
                        Vector2 newSpeed;
                        Vector2 speed = new Vector2(-16f, 0f).RotatedBy((float)Math.Atan2((double)npc.position.Y - lastPlayerPosY, (double)npc.position.X - lastPlayerPosX));
                        for (int i = 0; i < numberOfProjectiles; i++)
                        {
                            if (attackTimer < 160)
                            {
                                numberOfProjectiles = 2;
                                newSpeed = speed.RotatedByRandom(MathHelper.ToRadians((attackTimer - 60) / 4));
                            }
                            else
                            {
                                numberOfProjectiles = 4;
                                newSpeed = speed.RotatedByRandom(MathHelper.ToRadians(25));

                            }

                            Projectile.NewProjectile(npc.position.X + npc.width / 2 + newSpeed.X * 4, npc.position.Y + npc.height / 2 + newSpeed.Y * 4, newSpeed.X, newSpeed.Y, mod.ProjectileType("MasterSparkHostile"), 42, 0f);
                        }
                    }
                    if (attackTimer == 110 || attackTimer == 170)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                        Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 6;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotYellow"), 28, 0f);
                        }
                    }
                    if (attackTimer == 120 || attackTimer == 180)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                        Vector2 firstShot = new Vector2(6f, 6f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 6;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotRed"), 28, 0f);
                        }
                    }
                    if (attackTimer == 130 || attackTimer == 190)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                        Vector2 firstShot = new Vector2(10f, 10f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 6;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotBlue"), 28, 0f);
                        }
                    }
                    if (attackTimer == 140 || attackTimer == 200)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                        Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 6;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotPurple"), 28, 0f);
                        }
                    }
                    if (attackTimer == 150 || attackTimer == 210)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                        Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 6;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotOrange"), 28, 0f);
                        }
                    }
                    if (attackTimer == 160 || attackTimer == 220)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                        Vector2 firstShot = new Vector2(7f, 7f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 6;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigStarShotGreen"), 28, 0f);
                        }
                    }
                }
				if (attackTimer == 360)
				{
					attackTimer = 0;

                }
			}
		}
		public void EarthLightRay(Player target)
		{
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 3)
				{
					ShiftLeft(100f,20f, target);
					moveType = -1;
					moving = false;
					attacking = true;
				}
				else if (moveType == 2)
				{
					ShiftCenter(20f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 1)
				{
					ShiftRight(100f,20f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					ShiftCenter(20f, target);
					moving = false;
					attacking = true;
				}
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				Vector2 radius = new Vector2(14.1421f, 14.1421f);
				Vector2 speed = new Vector2(0f, -1f);
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 200 || attackTimer == 205)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 25)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            Projectile.NewProjectile(target.Center.X - 500 + i * 125, target.Center.Y + 150, 0f, 0f, mod.ProjectileType("MarisaSigilG"), 0, 0f);
                        }
                        Projectile.NewProjectile(npc.position.X + npc.width / 2 + radius.X, npc.position.Y + npc.height / 2 + radius.Y, speed.X, speed.Y, mod.ProjectileType("MarisaSigilH"), 0, 0f);
                    }
                    if (attackTimer == 30)
                    {
                        Projectile.NewProjectile(npc.position.X + npc.width / 2 + -radius.X, npc.position.Y + npc.height / 2 + radius.Y, -speed.X, speed.Y, mod.ProjectileType("MarisaSigilI"), 0, 0f);
                    }
                }
				if (attackTimer == 220)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
			}	
		}
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 0f;
			return null;
		}
        public override void FindFrame(int frameHeight)
		{
            if (!CMS)
            {
			    if (npc.position.X > targetPosX + 16)
				    npc.frame.Y = frameHeight;
			    else if (npc.position.X < targetPosX - 16)
				    npc.frame.Y = 2 * frameHeight;
			    else if (AAA)
				    npc.frame.Y = 3 * frameHeight;
			    else if (AAB)
				    npc.frame.Y = 4 * frameHeight;
			    else
				    npc.frame.Y = 0;
            }
		}
    }
}