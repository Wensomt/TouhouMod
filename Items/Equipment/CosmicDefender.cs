using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment
{
	public class CosmicDefender : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Defender");
			Tooltip.SetDefault("Taking damage unleases a thick cosmic storm and enchances magic capablities."
			+ "\n12% increased magic damage");
	    }				
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 100000;
			item.rare = 6;
			item.accessory = true;
            item.defense = 3;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<TouhouPlayer>(mod).cosmicDefender = true;
            player.magicDamage *= 1.12f + (float)(player.GetModPlayer<TouhouPlayer>(mod).cosmicDefenderBonus / 150f);
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltShield);
            recipe.AddIngredient(ItemID.StarVeil);
            recipe.AddIngredient(ItemID.SorcererEmblem);
			recipe.AddIngredient(mod.ItemType("StarDust"), 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

    }
}