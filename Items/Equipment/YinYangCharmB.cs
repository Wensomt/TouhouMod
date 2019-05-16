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
	public class YinYangCharmB : DivineItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yin Yang Charm B");
			Tooltip.SetDefault("Yin Yang Orbs Circle Around You");
	    }				
		public override void SafeSetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.value = 100000;
			item.rare = 3;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[1])
            {
				for (int i = 0 ; i < Main.projectile.Length ; i++)
				{
					if (Main.projectile[i].type == mod.ProjectileType("YinYangOrb") || Main.projectile[i].type == mod.ProjectileType("YinYangOrbRev"))
						Main.projectile[i].Kill();
				}
                Projectile.NewProjectile(player.position.X + (player.width/2), player.position.Y + (player.height/2), 56f, 56f, mod.ProjectileType("YinYangOrb"), 14, 4, player.whoAmI);
                Projectile.NewProjectile(player.position.X + (player.width/2), player.position.Y + (player.height/2), -56f, -56f, mod.ProjectileType("YinYangOrb"), 14, 4, player.whoAmI);
                Projectile.NewProjectile(player.position.X + (player.width/2), player.position.Y + (player.height/2), 32f, -32f, mod.ProjectileType("YinYangOrbRev"), 14, 4, player.whoAmI);
                Projectile.NewProjectile(player.position.X + (player.width/2), player.position.Y + (player.height/2), -32f, 32f, mod.ProjectileType("YinYangOrbRev"), 14, 4, player.whoAmI);
                player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[1] = true;
				
            }
			
            player.GetModPlayer<TouhouPlayer>(mod).yinYangOrbProjectileChance = 2;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("YinYangCharm"));
			recipe.AddIngredient(mod.ItemType("PowerItemB"), 50);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}