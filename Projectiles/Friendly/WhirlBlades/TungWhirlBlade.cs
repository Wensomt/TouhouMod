using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
 
namespace TouhouMod.Projectiles.Friendly.WhirlBlades
{
    public class TungWhirlBlade : ModProjectile
    {
        public override void SetDefaults()
        {
			//projectile.name = "Whirl Blade";
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = true;
            projectile.melee = true;
			projectile.timeLeft = 36;
		}
        public override void AI()
        {
            projectile.rotation += MathHelper.ToRadians(8);
            projectile.velocity.Y += 0.25f;
            Player owner = Main.player[projectile.owner];
            owner.velocity.X = projectile.velocity.X;
            owner.velocity.Y = projectile.velocity.Y;
            owner.position.X = projectile.position.X + projectile.width/2 - owner.width/2 + projectile.velocity.X/4*3;
            owner.position.Y = projectile.position.Y + projectile.height/2 - owner.height/2 + projectile.velocity.Y/4*3;
            owner.statDefense += 15;
            owner.AddBuff(mod.BuffType("MotionSickness"), 160, true);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
		{	
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X * 0.95f;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * 0.95f;
			}
			Main.PlaySound(SoundID.Item10, projectile.position);
            return false;
        }
    }
}