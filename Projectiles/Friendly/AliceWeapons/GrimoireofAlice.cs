using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
	
	
	public class GrimoireofAlice : ModProjectile
	{
        private Vector2 catchMouse;
		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;
			//projectile.name = "Doll";
			projectile.penetrate = 3;
			projectile.light = 0f;
			projectile.timeLeft = 100;
			projectile.friendly = true;
			projectile.tileCollide = true;
            projectile.scale = 0.60f;
		}
        public override void AI()
        {
            projectile.rotation += 0.1f;
            if (catchMouse.X == 0f && catchMouse.Y == 0f)
            {
                catchMouse.X = (float)(Main.mouseX + Main.screenPosition.X) + projectile.velocity.X * 20f;
                catchMouse.Y = (float)(Main.mouseY + Main.screenPosition.Y) + projectile.velocity.Y * 20f;
            }
            if (projectile.Center.X > catchMouse.X - 16f && projectile.Center.X < catchMouse.X + 16f)
            {
                if (projectile.Center.Y > catchMouse.Y - 16f && projectile.Center.Y < catchMouse.Y + 16f)
                    projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center , projectile.velocity.RotatedBy(MathHelper.ToRadians(180)), mod.ProjectileType("GrimoireofAliceAttack"),projectile.damage, projectile.knockBack, Main.player[projectile.owner].whoAmI);
        }
    }
}