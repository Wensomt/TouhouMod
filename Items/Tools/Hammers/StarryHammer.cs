using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Hammers
{
	public class StarryHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starry Hammer");
	    }			
		public override void SetDefaults()
		{
			item.damage = 68;
			item.melee = true;
			item.width = 46;
			item.height = 44;
			item.useTime = 8;
			item.useAnimation = 24;
			item.hammer = 90;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 500000;
			item.rare = 7;
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
            recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}