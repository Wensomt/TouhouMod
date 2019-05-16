using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Dusts
{
	public class magic : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 30, 30);
			dust.alpha = 120;
			
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.velocity *= 0.95f;
			dust.alpha += 1;
			if (dust.alpha > 150)
			{
				dust.active = false;
			}
			return false;
		}
	}
}