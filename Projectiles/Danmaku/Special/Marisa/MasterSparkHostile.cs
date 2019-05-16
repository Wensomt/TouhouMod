using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
 
namespace TouhouMod.Projectiles.Danmaku.Special.Marisa
{
    public class MasterSparkHostile : ModProjectile
    {
        public override void SetDefaults()
        {
            //projectile.name = "Master Spark";
            projectile.width = 40;
            projectile.height = 40;
            projectile.hostile = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;   
			projectile.light = 3f;
			projectile.extraUpdates = 1;
            projectile.timeLeft = 300;
            projectile.alpha = 200;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 30;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor);// * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
        public virtual void CreateDust()
		{
			Color? color = new Color(Main.rand.Next(155) + 100, Main.rand.Next(155) + 100 , Main.rand.Next(155) + 100);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
        public override void AI(){
            if (Main.rand.Next(20) == 0)
				CreateDust();
		}
    }
}