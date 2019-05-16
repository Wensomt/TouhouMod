using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Rumia
{
	public class MoonlightThrower : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Thrower");
			Tooltip.SetDefault("Throws enhanced moonlight knives");
	    }			
		public override void SetDefaults()
		{
			item.damage = 24;
			item.thrown = true;
			item.width = 28;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 50000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MoonlightKnife");
			item.shootSpeed = 16f;
			item.noMelee = false;
			item.noUseGraphic = true;
			item.useTurn = true;
		}

	}
}