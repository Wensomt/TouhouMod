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
    public class BloodWhirlBlade : ModProjectile
    {
        public override void SetDefaults()
        {
			//projectile.name = "Whirl Blade";
            projectile.width = 72;
            projectile.height = 72;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = true;   
            projectile.melee = true;
			projectile.timeLeft = 48;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
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
            owner.AddBuff(mod.BuffType("MotionSickness"), 180, true);
            owner.statDefense += 20;
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
    }
}