using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.ParseeWeapons
{
	public class EnvyThrowP : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(546);
			projectile.width = 12;
			projectile.height = 12;
			//projectile.name = "Envy Throw";
			projectile.penetrate = -1;
			projectile.light = 1f;
		}
		public override void AI()
		{
			if ((Main.rand.Next(24) == 1))
			{
				Vector2 perturbedSpeed = new Vector2(4f, 4f).RotatedByRandom(MathHelper.ToRadians(360));
				float speedX = perturbedSpeed.X;
				float speedY = perturbedSpeed.Y;
				Player owner = Main.player[projectile.owner];
				Projectile.NewProjectile(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2), perturbedSpeed.X, perturbedSpeed.Y, 207, 18, 1 , owner.whoAmI);
			}
		}
	}
}