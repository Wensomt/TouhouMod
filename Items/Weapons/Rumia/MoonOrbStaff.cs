using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Rumia
{
	public class MoonOrbStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Orb Staff");
			Tooltip.SetDefault("Summons an orb to release a few bursts of moonlight");
	    }		
		public override void SetDefaults()
		{
			item.damage = 10;
			item.summon = true;
			item.mana = 16;
			item.width = 42;
			item.height = 42;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = 50000;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("MoonlightOrbShooter");
			item.shootSpeed = 0f;
			item.sentry = true;
		}
	}
}