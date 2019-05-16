using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.MarisaWeapons
{
	public class FistoftheNorthStar : ModProjectile
	{
		public override void SetDefaults(){
			//projectile.name = "Fist of the North Star";
            projectile.width = 140;
            projectile.height = 140;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;   
            projectile.melee = true;
			projectile.light = 2f;
			projectile.timeLeft = 50;
		}
        public virtual void CreateDust()
		{
			Color? color = new Color(100,100,250);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
        public override void AI()
        {
            CreateDust();
            projectile.rotation += MathHelper.ToRadians(5);
            Player owner = Main.player[projectile.owner];
            owner.velocity.X = projectile.velocity.X;
            owner.velocity.Y = projectile.velocity.Y;
            owner.statDefense += 100;
            owner.AddBuff(mod.BuffType("MotionSickness"), 360, true);
            Vector2 starSpeed = projectile.velocity.RotatedBy(MathHelper.ToRadians(180)).RotatedByRandom(MathHelper.ToRadians(30)) * 0.5f;
            Projectile.NewProjectile(projectile.position.X + projectile.width/2 , projectile.position.Y + projectile.height/2, starSpeed.X, starSpeed.Y, mod.ProjectileType("FistoftheNorthStarTrail"), projectile.damage / 3 * 4, 6, owner.whoAmI);
        }
        //public override void OnTileCollide(Vector2 oldVelocity)
        //{
        //    projectile.timeLeft = 20;
        //}
        public override void Kill(int timeLeft)
        {
            for (int i = 0 ; i < 50 ; i++)
                CreateDust();
        }
    }
}