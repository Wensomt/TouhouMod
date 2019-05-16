using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class DollFragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doll Fragment");
			Tooltip.SetDefault("A magic cloth with an elegant design");
	    }		
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 26;
			item.maxStack  = 99;
			item.value = 5000;
			item.rare = 5;
		}
		
	}
}