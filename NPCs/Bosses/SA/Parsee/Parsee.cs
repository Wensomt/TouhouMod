using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.NPCs.Bosses.SA.Parsee
{
[AutoloadBossHead]
	public class Parsee : ModNPC
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
		private bool[] startedStage = new bool[6];
		private bool AAA = false; //Attack Animation A
		private bool AAB = false; //Attack Animation B
		private int aType = 0; //Animation Type
		
		private bool GEB = false; //true -> Custom Attack Movement
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Parsee");
			//Tooltip.SetDefault("The Jealousy Beneath the Earth's Crust, Parsee");
	    }	
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 9720;
			npc.damage = 0;
			npc.defense = 15;
			npc.knockBackResist = 0f;
			npc.width = 82;
			npc.height = 104;
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
			npc.scale = 0.95f;
			if (Main.expertMode)
					{
                    music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ParseeEx");
					}
					else
					{
					music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Parsee");
					}
			//music = MusicID.Boss2;

		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = 16800 + numPlayers * 700;
			npc.damage = (int)(npc.damage * 0.5f);
		}
		public override void HitEffect(int hitDirection, double damage)
		{
				Color? color = new Color(50,250,50);
				for (int i = 0 ; i < 3 ; i++)
				{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 21, 0f, 0f, 0, color.Value);
					Main.dust[dust].velocity *= 4f;
				}
				if (npc.life <= 0)
				{
					NPC.NewNPC((int)npc.position.X + (npc.width/2), (int)npc.position.Y + npc.height, mod.NPCType("ParseeDeath"));
				}
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = mod.ItemType("HarukeiBlessing");
			
			if (Main.expertMode)
			{
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("ParseeTreasureBag"));
			}
			else
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("ParseeTrophy"));
				}
				int x = Main.rand.Next(5);
				for (int i = 0; i < 2; i++)
				{
					if (x == 0)
					{
						Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("EnvyThrow"));
						x += Main.rand.Next(4) + 1;
					}
					else if (x == 1)
					{
						Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("GreenEyedBeast"));
						x += Main.rand.Next(4) + 1;
					}
					else if (x == 2)
					{
						Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("BloomingEnvyStaff"));
						x += Main.rand.Next(4) + 1;
					}
					else if (x == 3)
					{
						Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("BulbStaff"));
						x += Main.rand.Next(4) + 1;
						if (x > 4)
							x -= 5;
					}
					else if (x == 4)
					{
						Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("GreenEyeDagger"));
						x += Main.rand.Next(2) + 1;
						if (x > 4)
							x -= 5;
					}
				}
				x = (Main.rand.Next(6) + 4);
				for (int i = 0 ; i <= x ; i++)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("EnvyCrystal"));
				}
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
			
			player.AddBuff(mod.BuffType("ParseePresence"), 60, true);
			if (Distance(npc.Center , player.Center) > 960f && npc.life <= (npc.lifeMax / 1000) * 999)
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
				if (npc.life <= (npc.lifeMax / 7)) // 1/7 life on expert
				{
					ShrineVisitInTheDeadOfTheNight(player);
					if(startedStage[5] == false)
					{
						StartStage(player);
						startedStage[5] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 7 * 2)) // 2/7 life on expert
				{
					HateForTheHumbleAndRich(player);
					if(startedStage[4] == false)
					{
						StartStage(player);
						startedStage[4] = true;
					}
					npc.defense = 16;
				}
				else if (npc.life <= (npc.lifeMax / 7 * 3)) // 3/7 life on expert
				{
					StandardAttackC(player);
					if(startedStage[3] == false)
					{
						StartStage(player);
						startedStage[3] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 7 * 4)) // 4/7 life on expert
				{
					JealousyOfTheKindAndLovely(player);
					if(startedStage[2] == false)
					{
						StartStage(player);
						startedStage[2] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 7 * 5)) // 5/7 life on expert
				{
					StandardAttackB(player);
					if(startedStage[1] == false)
					{
						StartStage(player);
						startedStage[1] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 7 * 6)) // 6/7 life on expert
				{
					GreenEyedMonster(player);
					if(startedStage[0] == false)
					{
						StartStage(player);
						startedStage[0] = true;
					}
				}
				else // over 6 / 7 life on expert
				{
					StandardAttackA(player);
				}
			}
			else
			{
				if (npc.life <= (npc.lifeMax / 6)) // 1/6 life on normal
				{
					ShrineVisitInTheDeadOfTheNight(player);
					if(startedStage[3] == false)
					{
						StartStage(player);
						startedStage[3] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 6 * 2)) // 2/6 life on normal
				{
					HateForTheHumbleAndRich(player);
					if(startedStage[2] == false)
					{
						StartStage(player);
						startedStage[2] = true;
					}
					npc.defense = 16;
				}				
				else if (npc.life <= (npc.lifeMax / 6 * 3)) // 3/6 life on normal
				{
					StandardAttackC(player);
					if(startedStage[1] == false)
					{
						StartStage(player);
						startedStage[1] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 6 * 4)) // 4/6 life on normal
				{
					JealousyOfTheKindAndLovely(player);
					if(startedStage[0] == false)
					{
						StartStage(player);
						startedStage[0] = true;
					}
				}
				else if (npc.life <= (npc.lifeMax / 6 * 5)) // 5/6 life on normal
				{
					StandardAttackB(player);
					if(startedStage[0] == false)
					{
						StartStage(player);
						startedStage[0] = true;
					}
				}
				else // over 5/6 life on normal
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
			aType = 0;
			lastPlayerPosX = 0;
			lastPlayerPosY = 0;
			GEB = false;
			
			int x = Main.rand.Next(2) + 3;
			if (Main.expertMode)
				x+= Main.rand.Next(3);

			for (int i = 0 ; i < x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PointItemB"));

			x = Main.rand.Next(2) + 3;
			if (Main.expertMode)
				x+= Main.rand.Next(3);

			for (int i = 0 ; i < x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PowerItemB"));

			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePointItemB"));
			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePowerItemB"));

			Color? color = new Color(250,50,50);
			for (int i = 0 ; i < 8 ; i++)
			{
				int dust = Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value , 4f);
				Main.dust[dust].velocity *= 12f;
			}
			
			ShiftCenter(0f , player);
			for (int k = 0 ; k < Main.projectile.Length ; k++)
			{
				if (Main.projectile[k].hostile && Main.projectile[k].damage > 1 )
				{
					Main.projectile[k].Kill();
				}
			}
			for (int k = 0 ; k < Main.npc.Length ; k++)
			{
				if (Main.npc[k].lifeMax == 4000 || Main.npc[k].lifeMax==10000)
                {
                    Main.npc[k].life = 0;
                }
            }
		}
		//Attack Stages
		public void StandardAttackA(Player target)
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
				if (attackTimer == 5 || attackTimer == 10 || attackTimer == 45 || attackTimer == 50)
					AA();
                if (Main.netMode != 1)
                {
                    Vector2 perturbedSpeed = new Vector2(4f, 0f).RotatedBy((attackTimer - 12) * 0.19635 * 2);
                    if (attackTimer > 12 && attackTimer < 28)
                    {
                        Projectile.NewProjectile(npc.position.X + (npc.width / 2) + (int)perturbedSpeed.X * 32, npc.position.Y + (npc.height / 2) + (int)perturbedSpeed.Y * 32, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ScaleShotRandParsee"), 15, 0f);
                        if (attackTimer % 4 == 0)
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                    }
                }
				if (attackTimer == 50)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
				
			}
			
			
		}
		public void GreenEyedMonster(Player target)
		{
			Color? color = new Color(50,250,50);
			GEB = true;
			
			if (moving && attackComplete && moveComplete)
			{
				if (moveType == 1)
				{
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					npc.position.X = target.position.X + 240f;
					npc.position.Y = target.position.Y - 250f;
					npc.velocity = new Vector2(0f,0f);
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					moveType = 0;
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					npc.position.X = target.position.X - 240f;
					npc.position.Y = target.position.Y - 250f;
					npc.velocity = new Vector2(0f,0f); 
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					moving = false;
					attacking = true;
					moveType = 1;
				}
				
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
				
				if (attackTimer == 5 || attackTimer == 10 || attackTimer == 620 || attackTimer == 625)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 15)
                    {
                        NPC.NewNPC((int)npc.position.X - 128 + Main.rand.Next(256 + npc.width), (int)npc.position.Y - 128 + Main.rand.Next(258 + npc.height), mod.NPCType("GreenEyedMonster"));
                    }
                }
				if (attackTimer == 640)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
			
			}
			
		}
		public void StandardAttackB(Player target)
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
				if (attackTimer == 5 || attackTimer == 10 || attackTimer == 85 || attackTimer == 90)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 15 || attackTimer == 35 || attackTimer == 55)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 firstShot = new Vector2(10f, 10f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 24;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotYellow"), 14, 0f);
                        }
                    }
                }
				if (attackTimer == 100)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
				
			}
			
		}
		public void JealousyOfTheKindAndLovely(Player target)
		{
			moveTimer++;
			GEB = true;
			if (moveTimer <= 120 || moveTimer > 360)
			{
				npc.position.X += 2f;
				npc.velocity = new Vector2(0f,0f);
				aType = 9;
				if (moveTimer > 480)
					moveTimer = 0;
			}
			if (moveTimer > 120 && moveTimer <= 360)
			{
				npc.velocity = new Vector2(0f,0f);
				npc.position.X -= 2f;
				aType = 8;
			}
			
			attackTimer++;
            if (Main.netMode != 1)
            {
                if (attackTimer % 120 == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                    Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), new Vector2(-6f, 0f).RotatedBy((float)Math.Atan2((double)npc.position.Y - target.position.Y, (double)npc.position.X - target.position.X)).X, new Vector2(-6f, 0f).RotatedBy((float)Math.Atan2((double)npc.position.Y - target.position.Y, (double)npc.position.X - target.position.X)).Y, mod.ProjectileType("FloweringShotParsee"), 16, 0f);
                }
                if (attackTimer % 140 == 0)
                {
                    Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(180));
                    float speedX = firstShot.X;
                    float speedY = firstShot.Y;
                    float numberProjectiles = 8;
                    float rotation = MathHelper.ToRadians(360);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                        Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("LargeShotGreen"), 17, 0f);
                    }
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
				if (attackTimer == 5 || attackTimer == 10 || attackTimer == 85 || attackTimer == 90)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 15 || attackTimer == 35 || attackTimer == 55)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 firstShot = new Vector2(10f, 10f).RotatedByRandom(MathHelper.ToRadians(180));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 16;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotYellow"), 14, 0f);
                        }
                    }
                    if (attackTimer == 25 || attackTimer == 45 || attackTimer == 65)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 firstShot = new Vector2(10f, 10f).RotatedByRandom(MathHelper.ToRadians(180));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 16;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotBlue"), 14, 0f);
                        }
                    }
                }
				if (attackTimer == 100)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
				
			}

		}
		public void HateForTheHumbleAndRich(Player target)
		{
			Color? color = new Color(50,250,50);
			GEB = true;
			
			if (moving && attackComplete && moveComplete)
			{
				if (moveType == 1)
				{
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					npc.position.X = target.position.X + 240f;
					npc.position.Y = target.position.Y - 360f;
					NPC.NewNPC((int)(target.position.X - 240f) , (int)(target.position.Y - 360f + npc.height), mod.NPCType("ParseeClone"));
					npc.velocity = new Vector2(0f,0f);
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					moveType = 0;
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					npc.position.X = target.position.X - 240f;
					npc.position.Y = target.position.Y - 360f;
					NPC.NewNPC((int)(target.position.X + 240f) , (int)(target.position.Y - 360f + npc.height), mod.NPCType("ParseeClone"));
					npc.velocity = new Vector2(0f,0f); 
					for (int i = 0 ; i < 20 ; i++)
					{
						Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value);
					}
					moving = false;
					attacking = true;
					moveType = 1;
				}
				
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;

                if (attackType == 0)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
					attackType++;
				}
				
				if (attackTimer == 5 || attackTimer == 10 || attackTimer == 400 || attackTimer == 405)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer < 400 && attackTimer % 50 == 0 && attackType != 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.15f);
                        Vector2 firstShot = new Vector2(10f, 10f).RotatedByRandom(MathHelper.ToRadians(90));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 24;
                        float rotation = MathHelper.ToRadians(360);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotGreen"), 13, 0f);
                        }
                    }
                }
				if (attackType == 0)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
					attackType++;
				}
				if (attackTimer == 420)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
			
			}
			
		}
		public void ShrineVisitInTheDeadOfTheNight(Player target)
		{
			
			if (moving && attackComplete)
			{
				moving = false;
				attacking = true;
			}
			Move();
			if (attacking && moveComplete)
			{
                if (Main.netMode != 1)
                {
                    if (attackTimer == 0)
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/POWER UP"), 0.3f);
                }
				attackTimer++;
				if (attackTimer == 5 || attackTimer == 10 || attackTimer == 115 || attackTimer == 120)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer == 10)
                    {
                        lastPlayerPosX = target.position.X;
                        lastPlayerPosY = target.position.Y;
                    }
                    if (attackTimer % 4 == 0 && attackTimer > 48 && attackTimer < 132)
                    {
                        Projectile.NewProjectile(npc.position.X + (npc.width / 2) - 16 + Main.rand.Next(32), npc.position.Y + (npc.height / 2) - 16 + Main.rand.Next(32), new Vector2(-6f, 0f).RotatedBy((float)Math.Atan2((double)npc.position.Y - lastPlayerPosY, (double)npc.position.X - lastPlayerPosX)).X, new Vector2(-6f, 0f).RotatedBy((float)Math.Atan2((double)npc.position.Y - lastPlayerPosY, (double)npc.position.X - lastPlayerPosX)).Y, mod.ProjectileType("ShrineDiamondParsee"), 15, 0f);
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TWINKLE"), 0.3f);
                    }
                }
				if (attackTimer == 160)
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
			if (npc.position.X > targetPosX + 16 && !GEB || aType == 8)
				npc.frame.Y = frameHeight;
			else if (npc.position.X < targetPosX - 16 && !GEB || aType == 9)
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