using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Marisa
{
	public class ThrowingStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Throwing Star");
			Tooltip.SetDefault("A comet in the palm of your hand");
	    }		
		public override void SetDefaults(){
			item.damage = 56;
			item.thrown = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 750000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ThrowingStar");
			item.shootSpeed = 12f;
			item.noMelee = false;
			item.noUseGraphic = true;
			item.useTurn = true;
		}

	}
}