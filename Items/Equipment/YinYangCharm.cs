using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using System.Text;

namespace TouhouMod.Items.Equipment
{
	public class YinYangCharm : DivineItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yin Yang Charm A");
			Tooltip.SetDefault("Yin Yang Orbs Circle Around You");
	    }		
		public override void SafeSetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.value = 50000;
			item.rare = 2;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[0])
            {
				for (int i = 0 ; i < Main.projectile.Length ; i++)
				{
					if (Main.projectile[i].type == mod.ProjectileType("YinYangOrb") || Main.projectile[i].type == mod.ProjectileType("YinYangOrbRev"))
						Main.projectile[i].Kill();
				}
                Projectile.NewProjectile(player.position.X + (player.width/2), player.position.Y + (player.height/2), 56f, 56f, mod.ProjectileType("YinYangOrb"), 12, 4, player.whoAmI);
                Projectile.NewProjectile(player.position.X + (player.width/2), player.position.Y + (player.height/2), -56f, -56f, mod.ProjectileType("YinYangOrb"), 12, 4, player.whoAmI);
                player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[0] = true;
            }
            player.GetModPlayer<TouhouPlayer>(mod).yinYangOrbProjectileChance = 0;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("YinYangOrb"));
			recipe.AddIngredient(mod.ItemType("PowerItemA"), 75);
			recipe.AddIngredient(ItemID.StoneBlock, 20);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}