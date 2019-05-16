using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Projectiles.Friendly.AliceWeapons
{
	
	public class ExplosiveDollExplosion : ModProjectile
	{
		
		public override void SetDefaults()
		{
			projectile.width = 258;
			projectile.height = 258;
			//projectile.name = "Explosive Doll Explosion";
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
    }
}