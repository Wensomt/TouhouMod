using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.ParseeWeapons
{
	public class EnvyBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ChlorophyteBullet);
			//projectile.name = "Envy Bullet";
			aiType = ProjectileID.ChlorophyteBullet;
            projectile.penetrate = 6;
		}
    }
}