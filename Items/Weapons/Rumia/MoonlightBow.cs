using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Rumia
{
	public class MoonlightBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Bow");
			Tooltip.SetDefault("Converts arrows into a solid beam of moonlight");
	    }		
		public override void SetDefaults()
		{
			item.damage = 16;
			item.ranged = true;
			item.width = 20;
			item.height = 40;
			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 50000;
			item.rare = 2;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 4f;
			item.useAmmo = AmmoID.Arrow;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("MoonlightBeam");

			return true;
		}

	}
}



