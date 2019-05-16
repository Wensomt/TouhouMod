using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
	
	public class ArtfulSacrificeExplosion : ModProjectile
	{
		
		public override void SetDefaults()
		{
			projectile.width = 1024;
			projectile.height = 1024;
			//projectile.name = "Artful Sacrifice Explosion";
			projectile.penetrate = -1;
			projectile.light = 0f;
			projectile.timeLeft = 3;
			projectile.friendly = true;
			projectile.tileCollide = false;
            projectile.alpha = 255;
		}
        public virtual void OnHitNPC (Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
		public override void AI()
		{
			for (int i = 0 ; i < Main.projectile.Length ; i++)
			{
				if (Main.projectile[i].hostile && Main.projectile[i].damage > 0 && Contains(Main.projectile[i].Center))
					Main.projectile[i].Kill();
			}
		}
		public bool Contains(Vector2 placement)
		{
			if (placement.X > projectile.position.X && placement.X < projectile.position.X + 1024f && placement.Y > projectile.position.Y && placement.Y < projectile.position.Y + 1024f)
				return true;
			return false;
		}
    }
}