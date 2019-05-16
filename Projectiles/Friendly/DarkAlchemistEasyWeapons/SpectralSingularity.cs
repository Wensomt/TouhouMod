using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.DarkAlchemistEasyWeapons
{
	public class SpectralSingularity : ModProjectile
	{
		private bool initialized = false;
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			//projectile.name = "Spectral Singularity";
			projectile.penetrate = -1;
			projectile.light = 2f;
			projectile.timeLeft = 240;
			projectile.friendly = true;
			projectile.tileCollide = false;
            projectile.alpha = 255;
		}
        public override void AI()
        {
            if (Main.rand.Next(3) == 1)
                CreateDust();

            if (!initialized)
            {
                projectile.position.X = (int)(Main.mouseX + Main.screenPosition.X) - 15;
				projectile.position.Y = (int)(Main.mouseY + Main.screenPosition.Y) - 15;
                projectile.velocity = new Vector2(0f,0f);

                Vector2 vector = new Vector2(300f, 0f);
                int numberofProjectiles = 36;
                float angle = MathHelper.ToRadians(10);
                for (int i = 0 ; i < numberofProjectiles ; i++)
                {
                    Projectile.NewProjectile(vector.RotatedBy(angle * i) + projectile.Center, vector.RotatedBy(angle * 18 + angle * i) / 240, mod.ProjectileType("SpectralGrasp"), projectile.damage, 4f, Main.player[projectile.owner].whoAmI);
                }
                
                initialized = true;
            }
        }
        public virtual void CreateDust()
		{
			Color? color = new Color(255,50,50);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
        public override void Kill(int timeLeft)
        {
            for (int i = 0 ; i < Main.projectile.Length ; i++)
            {
                if (Main.projectile[i].hostile && Main.projectile[i].Center.X > projectile.Center.X - 50f && Main.projectile[i].Center.X < projectile.Center.X + 50f && Main.projectile[i].Center.Y > projectile.Center.Y - 50f && Main.projectile[i].Center.Y < projectile.Center.Y + 50f)
                    for (int k = 0; k < 3; k++)
                        Projectile.NewProjectile(projectile.Center, new Vector2(0f, -1f), mod.ProjectileType("HomingEnergy"), 56, 2f, Main.player[projectile.owner].whoAmI);
            }
        }
    }
}