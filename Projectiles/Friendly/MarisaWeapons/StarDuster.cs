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
    public class StarDuster : ModProjectile
    {
        private float rotate;
        public override void SetDefaults()
        {
            //projectile.name = "Stardust";
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 3;  
            projectile.tileCollide = true;   
            projectile.ranged = true;
			projectile.light = 0.25f;
			projectile.extraUpdates = 1;
            projectile.timeLeft = 100;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			
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
        public override void AI()
        {
            if (rotate == 0)
                rotate = (float)Main.rand.NextDouble();
                
            projectile.rotation += rotate;

        }
		
	}
}