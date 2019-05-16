using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class LargePowerItemB : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Large Power Item B");
			Tooltip.SetDefault("Used in upgrading some weapons to their true form");
	    }		
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 1;
            item.expert = true;
			item.value = 20000;
			item.rare = 4;
		}
		
	}
}