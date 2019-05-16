using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader.IO;
using System.IO;

namespace TouhouMod.NPCs.Bosses.EoSD.Rumia
{
[AutoloadBossHead]
	public class Rumia : ModNPC
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
		private bool[] startedStage = new bool[5];
		private bool AAA = false;
		private bool AAB = false;
		private int aType = 0;
       
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rumia");
			//Tooltip.SetDefault("Yokai of the Dusk, Rumia");
	    }
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 3600;
			npc.damage = 0;
			npc.defense = 10;
			npc.knockBackResist = 0f;
			npc.width = 70;
			npc.height = 98;
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
			music = MusicID.Boss2;
			if (Main.expertMode)
			{
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RumiaEx");
			}
		    else
			{
				music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Rumia");
			}
			npc.scale = 0.9f;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = 4800 + numPlayers * 300;
			npc.damage = 0;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
				Color? color = new Color(250,50,50);
				for (int i = 0 ; i < 3 ; i++)
				{
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 21, 0f, 0f, 0, color.Value);
					Main.dust[dust].velocity *= 4f;
				}
				if (npc.life <= 0)
				{
					NPC.NewNPC((int)npc.position.X + (npc.width/2), (int)npc.position.Y + npc.height, mod.NPCType("RumiaDeath"));
				}
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = mod.ItemType("LesserHarukeiBlessing");

			if (Main.expertMode)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("RumiaTreasureBag"));
			else
			{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("RumiaTrophy"));
			}
			int x = Main.rand.Next(5);
			for (int i = 0; i < 2; i++)
			{
				if (x == 0)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MoonlightBow"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 1)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MoonlightBurst"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 2)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MoonlightBlade"));
					x += Main.rand.Next(4) + 1;
				}
				else if (x == 3)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MoonlightThrower"));
					x += Main.rand.Next(4) + 1;
					if (x > 4)
						x -= 5;
				}
				else if (x == 4)
				{
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MoonOrbStaff"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
				}
			}
			x = (Main.rand.Next(6) + 4);
			for (int i = 0 ; i <= x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("MoonlightDust"));
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

                player.AddBuff(mod.BuffType("RumiaPresence"), 60, true);
                if (Distance(npc.Center, player.Center) > 960f && npc.life <= (npc.lifeMax / 1000) * 999) 
                    player.AddBuff(mod.BuffType("faithPunish"), 50, true);



                if (player.dead || Main.dayTime)
                {
                    npc.TargetClosest(false);
                    //Player player = Main.player[npc.target];
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
                    if (npc.life <= (npc.lifeMax / 6)) // 1/6 life on expert
                    {
                        Demarcation(player);
                        if (startedStage[4] == false)
                        {
                            StartStage(player);
                            startedStage[4] = true;
                        }
                    }
                    else if (npc.life <= (npc.lifeMax / 3)) // 2/6 life on expert
                    {
                        StandardAttackC(player);
                        if (startedStage[3] == false)
                        {
                            StartStage(player);
                            startedStage[3] = true;
                        }
                    }
                    else if (npc.life <= (npc.lifeMax / 2)) // 3/6 life on expert
                    {
                        NightBird(player);
                        if (startedStage[2] == false)
                        {
                            StartStage(player);
                            startedStage[2] = true;
                        }
                    }
                    else if (npc.life <= (npc.lifeMax / 3 * 2)) // 4/6 life on expert
                    {
                        StandardAttackB(player);
                        if (startedStage[1] == false)
                        {
                            StartStage(player);
                            startedStage[1] = true;
                        }
                    }
                    else if (npc.life <= (npc.lifeMax / 6 * 5)) // 5/6 life on expert
                    {
                        MoonlightRay(player);
                        if (startedStage[0] == false)
                        {
                            StartStage(player);
                            startedStage[0] = true;
                        }
                    }
                    else // over 5 / 6 life on expert
                    {
                        StandardAttackA(player);
                    }


                }
                else
                {
                    if (npc.life <= (npc.lifeMax / 5)) // 1/5 life on normal
                    {
                        Demarcation(player);
                        if (startedStage[3] == false)
                        {
                            StartStage(player);
                            startedStage[3] = true;
                        }
                    }
                    else if (npc.life <= (npc.lifeMax / 5 * 2)) // 2/5 life on normal
                    {
                        StandardAttackC(player);
                        if (startedStage[2] == false)
                        {
                            StartStage(player);
                            startedStage[2] = true;
                        }
                    }
                    else if (npc.life <= (npc.lifeMax / 5 * 3)) // 3/5 life on normal
                    {
                        NightBird(player);
                        if (startedStage[1] == false)
                        {
                            StartStage(player);
                            startedStage[1] = true;
                        }
                    }
                    else if (npc.life <= (npc.lifeMax / 5 * 4)) // 4/5 life on normal
                    {
                        StandardAttackB(player);
                        if (startedStage[0] == false)
                        {
                            StartStage(player);
                            startedStage[0] = true;
                        }
                    }
                    else // over 4/5 life normal
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
            float distance = (float)(Math.Sqrt(Math.Pow((npc.position.Y - targetPosY), 2) + Math.Pow((npc.position.X - targetPosX), 2)));

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

			int x = Main.rand.Next(2) + 3;
			if (Main.expertMode)
				x+= Main.rand.Next(3);

			for (int i = 0 ; i < x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PointItemA"));

			x = Main.rand.Next(2) + 3;
			if (Main.expertMode)
				x+= Main.rand.Next(3);

			for (int i = 0 ; i < x ; i++)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PowerItemA"));

			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePointItemA"));
			if (Main.expertMode && Main.rand.Next(50) == 1)
				Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePowerItemA"));
			
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
				if (Main.npc[k].lifeMax == 1)
					Main.npc[k].life = 0;
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
                    
                    if (attackType == 0)
                    {
                        if (attackTimer == 10)
                            AA();
                        if (attackTimer == 15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 20)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(16f, 16f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MediumShotRed"), 16, 0f);
                            }
                        }

                        if (attackTimer == 40)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(16f, 16f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MediumShotBlue"), 16, 0f);
                            }
                        }
                    }
                        if (attackTimer == 40)
                            AA();
                        if (attackTimer == 45)
                            AA();
                        if (attackTimer == 100)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType++;
                        }
                    }
                    if (attackType == 1)
                    {
                        if (attackTimer == 10 || attackTimer==15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 15)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedBy(MathHelper.ToRadians(30));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotRed"), 14, 0f);
                            }
                        }
                        if (attackTimer == 21)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedBy(MathHelper.ToRadians(38));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotBlue"), 14, 0f);
                            }
                        }
                        if (attackTimer == 27)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedBy(MathHelper.ToRadians(46));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotYellow"), 14, 0f);
                            }
                        }
                        if (attackTimer == 33)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedBy(MathHelper.ToRadians(54));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotGreen"), 14, 0f);
                            }
                        }
                        if (attackTimer == 39)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedBy(MathHelper.ToRadians(62));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotOrange"), 14, 0f);
                            }
                        }
                    }
                        if (attackTimer == 45)
                            AA();
                        if (attackTimer == 50)
                            AA();
                        if (attackTimer == 100)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType++;
                        }
                    }
                    if (attackType == 2)
                    {
                        if (attackTimer == 10)
                            AA();
                        if (attackTimer == 15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 20)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(30));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MediumShotYellow"), 16, 0f);
                            }
                        }

                        if (attackTimer == 40)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(30));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MediumShotGreen"), 16, 0f);
                            }
                        }
                    }
                        if (attackTimer == 40)
                            AA();
                        if (attackTimer == 45)
                            AA();
                        if (attackTimer == 100)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType++;
                        }
                    }
                    if (attackType == 3)
                    {
                        if (attackTimer == 10)
                            AA();
                        if (attackTimer == 15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 20)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotRed"), 14, 0f);
                            }
                        }
                        if (attackTimer == 25)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotBlue"), 15, 0f);
                            }
                        }
                        if (attackTimer == 30)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(10f, 10f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotGreen"), 15, 0f);
                            }
                        }
                        if (attackTimer == 35)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(6f, 6f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotOrange"), 14, 0f);
                            }
                        }
                        if (attackTimer == 40)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(14f, 14f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotRed"), 14, 0f);
                            }
                        }
                        if (attackTimer == 45)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotGreen"), 14, 0f);
                            }
                        }
                        if (attackTimer == 50)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotYellow"), 14, 0f);
                            }
                        }
                        if (attackTimer == 55)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            Vector2 firstShot = new Vector2(16f, 16f).RotatedByRandom(MathHelper.ToRadians(90));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 9;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i <= numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotOrange"), 14, 0f);
                            }
                        }
                    }
                        if (attackTimer == 60)
                            AA();
                        if (attackTimer == 65)
                            AA();
                        if (attackTimer == 100)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType = 0;
                        }
                    }

                }
            
			
			
		}
		public void MoonlightRay(Player target)
		{
		
			Color? color = new Color(100,100,250);
			if (moving && attackComplete)
			{
				if (moveType == 1)
				{
					ShiftLeft(240f,16f, target);
					moveType = 0;
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					ShiftRight(240f,16f, target);
					moving = false;
					attacking = true;
					moveType = 1;
				}
				
			}
			Move();
           
                if (attacking & moveComplete)
                {
                    attackTimer++;
                    if (attackTimer == 10)
                        AA();
                    if (attackTimer == 15)
                        AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer > 20)
                        {
                            if (attackTimer == 21)

                                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BEAM"), 0.2f);
                            NewLaser(npc.Center.X - 15f, npc.Center.Y, MathHelper.ToRadians(170 - (int)((attackTimer - 20) / 3)), 3, 24, 0f, 50);
                            NewLaser(npc.Center.X + 15f, npc.Center.Y, MathHelper.ToRadians(10 + (int)((attackTimer - 20) / 3)), 3, 24, 0f, 50);

                        }
                        Dust.NewDust(new Vector2(npc.position.X - 10f, npc.Center.Y), 20, 20, 20, 0f, 0f, 0, color.Value);
                        Dust.NewDust(new Vector2(npc.position.X + npc.width, npc.Center.Y), 20, 20, 20, 0f, 0f, 0, color.Value);
                    }
                    if (attackTimer == 200)
                        AA();
                    if (attackTimer == 205)
                        AA();
                    if (attackTimer == 210)
                    {
                        attackTimer = 0;
                        attacking = false;
                        moving = true;
                        attackComplete = true;
                    }

                }

                if (Main.netMode != 1)
                {
                    attackTimerB++;

                    if (attackTimerB % 40 == 0)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                        Vector2 firstShot = new Vector2(16f, 16f).RotatedByRandom(MathHelper.ToRadians(30));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 17;
                        float rotation = MathHelper.ToRadians(180);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;    //-360+(720*1/14)

                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotBlue"), 15, 0f);

                        }
                        attackTimerB = 0;
                    }
                }
			
			
		}
		public void StandardAttackB(Player target)
		{
			
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 7)
				{
					ShiftLeft(60f , 32f, target);
					moveType = 0;
					moving = false;
					attacking = true;
				}
				else if (moveType == 6)
				{
					ShiftLeft(120f , 32f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 5)
				{
					ShiftLeft(60f , 32f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 4)
				{
					ShiftCenter(32f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 3)
				{
					ShiftRight(60f , 32f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 2)
				{
					ShiftRight(120f , 32f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 1)
				{
					ShiftRight(60f , 32f, target);
					moving = false;
					attacking = true;
				}
				else if (moveType == 0)
				{
					ShiftCenter(32f, target);
					moving = false;
					attacking = true;
				}
			}
			Move();
            
                if (attacking && moveComplete)
                {
                    attackTimer++;
                    if (attackType == 0)
                    {
                        if (attackTimer == 10)
                            AA();
                        if (attackTimer == 15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer % 5 == 0 && attackTimer < 56 && attackTimer > 20)
                        {
                            float speedY;
                            float speedX = (float)Math.Sin((npc.position.X - target.position.X) / (npc.position.Y - target.position.Y)) * 6f;
                            if (npc.position.Y < target.position.Y)
                                speedY = (float)Math.Cos((npc.position.X - target.position.X) / (npc.position.Y - target.position.Y)) * 6f;
                            else
                                speedY = (float)Math.Cos((npc.position.X - target.position.X) / (npc.position.Y - target.position.Y)) * -6f;
                            if (attackTimer < 40)
                                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                            if (attackTimer % 2 == 0)
                            {
                                for (float i = 0f; i < 6f; i++)
                                {
                                    Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX / (float)Math.Sqrt(Math.Sqrt(i)), speedY / (float)Math.Sqrt(Math.Sqrt(i)), mod.ProjectileType("LargeShotRed"), 18, 0f);
                                }
                            }
                            else
                            {
                                for (float i = 0f; i < 6f; i++)
                                {
                                    Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX / (float)Math.Sqrt(Math.Sqrt(i)), speedY / (float)Math.Sqrt(Math.Sqrt(i)), mod.ProjectileType("LargeShotCrimson"), 18, 0f);
                                }
                            }
                        }
                    }
                        if (attackTimer == 65)
                            AA();
                        if (attackTimer == 70)
                            AA();
                        else if (attackTimer == 100)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType++;
                        }
                    }
                    else if (attackType == 1)
                    {
                        if (attackTimer == 10)
                            AA();
                        if (attackTimer == 15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer < 52 && attackTimer % 2 == 0 && attackTimer > 20)
                        {
                            if (attackTimer == 22)
                                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
                            Vector2 firstShot = new Vector2(-3f, 1.5f).RotatedBy(MathHelper.ToRadians(-((attackTimer - 20) % 36) * 6) - 12);
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX, speedY, mod.ProjectileType("MediumShotGreen"), 16, 0f);
                        }
                    }
                        if (attackTimer == 60 || attackTimer == 65)
                            AA();
                        else if (attackTimer == 100)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType += Main.rand.Next(2) + 1;
                        }
                    }
                    else if (attackType == 2)
                    {
                        if (attackTimer == 10 || attackTimer == 15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 20)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(16f, 16f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MediumShotBlue"), 16, 0f);
                            }
                        }
                        if (attackTimer == 25)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotBlue"), 14, 0f);
                            }
                        }
                        if (attackTimer == 40)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(16f, 16f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MediumShotBlue"), 16, 0f);
                            }
                        }
                        if (attackTimer == 45)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotBlue"), 15, 0f);
                            }
                        }
                    }
                        if (attackTimer == 50 || attackTimer == 55)
                            AA();
                        if (attackTimer == 120)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType = 0;
                        }
                    }
                    else if (attackType == 3)
                    {
                        if (attackTimer == 10 || attackTimer == 15)
                            AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 20)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(16f, 16f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 17;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MediumShotBlue"), 16, 0f);
                            }


                        }
                        if (attackTimer % 5 == 0 && attackTimer < 41)
                        {
                            if (lastPlayerPosX == 0)
                                lastPlayerPosX = target.position.X;
                            if (lastPlayerPosY == 0)
                                lastPlayerPosY = target.position.Y;

                            float speedY;
                            float speedX = (float)Math.Sin((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 24f;
                            if (npc.position.Y < target.position.Y)
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 24f;
                            else
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * -24f;
                            float numberProjectiles = 3;
                            float rotation = MathHelper.ToRadians(5);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotRed"), 15, 0f);
                            }
                        }
                    }
                        if (attackTimer == 45 || attackTimer == 50)
                            AA();
                        if (attackTimer == 120)
                        {
                            attackTimer = 0;
                            attacking = false;
                            moving = true;
                            attackComplete = true;
                            attackType = 0;
                            lastPlayerPosY = 0;
                            lastPlayerPosX = 0;
                        }

                    }


                }
            
		}
		public void NightBird(Player target)
		{
			
			if (moving && attackComplete)
			{
				moveType++;
				if (moveType == 3)
				{
					ShiftLeft(120f,16f, target);
					moveType = 0;
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
					ShiftRight(120f,16f, target);
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
				if (attackTimer == 5 || attackTimer == 10)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer % 36 < 24 && attackTimer % 2 == 0 && attackTimer < 218)
                    {
                        Vector2 firstShot = new Vector2(-2f, 1f).RotatedBy(MathHelper.ToRadians(-((attackTimer) % 36) * 6) - 12);
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX, speedY, mod.ProjectileType("MediumShotBlue"), 12, 0f);

                    }
                    if (attackTimer % 36 > 12 && attackTimer % 2 == 0 && attackTimer < 218)
                    {
                        Vector2 firstShot = new Vector2(2f, 1f).RotatedBy(MathHelper.ToRadians(((attackTimer - 12) % 36) * 6) + 12);
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX, speedY, mod.ProjectileType("MediumShotCyan"), 12, 0f);
                    }
                }
				if (attackTimer == 225 || attackTimer == 230)
					AA();
				if (attackTimer == 280)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
				}
                if (Main.netMode != 1)
                {
                    if (attackTimer % 4 == 0 && attackTimer % 40 != 0 && attackTimer < 218)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.1f);
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
					ShiftLeft(100f,16f, target);
					moveType = 0;
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
	            
				if (attackType == 0)
				{
					if (attackTimer == 10 || attackTimer == 15)
						AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer % 10 == 0 && attackTimer < 81 && attackTimer > 20)
                        {
                            int beam = NPC.NewNPC((int)npc.position.X - 128 + Main.rand.Next(256 + npc.width), (int)npc.position.Y - 128 + Main.rand.Next(258 + npc.height), mod.NPCType("BeamOrb"));
                        }
                    }
					if (attackTimer == 90 || attackTimer == 95)
						AA();
					if (attackTimer == 200)
					{
						attackTimer = 0;
						attacking = false;
						moving = true;
						attackComplete = true;
						attackType = 1;
					}	
					
				}
				if (attackType == 1)
				{
					if (attackTimer == 10 || attackTimer == 15)
						AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer < 52 && attackTimer % 2 == 0 && attackTimer > 20)
                        {
                            if (attackTimer == 22)
                                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(-3f, 1.5f).RotatedBy(MathHelper.ToRadians(-((attackTimer - 20) % 36) * 6) - 12);
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX, speedY, mod.ProjectileType("MediumShotGreen"), 16, 0f);
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2) - speedX * 5f, npc.position.Y + (npc.height / 2) - speedY * 5f, speedX, speedY, mod.ProjectileType("MediumShotGreen"), 16, 0f);
                        }
                    }
					if (attackTimer == 60 || attackTimer == 65)
						AA();
					if (attackTimer == 120)
					{
						attackTimer = 0;
						attacking = false;
						moving = true;
						attackComplete = true;
						attackType = 2;
					}
					
				}
				if (attackType == 2)
				{
					if (attackTimer == 10 || attackTimer == 15)
						AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 20)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 25;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotGreen"), 15, 0f);
                            }
                        }
                        if (attackTimer == 25)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 25;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallShotGreen"), 14, 0f);
                            }
                        }
                        if (attackTimer == 40)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                            Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(15));
                            float speedX = firstShot.X;
                            float speedY = firstShot.Y;
                            float numberProjectiles = 25;
                            float rotation = MathHelper.ToRadians(180);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotGreen"), 15, 0f);
                            }
                        }
                    }
					if (attackTimer == 50 || attackTimer == 55)
						AA();
					if (attackTimer == 120)
					{
						attackTimer = 0;
						attacking = false;
						moving = true;
						attackComplete = true;
						attackType = 3;
					}
					
					
				}
				if (attackType == 3)
				{
					if (attackTimer == 10 || attackTimer == 15)
						AA();
                    if (Main.netMode != 1)
                    {
                        if (attackTimer == 20)
                        {
                            if (lastPlayerPosX == 0)
                                lastPlayerPosX = target.position.X;
                            if (lastPlayerPosY == 0)
                                lastPlayerPosY = target.position.Y;

                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);

                            float speedY;
                            float speedX = (float)Math.Sin((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 12f;
                            if (npc.position.Y < target.position.Y)
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 12f;
                            else
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * -12f;
                            float numberProjectiles = 6;
                            float rotation = MathHelper.ToRadians(50);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotYellow"), 15, 0f);
                            }
                        }
                        if (attackTimer == 60)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);

                            float speedY;
                            float speedX = (float)Math.Sin((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 12f;
                            if (npc.position.Y < target.position.Y)
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 12f;
                            else
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * -12f;
                            float numberProjectiles = 6;
                            float rotation = MathHelper.ToRadians(50);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotYellow"), 15, 0f);
                            }
                        }
                        if (attackTimer == 40 || attackTimer == 80)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);

                            float speedY;
                            float speedX = (float)Math.Sin((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 12f;
                            if (npc.position.Y < target.position.Y)
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * 12f;
                            else
                                speedY = (float)Math.Cos((npc.position.X - lastPlayerPosX) / (npc.position.Y - lastPlayerPosY)) * -12f;
                            float numberProjectiles = 5;
                            float rotation = MathHelper.ToRadians(40);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                                Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RiceShotYellow"), 15, 0f);
                            }
                            if (attackTimer == 50)
                            {
                                lastPlayerPosX = 0;
                                lastPlayerPosY = 0;
                            }
                        }
                    }
					if (attackTimer == 60 || attackTimer == 65)
						AA();
					if (attackTimer == 120)
					{
						attackTimer = 0;
						attacking = false;
						moving = true;
						attackComplete = true;
						attackType = 0;
					}
					
					
				}
				
				
			}
			
		}
		public void Demarcation(Player target)
		{
			
			if (moving && attackComplete)
			{
				ShiftCenter(32f , target);
				moving = false;
				attacking = true;
			}
			Move();
			if (attacking && moveComplete)
			{
				attackTimer++;
                if (Main.netMode != 1)
                {
                    if (attackTimer == 20)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 firstShot = new Vector2(12f, 12f).RotatedByRandom(MathHelper.ToRadians(15));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 17;
                        float rotation = MathHelper.ToRadians(180);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Vector2 perturbedSpeedB = perturbedSpeed.RotatedBy(MathHelper.ToRadians(5));
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RotatingRiceBlueL"), 15, 0f);
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RotatingRiceBlueR"), 15, 0f);
                        }
                    }
                    if (attackTimer == 80)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 firstShot = new Vector2(10f, 10f).RotatedByRandom(MathHelper.ToRadians(15));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 17;
                        float rotation = MathHelper.ToRadians(180);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Vector2 perturbedSpeedB = perturbedSpeed.RotatedBy(MathHelper.ToRadians(5));
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RotatingRiceGreenL"), 15, 0f);
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RotatingRiceGreenR"), 15, 0f);
                        }
                    }
                    if (attackTimer == 140)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.1f);
                        Vector2 firstShot = new Vector2(8f, 8f).RotatedByRandom(MathHelper.ToRadians(15));
                        float speedX = firstShot.X;
                        float speedY = firstShot.Y;
                        float numberProjectiles = 17;
                        float rotation = MathHelper.ToRadians(180);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                            Vector2 perturbedSpeedB = perturbedSpeed.RotatedBy(MathHelper.ToRadians(5));
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RotatingRiceRedL"), 15, 0f);
                            Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RotatingRiceRedR"), 15, 0f);
                        }
                    }
                }
				if (attackTimer == 150 || attackTimer == 155)
					AA();
                if (Main.netMode != 1)
                {
                    if (attackTimer >= 160 && attackTimer <= 240 && attackTimer % 20 == 0)
                    {
                        int beam = NPC.NewNPC((int)npc.position.X - 128 + Main.rand.Next(256 + npc.width), (int)npc.position.Y - 128 + Main.rand.Next(258 + npc.height), mod.NPCType("BeamOrb"));
                    }
                }
				if (attackTimer == 250 || attackTimer == 255)
					AA();
				if (attackTimer == 320)
				{
					attackTimer = 0;
					attacking = false;
					moving = true;
					attackComplete = true;
					lastPlayerPosX = 0;
					lastPlayerPosY = 0;
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