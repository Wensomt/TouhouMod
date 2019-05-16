using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TouhouMod.Projectiles.Minions
{
	public class DollMinion : HoverShooter
	{
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			//projectile.name = "Doll";
			projectile.width = 60;
			projectile.height = 72;
			Main.projFrames[projectile.type] = 2;
			projectile.friendly = true;
			Main.projPet[projectile.type] = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.scale = 0.50f;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			inertia = 20f;
			shoot = mod.ProjectileType("DollScale");
			shootSpeed = 12f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			TouhouPlayer modPlayer = player.GetModPlayer<TouhouPlayer>(mod);
			if (player.dead)
			{
				modPlayer.dollMinion = false;
			}
			if (modPlayer.dollMinion)
			{
				projectile.timeLeft = 2;
			}
		}

		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 2;
			}
		}
	}
}


