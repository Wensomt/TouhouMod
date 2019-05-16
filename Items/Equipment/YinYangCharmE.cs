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
	public class YinYangCharmE : DivineItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yin Yang Charm E");
			Tooltip.SetDefault("Yin Yang Orbs Circle Around You");
	    }				
		public override void SafeSetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.value = 750000;
			item.rare = 6;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[4])
            {
                player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[4] = true;
                Vector2 position = new Vector2(24f,24f);
                float[] offset = new float[4];
                for (int i = 0 ; i < 3 ; i++)
                {
                    offset[i] = Main.rand.NextFloat() * 50;
                }
				for (int i = 0 ; i < Main.projectile.Length ; i++)
				{
					if (Main.projectile[i].type == mod.ProjectileType("YinYangOrb") || Main.projectile[i].type == mod.ProjectileType("YinYangOrbRev"))
						Main.projectile[i].Kill();
				}
                for (int i = 0 ; i < 4 ; i++)
                {
                    Projectile.NewProjectile(player.Center, position.RotatedBy(offset[0]), mod.ProjectileType("YinYangOrbRev"), 32, 4, player.whoAmI);
                    position = position.RotatedBy(MathHelper.ToRadians(90));
                }
				for (int i = 0 ; i < 6 ; i++)
                {
 				    Projectile.NewProjectile(player.Center, position.RotatedBy(offset[1]) * 2f, mod.ProjectileType("YinYangOrb"), 32, 4, player.whoAmI);
                    position = position.RotatedBy(MathHelper.ToRadians(60));
                }
            	for (int i = 0 ; i < 8 ; i++)
                {
 				    Projectile.NewProjectile(player.Center, position.RotatedBy(offset[2])* 3f, mod.ProjectileType("YinYangOrbRev"), 32, 4, player.whoAmI);
                    position = position.RotatedBy(MathHelper.ToRadians(45));
                }
			}
            player.GetModPlayer<TouhouPlayer>(mod).yinYangOrbProjectileChance = 5;
		}
        /*public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("YinYangCharmD"));
			recipe.AddIngredient(mod.ItemType("PowerItemE"), 50);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}*/

	}
}