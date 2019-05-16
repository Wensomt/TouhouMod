using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class StarDust : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Dust");
			Tooltip.SetDefault("The dust of the cosmos");
	    }			
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 26;
			item.maxStack  = 99;
			item.value = 7500;
			item.rare = 7;
		}
		
	}
}