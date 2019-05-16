using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Prismriver
{
	public class LunasaBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunasa's Violin Bow");
			Tooltip.SetDefault("'Unwanted noise like you should be drowned out'");
	    }			
		public override void SetDefaults()
		{
			item.damage = 56;
			item.ranged = true;
			item.width = 10;
			item.height = 40;
			item.useTime = 80;
			item.useAnimation = 80;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = 100000;
			item.rare = 8;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 4f;
			item.useAmmo = AmmoID.Arrow;
		
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
				type = mod.ProjectileType("LunasaBowShotLarge");
			
			return true;
		}
	}
}