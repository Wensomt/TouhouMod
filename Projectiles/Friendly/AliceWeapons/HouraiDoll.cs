using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
	public class HouraiDoll : ModProjectile
	{
        private int timer;
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Chik);
			projectile.width = 16;
			projectile.height = 24;
			//projectile.name = "Hourai Doll";
			projectile.penetrate = -1;
			projectile.light = 1f;
		}
		public override void AI()
		{
			timer++;
            if (timer % 100 == 0)
            {
                Vector2 speed = new Vector2(5f,0f);
                for (int i = 0 ; i < 8 ; i++)
                {
                    Projectile.NewProjectile(projectile.Center, speed, mod.ProjectileType("HouraiScale") , projectile.damage , projectile.knockBack , Main.player[projectile.owner].whoAmI);
                    speed = speed.RotatedBy(MathHelper.ToRadians(45));
                }
            }
		}
	}
}