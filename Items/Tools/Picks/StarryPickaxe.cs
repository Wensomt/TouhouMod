using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Picks
{
	public class StarryPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starry Pickaxe");
	    }			
		public override void SetDefaults()
		{
			item.damage = 56;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.useTime = 4;
			item.useAnimation = 16;
			item.pick = 200;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 150000;
			item.rare = 6;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(4) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 20);
			}
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarDust"), 5);
			recipe.AddIngredient(ItemID.ChlorophyteBar , 5);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}