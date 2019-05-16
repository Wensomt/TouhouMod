using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.ParseeWeapons
{
	public class EnvyBulb : ModProjectile
	{
		
		private int timer = 0;
		private bool b = false;
		private int d = 0;
		
		public override void SetDefaults(){
			//projectile.name = "Envy Bulb";
            projectile.width = 56;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = true;
			projectile.light = 0.5f;
			projectile.sentry = true;
			Main.projFrames[projectile.type] = 2;
		}
		
		public override void AI()
		{
			
			if (projectile.frame == 0 && timer % 20 == 0)
				projectile.frame = 1;
			else if (timer % 20 == 0)
				projectile.frame = 0;
			
			
			if (d == 0)
			{
				d = projectile.damage;
				projectile.velocity.Y = 8f;
			}

			projectile.damage = 0;
			timer++;
			
			if (timer % 100 == 0)
			{
				Player owner = Main.player[projectile.owner];
				int numberOfProjectiles = Main.rand.Next(2) + 3;
				for (int i = 0 ; i < numberOfProjectiles ; i++)
					Projectile.NewProjectile(projectile.position.X + (projectile.width/2), projectile.position.Y + (projectile.height/4 * 3), (float)(Main.rand.NextDouble() * 6 - 3.0), (float)(Main.rand.NextDouble() * 2 - 6), mod.ProjectileType("EnvyBulbShot"), d, 2, owner.whoAmI);
			}
			if (!b)
			{
				b = !b;
				projectile.position.X = (int)(Main.mouseX + Main.screenPosition.X) - (projectile.width / 2);
				projectile.position.Y = (int)(Main.mouseY + Main.screenPosition.Y) - (projectile.height / 2);
			}
			if (timer == 500)
			{
				for (int i = 0 ; i < 5; i++)
					CreateDust();
				
				projectile.Kill();
			}
		}
		public virtual void CreateDust()
		{
			Color? color = new Color(100,250,100);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 44, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{	
			projectile.velocity.Y = 0f;
			projectile.position.Y += 6f;
			return false;
		}
	}

}	