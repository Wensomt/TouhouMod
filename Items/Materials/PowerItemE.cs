using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class PowerItemE : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Item E");
			Tooltip.SetDefault("Used in upgrading some weapons");
	    }			
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 12500;
			item.rare = 9;
		}
		
	}
}