using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.NPCs.Bosses.PCB.Alice
{
[AutoloadBossHead]
	public class Alice : ModNPC
	{

        private bool plus = false;
        private int moveType = 0;
		private int moveTimer = 0;
		private int attackType = 0;
		private int attackTypeB = 0;
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
		private bool AAA = false; //Attack Animation A
		private bool AAB = false; //Attack Animation B
		private int aType = 0; //Animation Frame
        private bool CMS = false; //Custom Movement Stage
		private bool trueShiftCenter = false;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Alice");
			//Tooltip.SetDefault("Rainbow Colored Puppeteer, Alice Margatroid");
	    }
		
        public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 42000;
			npc.damage = 0;
			npc.defense = 25;
            npc.knockBackResist = 0f;
			npc.width = 92;
			npc.height = 126;
			Main.npcFrameCount[npc.type] = 5;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 30f;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            npc.netUpdate = true;
            npc.scale = 0.75f;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Alice");
			
			//music = MusicID.Boss2;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = 42000 + numPlayers * 4000;
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
					NPC.NewNPC((int)npc.position.X + (npc.width/2), (int)npc.position.Y + npc.height, mod.NPCType("AliceDeath"));
				}
		}
        public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = mod.ItemType("GreaterHarukeiBlessing");

			if (Main.expertMode)
			{	
			   Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("AliceTreasureBag"));
			}
			else
			{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("AliceTrophy"));
			}
			int x = Main.rand.Next(5);
			for (int i = 0; i < 2; i++)
			{
				if (x == 0)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("HouraiDoll"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 1)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("ElegantBow"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 2)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("ShanghaiDoll"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 3)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("GrimoireofAlice"));
					x += Main.rand.Next(4) + 1;
					if (x > 4)
						x -= 5;
				}
				else if (x == 4)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("ExplosiveDoll"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
				}
			}
			x = (Main.rand.Next(6) + 4);
			for (int i = 0 ; i <= x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("DollFragment"));
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
			
			player.AddBuff(mod.BuffType("AlicePresence"), 60, true);
			if (Distance(npc.Center , player.Center) > 960f && npc.life <= (npc.lifeMax / 10000) * 9999)
				player.AddBuff(mod.BuffType("faithPunish"), 50, true);
			
			
			if (player.dead)
			{
				npc.TargetClosest(false);
				player = Main.player[npc.target];
				if (player.dead || !player.active)
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
				if (npc.life <= (npc.lifeMax / 9 * 2)) // 1/8 life on expert
				{
					EerieShanghaiDolls(player);
					if(startedStage[6] == false)
					{
						trueShiftCenter = true;
						StartStage(player);
						startedStage[6] = true;
					}
					
				}
				else if (npc.life <= (npc.lifeMax / 3)) // 2/8 life on expert
				{
					FoggyLondonDolls(player);
					if(startedStage[5] == false)
					{
						StartStage(player);
						startedStage[5] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 4)) // 3/8 life on expert
				{
					StandardAttackD(player);
					if(startedStage[4] == false)
					{
						StartStage(player);
						startedStage[4] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 5)) // 4/8 life on expert
				{
					RedDutchDolls(player);
					if(startedStage[3] == false)
					{
						StartStage(player);
						startedStage[3] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 3 * 2)) // 5/8 life on expert
				{
					BenevolentFrenchDolls(player);
					if(startedStage[2] == false)
					{
						trueShiftCenter = true;
						StartStage(player);
						startedStage[2] = true;
					
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 7)) // 6/8 life on expert
				{
					StandardAttackC(player);
					if(startedStage[1] == false)
					{
						StartStage(player);
						startedStage[1] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 9 * 8)) // 7/8 life on expert
				{
					StandardAttackB(player);
					if(startedStage[0] == false)
					{
						StartStage(player);
						startedStage[0] = true;
					}
				}
				else // over 7 / 8 life on expert
				{
					StandardAttackA(player);
				}
				
				
			}
			else
			{
				
				if (npc.life <= (npc.lifeMax / 4)) // 1/7 life on normal
				{
					EerieShanghaiDolls(player);
					if(startedStage[5] == false)
					{
						trueShiftCenter = true;
						StartStage(player);
						startedStage[5] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 8 * 3)) // 2/7 life on normal
				{
					StandardAttackD(player);
					if(startedStage[4] == false)
					{
						StartStage(player);
						startedStage[4] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 2)) // 3/7 life on normal
				{
					RedDutchDolls(player);
					if(startedStage[3] == false)
					{
						StartStage(player);
						startedStage[3] = true;
					}
				}		
				else if (npc.life <= (npc.lifeMax / 8 * 5)) // 4/7 life on normal
				{
					BenevolentFrenchDolls(player);
					if(startedStage[2] == false)
					{
						StartStage(player);
						startedStage[2] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 4 * 3)) // 5/7 life on normal
				{
					StandardAttackC(player);
					if(startedStage[1] == false)
					{
						StartStage(player);
						startedStage[1] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 8 * 7)) // 6/7 life on normal
				{
					StandardAttackB(player);
					if(startedStage[0] == false)
					{
						StartStage(player);
						startedStage[0] = true;
					}
				}
				else // over 6/7 life normal
				{
					StandardAttackA(player);
				}
			
			}
			return;
		}
        //Movement and Attack Animation
        public void ShiftCenter(float offset, Player player)
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


            intialDistance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY), 2) + Math.Pow((npc.position.X - targetPosX), 2)));

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

            intialDistance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY), 2) + Math.Pow((npc.position.X - targetPosX), 2)));

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

            intialDistance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY), 2) + Math.Pow((npc.position.X - targetPosX), 2)));

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
		public void sideStep(float distance)
		{
			targetPosX = npc.Center.X + distance;
			if (distance > 0f)
				npc.velocity.X = 6f;
			else
				npc.velocity.X = -6f;
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
			attackTypeB = 0;
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
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PointItemC"));

			x = Main.rand.Next(2) + 3;
			if (Main.expertMode)
				x+= Main.rand.Next(3);

			for (int i = 0 ; i < x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PowerItemC"));

			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePointItemC"));
			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePowerItemC"));
			
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
		public void CompleteAttack()
		{
			attackTimer = 0;
			attacking = false;
			moving = true;
			attackComplete = true;
			attackType++;
			attackTypeB++;
		}
        //Attack Stages
        public void StandardAttackA(Player target)
        {
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 3)
				{
					ShiftLeft(200f,16f, target);
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
					ShiftRight(200f,16f, target);
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
				Vector2 position;
				Vector2 speed;
                if (Main.netMode != 1)
                {
                    int type = 0;
                    switch (attackType)
                    {
                        case 0:
                            type = mod.ProjectileType("MediumShotBlue");
                            break;
                        case 1:
                            type = mod.ProjectileType("MediumShotRed");
                            break;
                        case 2:
                            type = mod.ProjectileType("MediumShotGreen");
                            break;
                        case 3:
                            type = mod.ProjectileType("MediumShotYellow");
                            break;
                    }
                    position = new Vector2(100f, 0f);
                    speed = new Vector2(-.50f, 2f);
                    int numberofProjectiles;
                    if (attackTimer == 10 || attackTimer == 15 || attackTimer == 275 || attackTimer == 280)
                        AA();
                    switch (attackTypeB)
                    {
                        case 0:
                            if (attackTimer >= 20 && attackTimer <= 260 && attackTimer % 7 == 0)
                            {
                                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.1f);

                                numberofProjectiles = 3;
                                for (int i = 0; i < numberofProjectiles; i++)
                                {
                                    if (attackTimer % 2 == 0)
                                        Projectile.NewProjectile(position.RotatedBy(MathHelper.ToRadians(attackTimer + 120 * i)) * (1 - ((attackTimer - 20) * 0.005f)) + npc.Center,
                                                                speed.RotatedBy(MathHelper.ToRadians(attackTimer + 120 * i)).RotatedBy(MathHelper.ToRadians(160)),
                                                                type, 21, 0f);
                                    else
                                        Projectile.NewProjectile(position.RotatedBy(MathHelper.ToRadians(attackTimer + 120 * i)) * (1 - ((attackTimer - 20) * 0.005f)) + npc.Center,
                                                                speed.RotatedBy(MathHelper.ToRadians(attackTimer + 120 * i)).RotatedBy(MathHelper.ToRadians(-160)),
                                                                type, 21, 0f);
                                }
                            }
                            break;
                        case 1:
                            speed = new Vector2(.50f, -2f);
                            if (attackTimer >= 20 && attackTimer <= 260 && attackTimer % 7 == 0)
                            {
                                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.1f);
                                numberofProjectiles = 3;
                                for (int i = 0; i < numberofProjectiles; i++)
                                {
                                    if (attackTimer % 2 == 0)
                                        Projectile.NewProjectile(position.RotatedBy(MathHelper.ToRadians(attackTimer * -1 + 120 * i)) * (1 - ((attackTimer - 20) * 0.005f)) + npc.Center,
                                                                speed.RotatedBy(MathHelper.ToRadians(attackTimer * -1 + 120 * i)).RotatedBy(MathHelper.ToRadians(160)),
                                                                type, 21, 0f);
                                    else
                                        Projectile.NewProjectile(position.RotatedBy(MathHelper.ToRadians(attackTimer * -1 + 120 * i)) * (1 - ((attackTimer - 20) * 0.005f)) + npc.Center,
                                                                speed.RotatedBy(MathHelper.ToRadians(attackTimer * -1 + 120 * i)).RotatedBy(MathHelper.ToRadians(-160)),
                                                                type, 21, 0f);
                                }
                            }
                            break;
                    }
                }
				if (attackTimer == 290)
				{
					CompleteAttack();
					if (attackType == 4)
						attackType = 0;
					if (attackTypeB == 2)
						attackTypeB = 0;
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
				if (attackType == 4)
					attackType = 0;
				if (attackTypeB == 3)
					attackTypeB = 0;
			}
			Move();
			
			if (attacking && moveComplete)
			{
				int type = 0;
				Vector2 position = new Vector2(0f,0f);
				int numberofProjectiles;
				Vector2 speed;
				attackTimer++;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 95 || attackTimer == 100)
					AA();
                if (Main.netMode != 1)
                {
                    switch (attackType)
                    {
                        case 0:
                            type = mod.ProjectileType("ScaleShotBlue");
                            break;
                        case 1:
                            type = mod.ProjectileType("ScaleShotRed");
                            break;
                        case 2:
                            type = mod.ProjectileType("ScaleShotGreen");
                            break;
                        case 3:
                            type = mod.ProjectileType("ScaleShotYellow");
                            break;
                    }
                    switch (attackTypeB)
                    {
                        case 0:
                            position = new Vector2(-60f, 60f).RotatedBy(MathHelper.ToRadians(attackTimer * -1.5f));
                            break;
                        case 1:
                            position = new Vector2(-60f, -60f).RotatedBy(MathHelper.ToRadians((attackTimer - 81) * 1.5f));
                            break;
                        case 2:
                            attackTypeB = 0;
                            break;
                    }
                    speed = new Vector2(3f, 0f);
                    numberofProjectiles = 6;
                    if (attackTimer == 16)
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TWINKLE"), 0.3f);
                    if (attackTimer % 8 == 0 && attackTimer > 15 && attackTimer < 75)
                    {
                        speed = speed.RotatedBy(MathHelper.ToRadians((attackTimer - 18) * 18));
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            Projectile.NewProjectile(position + npc.Center, speed, type, 22, 0f);
                            speed = speed.RotatedBy(MathHelper.ToRadians(60));
                        }
                    }
                    if (attackTimer == 80)
                    {
                        attackType++;
                        attackTypeB++;
                    }
                    if (attackTimer == 92)
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TWINKLE"), 0.3f);
                    if (attackTimer % 8 == 0 && attackTimer > 90 && attackTimer < 150)
                    {
                        speed = speed.RotatedBy(MathHelper.ToRadians((attackTimer - 96) * 18) + 45);
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            Projectile.NewProjectile(position + npc.Center, speed, type, 22, 0f);
                            speed = speed.RotatedBy(MathHelper.ToRadians(60));
                        }
                    }
                }
				if (attackTimer == 200)
				{
					CompleteAttack();
				}
			}
        }
        public void StandardAttackC(Player target)
        {
			if (moving && attackComplete)
			{
				ShiftCenter(0f,target);
				moving = false;
				attacking = true;
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				Vector2 speedA;
				Vector2 speedB;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 110 || attackTimer == 115)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer >= 20 && attackTimer <= 60 && attackTimer % 10 == 0 && attackType == 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 speed = new Vector2(4f, 0f).RotatedBy(MathHelper.ToRadians((attackTimer - 20) / 3f));
                        int numberofProjectiles = 20;
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            speed = speed.RotatedBy(MathHelper.ToRadians(18));
                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("MediumShotRed"), 21, 0f);
                        }
                    }
                    if (attackTimer >= 20 && attackTimer <= 60 && attackTimer % 10 == 0 && attackType == 1)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 speed = new Vector2(4f, 0f).RotatedBy(MathHelper.ToRadians((attackTimer - 20) / -3f));
                        int numberofProjectiles = 20;
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            speed = speed.RotatedBy(MathHelper.ToRadians(18));
                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("MediumShotRed"), 21, 0f);
                        }
                    }
                    if (attackTimer == 70 || attackTimer == 85 || attackTimer == 100)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                        Vector2 position = new Vector2(90f, 0f);
                        speedA = new Vector2(-3f, 0f).RotatedBy(MathHelper.ToRadians(13));
                        speedB = new Vector2(-3f, 0f).RotatedBy(MathHelper.ToRadians(-13));
                        int numberofProjectiles = 14;
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            speedA = speedA.RotatedBy(MathHelper.ToRadians((360 / 14)));
                            speedB = speedB.RotatedBy(MathHelper.ToRadians((360 / 14)));
                            position = position.RotatedBy(MathHelper.ToRadians((360 / 14)));

                            Projectile.NewProjectile(position + npc.Center, speedA, mod.ProjectileType("ScaleShotRed"), 21, 0f);
                            Projectile.NewProjectile(position + npc.Center, speedB, mod.ProjectileType("ScaleShotRed"), 21, 0f);
                        }
                    }
                    if (attackTimer == 60 || attackTimer == 80 || attackTimer == 100)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(3f, 0f).RotatedByRandom(MathHelper.ToRadians(30)), mod.ProjectileType("SpinningDoll"), 21, 0f);
                        Projectile.NewProjectile(npc.Center, new Vector2(-3f, 0f).RotatedByRandom(MathHelper.ToRadians(30)), mod.ProjectileType("SpinningDoll"), 21, 0f);
                    }
                }
				if (attackTimer == 120)
				{
					if (attackTypeB == 0)
						sideStep(300f);
					else
						sideStep(-300f);
				}
				if (attackTimer == 180)
				{
					if (attackTypeB == 0)
						sideStep(-300f);
					else
						sideStep(300f);
				}
				if (attackTimer == 240)
				{
					CompleteAttack();
					if (attackTypeB == 2)
						attackTypeB = 0;
					if (attackType == 2)
						attackType = 0;
				}
			}
        }
        public void BenevolentFrenchDolls(Player target)
        {
			if (moving && attackComplete)
			{
				ShiftCenter(0f,target);
				moving = false;
				attacking = true;
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 30 || attackTimer == 35)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 50)
                    {
                        Vector2 position = new Vector2(0f, 60f);
                        Vector2 velocity = new Vector2(0f, 0f);
                        if (attackTypeB == 0)
                            velocity = new Vector2(-2.5f, 0f);
                        if (attackTypeB == 1)
                            velocity = new Vector2(2.5f, 0f);
                        int numberofProjectiles = 6;
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            position = position.RotatedBy(MathHelper.ToRadians(60));
                            velocity = velocity.RotatedBy(MathHelper.ToRadians(60));
                            Projectile.NewProjectile(position + npc.Center, velocity, mod.ProjectileType("BeneBlueShot"), 21, 0f);
                        }
                    }
                }
				if (attackTimer == 140)
				{
					if (attackType == 0)
						sideStep(300f);
					else
						sideStep(-300f);
				}
				if (attackTimer == 200)
				{
					if (attackType == 0)
						sideStep(-300f);
					else
						sideStep(300f);
				}
				if (attackTimer == 320)
				{
					CompleteAttack();
					if (attackType == 2)
						attackType = 0;
					if (attackTypeB == 2)
						attackTypeB = 0;
				}
				//Doll Placement
				Vector2 placement = new Vector2(0f, 60f);
				bool reversed = false;
				float posRotation = MathHelper.ToRadians(2 * attackTimer);
				float rotation = MathHelper.ToRadians(10 * attackTimer);
				if (attackTypeB == 0)
					reversed = true;

				//Spin out
				if (attackTimer > 20 && attackTimer < 50)
					placement = placement * (float)((attackTimer - 20) / 30.0);
				
				//Rotation Direction
				if (reversed)
					placement = placement.RotatedBy(-posRotation);
				else
					placement = placement.RotatedBy(posRotation);
				
				//Spin in
				if (attackTimer > 240 && attackTimer < 270)
					placement = placement * (float)((30.0 - (attackTimer - 240)) / 30.0);

                //Placement
                if (Main.netMode != 1)
                {
                    if (attackTimer > 20 && attackTimer < 270)
                        for (int i = 0; i < 6; i++)
                            Projectile.NewProjectile(npc.Center + placement.RotatedBy(MathHelper.ToRadians(60 * i)), new Vector2(0f, 0f), mod.ProjectileType("SpinningDoll"), 0, rotation);
                }

			}
        }
        public void RedDutchDolls(Player target)
        {
			Move();
			attackTimer++;
            if (Main.netMode != 1)
            {
                if (attackTimer % 16 == 0 && attackTimer <= 96)
                {
                    Vector2 position = new Vector2(npc.Center.X - 250f + (attackTimer / 16 * 100f), npc.Center.Y - 32f + (float)(Main.rand.Next(128)));
                    NPC.NewNPC((int)position.X, (int)position.Y, mod.NPCType("RedDutchDoll"));
                    position.X = position.X + 100f;
                    position.Y = npc.Center.Y - 32f + (float)(Main.rand.Next(128));
                }
            }
			if (attackTimer == 180)
				attackTimer = -1;
        }
        public void StandardAttackD(Player target)
        {
			if (moving && attackComplete)
			{
				ShiftCenter(0f,target);
				moving = false;
				attacking = true;
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				if (attackTimer == 10 || attackTimer == 15 || attackTimer == 670 || attackTimer == 675)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer >= 25 && attackTimer <= 225 && attackTimer % 16 == 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 speed = new Vector2(2f, 0f).RotatedBy(MathHelper.ToRadians((attackTimer - 25) / -1.2f));
                        float angle = MathHelper.ToRadians(30);
                        for (int i = 0; i < 5; i++)
                        {
                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("MediumShotBlue"), 21, 0f);
                            Projectile.NewProjectile(npc.Center, speed.RotatedBy(MathHelper.ToRadians(180)), mod.ProjectileType("MediumShotBlue"), 22, 0f);
                            speed = speed.RotatedBy(angle);
                        }
                    }
                    if (attackTimer >= 250 && attackTimer <= 450 && attackTimer % 16 == 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 speed = new Vector2(2f, 0f).RotatedBy(MathHelper.ToRadians((attackTimer - 25) / 1.2f));
                        float angle = MathHelper.ToRadians(30);
                        for (int i = 0; i < 5; i++)
                        {
                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("MediumShotRed"), 21, 0f);
                            Projectile.NewProjectile(npc.Center, speed.RotatedBy(MathHelper.ToRadians(180)), mod.ProjectileType("MediumShotRed"), 22, 0f);
                            speed = speed.RotatedBy(angle);
                        }
                    }
                    if (attackTimer >= 520 && attackTimer <= 580 && attackTimer % 10 == 0)
                    {
                        if (attackTimer == 520)
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TWINKLE"), 0.3f);
                        Vector2 speed = new Vector2(2.5f, 0f).RotatedBy(MathHelper.ToRadians((attackTimer - 220) / -3f));
                        int numberofProjectiles = 16;
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            speed = speed.RotatedBy(MathHelper.ToRadians(360f / 16f));
                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("ScaleShotRed"), 21, 0f);
                        }
                    }
                    if (attackTimer >= 600 && attackTimer <= 660 && attackTimer % 10 == 0)
                    {
                        if (attackTimer == 600)
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TWINKLE"), 0.3f);
                        Vector2 speed = new Vector2(2.5f, 0f).RotatedBy(MathHelper.ToRadians((attackTimer - 280) / 3f));
                        int numberofProjectiles = 16;
                        for (int i = 0; i < numberofProjectiles; i++)
                        {
                            speed = speed.RotatedBy(MathHelper.ToRadians(360f / 16f));
                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("ScaleShotBlue"), 21, 0f);
                        }
                    }
                }
				if (attackTimer == 700)
					CompleteAttack();

			}
        }
        public void FoggyLondonDolls(Player target)
        {
			Move();
			if (moveComplete)
			{
				attackTimer++;
				Vector2 position = new Vector2(120f,0f);
				Vector2 speed = new Vector2(0f,2f);
				int type = 0;
                if (Main.netMode != 1)
                {
                    switch (attackType)
                    {
                        case 0:
                            type = mod.ProjectileType("RiceShotYellow");
                            break;
                        case 1:
                            type = mod.ProjectileType("RiceShotGreen");
                            break;
                        case 2:
                            type = mod.ProjectileType("RiceShotBlue");
                            break;
                        case 3:
                            attackType = 0;
                            break;
                    }
                    if (attackTimer % 20 == 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                        for (int i = 0; i < 6; i++)
                        {
                            Projectile.NewProjectile(npc.Center + position.RotatedBy(MathHelper.ToRadians(i * 60 + attackTimer)), speed.RotatedBy(MathHelper.ToRadians(attackTimer)), type, 21, 0f);
                            Projectile.NewProjectile(npc.Center + position.RotatedBy(MathHelper.ToRadians(i * 60 + attackTimer)), speed.RotatedBy(MathHelper.ToRadians(attackTimer + 180)), type, 21, 0f);
                        }
                    }
                }
				switch(attackTimer % 800)
				{
					case 300:
						sideStep(300f);
						break;
					case 380:
						sideStep(-300f);
						break;
					case 700:
						sideStep(-300f);
						break;
					case 780:
						sideStep(300f);
						break;
				}
				if (attackTimer % 300 == 0)
					attackType++;

				//Doll Placement
				Vector2 placement = new Vector2(120f,0f);
				float dollRotation = MathHelper.ToRadians(10 * attackTimer);
                if (Main.netMode != 1)
                {
                    for (int i = 0; i < 6; i++)
                        Projectile.NewProjectile(npc.Center + placement.RotatedBy(MathHelper.ToRadians(i * 60 + attackTimer)), new Vector2(0f, 0f), mod.ProjectileType("SpinningDoll"), 0, dollRotation);
                }
			}
        }
        public void EerieShanghaiDolls(Player target)
        {
			Move();
			if (moveComplete)
			{
				attackTimer++;
                if (Main.netMode != 1)
                {
                    if (attackTimer % 120 == 0)
				{
					Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
					int type = mod.ProjectileType("EerieBigLightRight");
					if (attackTimer % 200 == 0)
					{
						type = mod.ProjectileType("EerieBigLightLeft");
					}
					int numberofProjectiles = 8;
					int offset = Main.rand.Next(90);
					Vector2 speed = new Vector2(3f,0f);
					for (int i = 0 ; i < numberofProjectiles ; i++)
					{
						Projectile.NewProjectile(npc.Center, speed.RotatedBy(MathHelper.ToRadians(45 * i + offset)), type, 24 , 0f);
					}
				}
				if (attackTimer % 240 == 0)
				{
					Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
					int numberofProjectiles = 18;
					Vector2 speed = new Vector2(1.5f,0f);
					for (int i = 0 ; i < numberofProjectiles ; i++)
					{
						for (int j = 0 ; j < 3 ; j++)
						{
							Projectile.NewProjectile(npc.Center, speed.RotatedBy(MathHelper.ToRadians(20 * i)) * (1f + 0.40f * j), mod.ProjectileType("ScaleShotBlue"), 21 , 0f);
						}
					}
				}
                    if (attackTimer % 200 >= 35 && attackTimer % 200 <= 80)
                    {
                        Vector2 position = new Vector2(80f, 0f);
                        Vector2 speed = new Vector2(0f, -1.5f);
                        int type = mod.ProjectileType("ScaleShotYellow");
                        switch (attackType)
                        {
                            case 0:
                                type = mod.ProjectileType("EerieScaleYellow");
                                break;
                            case 1:
                                type = mod.ProjectileType("EerieScaleOrange");
                                break;
                            case 2:
                                attackType = 0;
                                type = mod.ProjectileType("EerieScaleYellow");
                                break;
                        }
                        if (attackTimer % 9 == 0)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            for (int i = 0; i < 4; i++)
                            {
                                Projectile.NewProjectile(npc.Center + position.RotatedBy(MathHelper.ToRadians(i * -90 - (attackTimer - 35) * 2)),
                                                 speed.RotatedBy(MathHelper.ToRadians(i * -90 - (attackTimer - 35) * 2 + 12)),
                                                 type, 21, 0f);
                                Projectile.NewProjectile(npc.Center + position.RotatedBy(MathHelper.ToRadians(i * -90 - (attackTimer - 35) * 2)),
                                                 speed.RotatedBy(MathHelper.ToRadians(i * -90 - (attackTimer - 35) * 2 - 24)),
                                                 type, 21, 0f);
                            }
                        }
                    }
					if (attackTimer % 200 == 80)
						attackType++;
						

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