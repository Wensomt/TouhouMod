using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Items.Equipment.Armor.Moonlight
{
    [AutoloadEquip(EquipType.Head)]	
	public class MoonlightHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Helm");
			Tooltip.SetDefault("8% increased magic damage"
			+ "\n+25 Max Mana");
	    }
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 3000;
			item.rare = 2;
			item.defense = 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MoonlightChestpiece") && legs.type == mod.ItemType("MoonlightLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "12% increased magic crit during nighttime";
            if (!Main.dayTime)
            {
                player.magicCrit += 12;
            }
            if (Main.rand.NextFloat() < .10f)
				CreateDust(player);
		}
		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 25;
            player.magicDamage *= 1.04f;
		}
        public virtual void CreateDust(Player player)
		{
			Color? color = new Color(100,100,250);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(player.position, player.width, player.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 2f;
			}
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MoonlightDust"), 4);
			recipe.AddIngredient(ItemID.Silk, 4);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}