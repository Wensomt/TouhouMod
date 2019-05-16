using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Rumia
{
	public class TrueMoonlightBurst : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Moonlight Burst");
			Tooltip.SetDefault("Casts a compact moonlit orb");
	    }			
		public override void SetDefaults(){
			item.damage = 12;
			item.magic = true;
			item.mana = 6;
			item.width = 28;
			item.height = 32;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = 100000;
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MoonlightOrb");
			item.shootSpeed = 12f;
			item.expert = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("MoonlightOrb");
			float numberProjectiles = 4 + Main.rand.Next(3);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MoonlightBurst"));
			recipe.AddIngredient(mod.ItemType("LargePowerItemA"));
			recipe.AddIngredient(mod.ItemType("PowerItemA"), 50);
			recipe.AddIngredient(mod.ItemType("MoonlightDust"), 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}