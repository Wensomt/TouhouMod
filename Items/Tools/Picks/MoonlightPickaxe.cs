using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Picks
{
	public class MoonlightPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Pickaxe");
			Tooltip.SetDefault("A pickaxe forged to provide light on use");
	    }			
		public override void SetDefaults()
		{
			item.damage = 8;
			item.melee = true;
			item.width = 34;
			item.height = 34;
			item.useTime = 12;
			item.useAnimation = 24;
			item.pick = 70;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 5000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 20);
			}
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MoonlightDust"), 4);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}