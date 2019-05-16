using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.DarkAlchemistEasyWeapons
{
	public class SpectralGrasp : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			//projectile.name = "Spectral Grasp";
			projectile.penetrate = -1;
			projectile.light = 2f;
			projectile.timeLeft = 241;
            projectile.alpha = 120;
			projectile.friendly = true;
			projectile.tileCollide = false;
		}
        public override void AI()
        {
            for (int i = 0 ; i < Main.projectile.Length ; i++)
            {
                if (Main.projectile[i].hostile && Main.projectile[i].damage > 0 && Main.projectile[i].Center.X > projectile.Center.X - 12f && Main.projectile[i].Center.X < projectile.Center.X + 12f && Main.projectile[i].Center.Y > projectile.Center.Y - 12f && Main.projectile[i].Center.Y + Main.projectile[i].height/2 < projectile.Center.Y + 12f)
                {
                    Main.projectile[i].velocity = projectile.velocity;
                    Main.projectile[i].timeLeft = projectile.timeLeft;
                    Main.projectile[i].damage = 0;
                }
            }
        }
    }
}