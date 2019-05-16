using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class LargePointItemA : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Large Point Item A");
			Tooltip.SetDefault("Sells for a handsome amount of money");
	    }
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 1;
            item.expert = true;
			item.value = 500000;
			item.rare = 3;
		}
		
	}
}