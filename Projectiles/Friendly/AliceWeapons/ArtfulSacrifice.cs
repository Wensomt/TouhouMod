using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
	
	
	public class ArtfulSacrifice : ModProjectile
	{
		private int catchDamage = 0;
        private int timer = 0;
		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 50;
			//projectile.name = "ArtfulSacrifice";
			projectile.penetrate = 1;
			projectile.light = 0f;
			projectile.timeLeft = 100;
			projectile.friendly = true;
			projectile.tileCollide = true;
            projectile.scale = 0.5f;
		}
        public override void AI()
        {
            projectile.velocity.Y += 0.1f;
            timer++;
            if (catchDamage == 0)
            {
                catchDamage = projectile.damage;
                projectile.damage = 0;
            }
            if (timer % 3 == 0)
            {
                CreateDust();
            }
            for (int k = 0 ; k < Main.projectile.Length ; k++)
			{
				if (Main.projectile[k].hostile && Main.projectile[k].damage > 1 && Contains(Main.projectile[k].Center))
				{
					Main.projectile[k].Kill();
				}
			}
            projectile.rotation += 0.2f;
        }
        public virtual void CreateDust()
		{
			Color? color = new Color(Main.rand.Next(155) + 100, Main.rand.Next(155) + 100 , Main.rand.Next(155) + 100);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
        public virtual void CreateDustEx()
		{
			Color? color = new Color(Main.rand.Next(155) + 100, Main.rand.Next(155) + 100 , Main.rand.Next(155) + 100);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, 0f, 0f, 0, color.Value, 8f);
				Main.dust[dust].velocity *= 20f;
			}
		}
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center , new Vector2(0f,0f) , mod.ProjectileType("ArtfulSacrificeExplosion") , catchDamage , 16f , Main.player[projectile.owner].whoAmI);
            for (int i = 0 ; i < 48 ; i++)
                CreateDustEx();
            Main.PlaySound(SoundID.Item14, projectile.position);

        }
		public bool Contains(Vector2 placement)
		{
			if (placement.X > projectile.Center.X - 80f && placement.X < projectile.Center.X + 80f && placement.Y > projectile.Center.Y - 80f && placement.Y < projectile.Center.Y + 80f)
				return true;
			return false;
		}
    }
}