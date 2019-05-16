using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.ParseeWeapons
{
	
	
	public class BloomingEnvy : ModProjectile
	{
		private bool b = false;
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			//projectile.name = "Blooming Envy";
			projectile.penetrate = 1;
			projectile.light = 2f;
			projectile.timeLeft = 1200;
			projectile.friendly = true;
			projectile.tileCollide = false;
			
		}
		public override void AI()
		{
			projectile.rotation += 0.05f;
			if (!b)
			{
				b = !b;
				projectile.position.X = (int)(Main.mouseX + Main.screenPosition.X) - 15;
				projectile.position.Y = (int)(Main.mouseY + Main.screenPosition.Y) - 15;
			}
			
			
		}
		public override void Kill(int timeLeft)
		{
			int x = Main.rand.Next(4) + 2;
			for (int i = 0 ; i < x ; i++)
			{
				Vector2 perturbedSpeed = new Vector2(2f, 2f).RotatedByRandom(MathHelper.ToRadians(360));
				float speedX = perturbedSpeed.X;
				float speedY = perturbedSpeed.Y;
				Player owner = Main.player[projectile.owner];
				Projectile.NewProjectile(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2), perturbedSpeed.X, perturbedSpeed.Y, 207, projectile.damage / 2, 1 , owner.whoAmI);
			}
		}
	}
}