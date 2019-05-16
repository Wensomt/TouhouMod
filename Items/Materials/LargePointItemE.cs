using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class LargePointItemE : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Large Point Item E");
			Tooltip.SetDefault("Sells for a handsome amount of money");
	    }			
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 1;
            item.expert = true;
			item.value = 2500000;
			item.rare = 8;
		}
		
	}
}