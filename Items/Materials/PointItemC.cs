using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class PointItemC : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Point Item C");
			Tooltip.SetDefault("Sells for a bit of money");
	    }		
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack  = 999;
			item.value = 37500;
			item.rare = 5;
		}
		
	}
}