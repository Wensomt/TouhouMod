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
    public class StarBurstLarge : ModProjectile
    {
        private float rotateBy;

        public override void SetDefaults()
        {
            //projectile.name = "Star Burst";
            projectile.width = 35;
            projectile.height = 35;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;   
            projectile.magic = true;
			projectile.light = 1f;
            projectile.timeLeft = 100;
        }
        public override void AI()
        {
            if (rotateBy == 0)
                rotateBy = (float)Main.rand.NextDouble();

            projectile.velocity.Y += 0.1f;
            projectile.rotation += rotateBy;
        }
        public override void Kill(int timeLeft)
        {
            Player owner = Main.player[projectile.owner];
            int numberofProjectiles = Main.rand.Next(4) + 6;
            for (int i = 0; i < numberofProjectiles; i++)
            {
                Vector2 shootSpeed = new Vector2(8f,0f).RotatedByRandom(MathHelper.ToRadians(360)) * (float)(Main.rand.NextDouble() * 0.5 + 1.0);
                Projectile.NewProjectile(projectile.position.X + projectile.width/2, projectile.position.Y + projectile.height/2, shootSpeed.X, shootSpeed.Y, mod.ProjectileType("StarBurst"), projectile.damage/4 * 3, 2, owner.whoAmI);
            }
        }
    }
}
