using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class PowerItemD : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Item D");
			Tooltip.SetDefault("Used in upgrading some weapons");
	    }			
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 10000;
			item.rare = 6;
		}
		
	}
}