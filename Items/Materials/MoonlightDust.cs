using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class MoonlightDust : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Dust");
			Tooltip.SetDefault("It gives off a faint blue light");
	    }		
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack  = 99;
			item.value = 1250;
			item.rare = 2;
		}
		
	}
}