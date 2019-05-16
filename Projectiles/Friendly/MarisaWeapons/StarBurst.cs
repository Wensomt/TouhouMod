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
    public class StarBurst : ModProjectile
    {
        private float rotateBy;
        private bool bounced = false;
        public override void SetDefaults()
        {
            //projectile.name = "Star Burst";
            projectile.width = 35;
            projectile.height = 35;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;   
            projectile.magic = true;
			projectile.light = 0.5f;
            projectile.scale = 0.66f;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (rotateBy == 0)
                rotateBy = (float)Main.rand.NextDouble();

            projectile.rotation += rotateBy;
            projectile.velocity.Y += 0.25f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
            for (int i = 0; i < 2; i++)
				CreateDust();

            if (!bounced)
            {
			    if (projectile.velocity.X != oldVelocity.X)
			    {
				    projectile.velocity.X = -oldVelocity.X;
			    }
			    if (projectile.velocity.Y != oldVelocity.Y)
			    {
				    projectile.velocity.Y = -oldVelocity.Y;
			    }
                bounced = true;
            }
            else
                projectile.Kill();

			return false;
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

    }
}