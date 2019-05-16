using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Standard.Laser
{
    public class PurpleLaserWarning : ModProjectile
    {
        private bool rotated = false;
        public override void SetDefaults()
        {
            //projectile.name = "Laser Warning";
            projectile.width = 16;
            projectile.height = 80;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.light = 1.5f;
            projectile.timeLeft = 2;
            Main.projFrames[projectile.type] = 2;
        }
        public override void AI()
        {
            if (!rotated)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                rotated = true;
                projectile.velocity = new Vector2(0f,0f);
                projectile.scale = projectile.knockBack;
                if (projectile.damage == 1)
                {
                    projectile.frame = 1;
                }
                projectile.damage = 0;
            }

        }
    }
}