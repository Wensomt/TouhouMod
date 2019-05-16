using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
 
namespace TouhouMod.Projectiles.Danmaku.Special.Rumia
{
    public class MoonlightRayB : ModProjectile
    {
		
		private float angle;
        public override void SetDefaults()
        {
           // projectile.name = "Moonlight Ray";
            projectile.width = 20;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;   
			projectile.light = 1f;
			projectile.extraUpdates = 1;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			projectile.timeLeft = 60;
			
		}
		public override void AI(){
			
			angle = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.rotation = angle;
			
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
	}
	
}