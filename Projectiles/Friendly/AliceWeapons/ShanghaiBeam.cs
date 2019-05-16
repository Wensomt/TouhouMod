using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
 
namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
    public class ShanghaiBeam : ModProjectile
    {
        public override void SetDefaults()
		{
			projectile.width = 5;
			projectile.height = 5;
			//projectile.name = "ShanghaiBeam";
			projectile.penetrate = -1;
			projectile.light = 0f;
			projectile.timeLeft = 25;
			projectile.friendly = true;
			projectile.tileCollide = false;
            projectile.alpha = 255;
		}
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            Projectile.NewProjectile(projectile.Center, projectile.velocity, mod.ProjectileType("ShanghaiBeamTrail"), projectile.damage, projectile.knockBack, owner.whoAmI);
        }
        public override void Kill(int timeLeft)
        {
            Player owner = Main.player[projectile.owner];
            Projectile.NewProjectile(projectile.Center, projectile.velocity.RotatedBy(MathHelper.ToRadians(180)), mod.ProjectileType("ShanghaiLaserEnd"), projectile.damage, projectile.knockBack, owner.whoAmI);
        }
    }
}