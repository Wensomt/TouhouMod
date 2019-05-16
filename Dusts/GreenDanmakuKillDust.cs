using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Dusts
{
	public class GreenDanmakuKillDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 16, 16);
			dust.alpha = 0;
		}
		public override bool Update(Dust dust)
		{
			//dust.alpha += 1;
            dust.scale -= 0.04f;
            dust.position += new Vector2(0.01f,0.01f);
			/*if (dust.alpha > 150)
			{
				dust.active = false;
			}*/
            if (dust.scale < 0.01f)
            {
                dust.active = false;
            }
			return false;
		}
	}
}