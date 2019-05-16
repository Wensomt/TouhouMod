using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Parsee
{
	public class TrueGreenEyedBeast : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Green Eyed Beast");
			Tooltip.SetDefault("The gun itself seems alive with envy");
	    }			
		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 4;
			item.value = 200000;
			item.rare = 4;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.shoot = 10;
			item.shootSpeed = 4f;
			item.useAmmo = AmmoID.Bullet;
			item.expert = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			/*
			if (type == ProjectileID.Bullet)
			{
				type = 207;
			}
			float numberProjectiles = 8;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			}
			return false;
			*/
			type = mod.ProjectileType("EnvyBullet");
			return true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("GreenEyedBeast"));
			recipe.AddIngredient(mod.ItemType("LargePowerItemB"));
			recipe.AddIngredient(mod.ItemType("PowerItemB"), 50);
			recipe.AddIngredient(mod.ItemType("EnvyCrystal"), 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
		
	}
}