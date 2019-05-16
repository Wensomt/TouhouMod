using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.MarisaWeapons
{
	public class StarryNight : ModProjectile
	{
		
		private int timer = 0;
		private bool b = false;
		private int d = 0;
		
		public override void SetDefaults(){
			//projectile.name = "StarryNight";
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;  
            projectile.tileCollide = true;
			projectile.timeLeft = 3600;
			projectile.sentry = true;
            projectile.alpha = 255;
		}
		
		public override void AI()
		{
			if (d == 0)
			{
				d = projectile.damage;
			}

			projectile.damage = 0;
			timer++;
			
			if (timer % 5 == 0)
			{
				Player owner = Main.player[projectile.owner];
                Vector2 speed = new Vector2(0f,10f).RotatedByRandom(MathHelper.ToRadians(30));
                Projectile.NewProjectile(projectile.position.X + projectile.width - Main.rand.Next(512) + 258, projectile.position.Y - 600, speed.X, speed.Y, ProjectileID.HallowStar, d, 4, owner.whoAmI);
			}
			if (!b)
			{
				b = !b;
				projectile.position.X = (int)(Main.mouseX + Main.screenPosition.X) - 15;
				projectile.position.Y = (int)(Main.mouseY + Main.screenPosition.Y) - 15;
			}
			if (timer == 300)
				projectile.Kill();
			
		}
	
	}

}	