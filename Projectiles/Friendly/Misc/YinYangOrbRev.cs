using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.Misc
{
	public class YinYangOrbRev : ModProjectile
	{
        private int timer = 0;
        float intPositionX;
        float intPositionY;
		public override void SetDefaults(){
			//projectile.name = "Yin Yang Orb";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;
			projectile.timeLeft = 5;
            projectile.alpha = 80;
		}
        public override void AI(){
            timer++;
            if (timer == 1)
            {
                intPositionX = projectile.velocity.X;
                intPositionY = projectile.velocity.Y;
                projectile.velocity.X = 0f;
                projectile.velocity.Y = 0f;
            }
            Player owner = Main.player[projectile.owner];
            Vector2 placement = new Vector2(intPositionX , intPositionY).RotatedBy(MathHelper.ToRadians(-((2 * timer) % 360)));
            projectile.position.X = owner.position.X + owner.width + placement.X - projectile.width;
            projectile.position.Y = owner.position.Y + owner.height - owner.height / 4 + placement.Y - projectile.height;
            for (int i = 0; i < owner.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs.Length ; i++)
                if(owner.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[i])
                    projectile.timeLeft = 5;
            if(Main.rand.Next(100 - owner.GetModPlayer<TouhouPlayer>(mod).yinYangOrbProjectileChance) == 1)
            {
                if (ClosestEnemy(800f) != -1)
                {
                    int target = ClosestEnemy(800f);
                    Vector2 perturbedSpeed = new Vector2(-2f,0f).RotatedBy((float)Math.Atan2((double)projectile.Center.Y - Main.npc[target].Center.Y, (double)projectile.Center.X - Main.npc[target].Center.X) );
                    Projectile.NewProjectile(projectile.Center, perturbedSpeed, mod.ProjectileType("HomingAmulet"), projectile.damage, 2, owner.whoAmI);
                }
            }
            projectile.rotation = MathHelper.ToRadians(timer * 6 % 360);
        }
        public int ClosestEnemy(float range)
        {
            int closest = -1;
            float minDistance = range;
            for (int i = 0 ; i < Main.npc.Length ; i++)
            {
                if (!Main.npc[i].friendly && Distance(Main.npc[i].Center, projectile.Center) < range && Distance(Main.npc[i].Center, projectile.Center) < minDistance)
                {
                    minDistance = Distance(Main.npc[i].Center, projectile.Center);
                    closest = i;
                }
            }
            return closest;
        }
        private float Distance(Vector2 v1, Vector2 v2)
        {
            float dx = v1.X - v2.X;
            float dy = v1.Y - v2.Y;
            dx = (float)Math.Pow(dx,2);
            dy = (float)Math.Pow(dy,2);
            return (float)(Math.Sqrt(dx + dy));
        }
    }
}