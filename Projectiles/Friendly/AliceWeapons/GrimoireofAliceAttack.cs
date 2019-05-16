using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
	public class GrimoireofAliceAttack : ModProjectile
	{
		
		private int timer = 0;
		private int catchDamage = 0;
        private Vector2 catchVel;
		
		public override void SetDefaults(){
			//projectile.name = "Doll";
            projectile.width = 60;
            projectile.height = 74;
            projectile.friendly = true;
            projectile.penetrate = -1;  
            projectile.tileCollide = false;
			projectile.light = 0.5f;
			projectile.sentry = true;
            projectile.scale = 0.60f;
			Main.projFrames[projectile.type] = 4;
		}
        public override void AI()
        {
            if (timer == 0)
            {
                catchDamage = projectile.damage;
                projectile.damage = 0;
                catchVel.X = projectile.velocity.X;
                catchVel.Y = projectile.velocity.Y;
                projectile.velocity = new Vector2(0f, 0f);
                if (catchVel.X > 0)
                    projectile.frame = 1;
                else
                    projectile.frame = 0;
            }
            if (timer > 29)
            {
                if (catchVel.X > 0)
                    projectile.frame = 3;
                else
                    projectile.frame = 2;
                if (timer % 5 == 0)
                    Projectile.NewProjectile(projectile.Center + catchVel * 2f , catchVel.RotatedByRandom(MathHelper.ToRadians(12)) , mod.ProjectileType("DollScale") , catchDamage/4*3 , projectile.knockBack , Main.player[projectile.owner].whoAmI);
            }
            if (timer == 90)
                projectile.Kill();

            timer++;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0 ; i < 12 ; i++)
                CreateDust();
        }
        public virtual void CreateDust()
		{
			Color? color = new Color(200, 100 , 100);
			if (color.HasValue)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
    }
}