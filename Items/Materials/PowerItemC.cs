using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class PowerItemC : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Item C");
			Tooltip.SetDefault("Used in upgrading some weapons");
	    }			
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 7500;
			item.rare = 5;
		}
		
	}
}