using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class PointItemE : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Point Item E");
			Tooltip.SetDefault("Sells for a bit of money");
	    }		
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack  = 999;
			item.value = 62500;
			item.rare = 7;
		}
		
	}
}