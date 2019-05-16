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
	public class YinYangCharmD : DivineItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yin Yang Charm D");
			Tooltip.SetDefault("Yin Yang Orbs Circle Around You");
	    }		
		public override void SafeSetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.value = 500000;
			item.rare = 5;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[3])
            {
                player.GetModPlayer<TouhouPlayer>(mod).hasYinYangOrbs[3] = true;
                Vector2 position = new Vector2(24f,24f);
                float[] offset = new float[3];
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
                    Projectile.NewProjectile(player.Center, position.RotatedBy(offset[1]) * 2f, mod.ProjectileType("YinYangOrb"), 32, 4, player.whoAmI);
                    Projectile.NewProjectile(player.Center, position.RotatedBy(offset[2]) * 3f, mod.ProjectileType("YinYangOrbRev"), 32, 4, player.whoAmI);
                    position = position.RotatedBy(MathHelper.ToRadians(90));
                }
				
            }
            player.GetModPlayer<TouhouPlayer>(mod).yinYangOrbProjectileChance = 5;
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("YinYangCharmC"));
			recipe.AddIngredient(mod.ItemType("PowerItemD"), 50);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}