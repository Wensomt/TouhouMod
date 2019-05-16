using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Materials
{
	public class FrozenShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Shard");
			Tooltip.SetDefault("'This shard is full of condensed ice magic'");
	    }
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack  = 99;
			item.value = 2500;
			item.rare = 2;
		}
		
	}
}