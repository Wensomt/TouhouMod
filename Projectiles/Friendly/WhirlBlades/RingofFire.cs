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
    public class RingofFire : ModProjectile
    {
        public override void SetDefaults()
        {
			//projectile.name = "Ring of Fire";
            projectile.width = 80;
            projectile.height = 80;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = true;   
            projectile.melee = true;
			projectile.timeLeft = 60;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
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
            owner.statDefense += 24;
            owner.AddBuff(mod.BuffType("MotionSickness"), 200, true);
            CreateDust();
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
        public virtual void CreateDust()
		{
			Color? color = new Color(250,50,50);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
    }
}