using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.RumiaWeapons
{
	public class MoonlightOrbShooter : ModProjectile
	{
		
		private int timer = 0;
		private bool b = false;
		private int d = 0;
		
		public override void SetDefaults(){
			//projectile.name = "Moonlight Orb";
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;
			projectile.light = 0.5f;
			projectile.timeLeft = 3600;
			projectile.sentry = true;
		}
		
		public override void AI()
		{
			if (d == 0)
			{
				d = projectile.damage;
			}

			projectile.damage = 0;
			timer++;
			
			if (timer % 100 == 0)
			{
				
				Player player = Main.player[projectile.owner];
				float numberProjectiles = 5;
				float rotation = MathHelper.ToRadians(180);
				Vector2 v = new Vector2(8f,8f).RotatedByRandom(360);
				for (int i = 0; i <= numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = v.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					Projectile.NewProjectile(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MoonlightOrbS"), d, 1, player.whoAmI);
					CreateDust();
					CreateDust();
					CreateDust();
				}
				
			}
			if (!b)
			{
				b = !b;
				projectile.position.X = (int)(Main.mouseX + Main.screenPosition.X) - 15;
				projectile.position.Y = (int)(Main.mouseY + Main.screenPosition.Y) - 15;
			}
			if (timer == 400)
				projectile.Kill();
			
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