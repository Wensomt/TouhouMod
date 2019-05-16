using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items
{
	public class YinYangOrb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yin Yang Orb");
			Tooltip.SetDefault("The beast ate this orb... It doesn't seem to be from this realm...");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack  = 1;
			item.value = 2500;
			item.rare = 2;
		}
	}
}