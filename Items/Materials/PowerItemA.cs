using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class PowerItemA : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Item A");
			Tooltip.SetDefault("Used in upgrading some weapons");
	    }			
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 2500;
			item.rare = 2;
		}
		
	}
}