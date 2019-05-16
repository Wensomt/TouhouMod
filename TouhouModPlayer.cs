using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace TouhouMod
{
    public class TouhouPlayer : ModPlayer
    {
       
		//Yin Yang Charms
        public int yinYangOrbProjectileChance = 0;
        public bool[] hasYinYangOrbs = new bool[5];

        //Envy Buckler
        public bool envyShield = false;
        public int envyShieldBonus = 0;

        //Cosmic Defender
        public bool cosmicDefender = false;
        public int cosmicDefenderBonus = 0;

        //Armor Sets
        public int warSet = 0;
        public int starrySet = 0;


        //Extra Varibles
        public float critBonus = 1f;
        public bool dollMinion = false;
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            //Envy Buckler
            if (envyShield)
            {
                envyShieldBonus -= 2;
                if (envyShieldBonus < 0)
                    envyShieldBonus = 0;
            }
            
        }
        
		public override void PostUpdateEquips()
		{
            NeutralizeEquips();
            //Ascessories
                //Cosmic Defender
                if (cosmicDefender && cosmicDefenderBonus > 0)
                {
                    Vector2 speed = new Vector2(0f, 16f).RotatedByRandom(MathHelper.ToRadians(30));
                    Projectile.NewProjectile(player.Center.X - 512 + Main.rand.Next(1024), player.Center.Y - 640 + Main.rand.Next(128), speed.X, speed.Y, mod.ProjectileType("MagicShot"), 56, 4f, player.whoAmI);
                    cosmicDefenderBonus--;
                }

            //Armor Sets
                // N/a

            //Buffs
                //Removes doll minions
                if (player.FindBuffIndex(mod.BuffType("DollMinion")) == -1)
                {
                    for (int i = 0 ; i < Main.projectile.Length ; i++)
                    {
                        if (Main.projectile[i].type == mod.ProjectileType("DollMinion"))
                            Main.projectile[i].Kill();
                    }
                }
            
        }
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            //Ascessories
                //Envy Buckler
                if (envyShield)
                {
                    envyShieldBonus += (int)damage;
                    if (envyShieldBonus > 100)
                        envyShieldBonus = 100;
                }
                //Cosmic Denfender
                if (cosmicDefender)
                {
                    cosmicDefenderBonus += (int)damage;
                }

            //Armor
                // N/a

        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //Crit Bonus Damage
            if (crit)
            {
                damage = (int)(critBonus * damage);
            }
            //War Armor
            if (warSet > -1 && proj.type != mod.ProjectileType("LancingDoll") && Main.rand.Next(3) == 0)
            {
                Vector2 vector = new Vector2(12f,0f).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(target.Center + vector * 12f, vector.RotatedBy(MathHelper.ToRadians(180)), mod.ProjectileType("LancingDoll"), damage/3*2, 1f, player.whoAmI);
            }
            //Starry Armor
            if (starrySet > -1)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Vector2 vector = new Vector2(12f,0f).RotatedByRandom(MathHelper.ToRadians(360)) * (float)(Main.rand.NextDouble() * 24.0 + 1.0);
                    Projectile.NewProjectile(target.Center + vector, new Vector2(0f,0f), mod.ProjectileType("HealingStar"), 0, 0f, player.whoAmI);
                }
            }
        }
        public void NeutralizeEquips()
        {
            //Ascessories
                //Yin Yang Charms
                    hasYinYangOrbs[0] = CheckArmor(mod.ItemType("YinYangCharm"));
                    hasYinYangOrbs[1] = CheckArmor(mod.ItemType("YinYangCharmB"));
                    hasYinYangOrbs[2] = CheckArmor(mod.ItemType("YinYangCharmC"));
                    hasYinYangOrbs[3] = CheckArmor(mod.ItemType("YinYangCharmD"));
                    hasYinYangOrbs[4] = CheckArmor(mod.ItemType("YinYangCharmE"));
                //Moonlight Amulet
                    // N/a
                //Envy Buckler
                    envyShield = CheckArmor(mod.ItemType("EnvyBuckler"));
                    if (!envyShield)
                        envyShieldBonus = 0;
                //War Sign
                    // N/a
                //Cosmic Defender
                    cosmicDefender = CheckArmor(mod.ItemType("CosmicDefender"));
                    if (!cosmicDefender)
                        cosmicDefenderBonus = 0;
            
            //Armor Sets
                //Moonlight Set
                    // N/a
                //Envy Set
                    // N/a
                //War Set
                    warSet = ReduceArmor(warSet);
                //Starry Set
                    starrySet = ReduceArmor(starrySet);
        }
        public bool CheckArmor(int itemType)
        {
            bool hasItem = false;
            for (int i = 3; i < 8 + player.extraAccessorySlots ; i++)
            {
                if (player.armor[i].type == itemType)
                    hasItem = true;
            }
            return hasItem;
        }
        public int ReduceArmor(int armorSet)
        {
            if (armorSet >= 0)
            {
                return armorSet - 1;
            }
            return armorSet;
        }
    }

	}