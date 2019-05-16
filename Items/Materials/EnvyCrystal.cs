using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class EnvyCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Crystal");
			Tooltip.SetDefault("A pressurized piece of envy that had time to crystalize and grow");
	    }				
		public override void SetDefaults()
		{
			item.width = 23;
			item.height = 23;
			item.maxStack  = 99;
			item.value = 2500;
			item.rare = 2;
		}
		
	}
}