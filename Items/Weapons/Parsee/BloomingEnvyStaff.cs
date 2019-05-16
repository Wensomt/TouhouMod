using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Parsee
{
	public class BloomingEnvyStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blooming Envy Staff");
			Tooltip.SetDefault("Creates a stationary flower of blooming jealousy");
	    }		
		public override void SetDefaults(){
			item.damage = 28;
			item.magic = true;
			item.mana = 16;
			item.width = 42;
			item.height = 42;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = 100000;
			item.rare = 4;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("BloomingEnvy");
			item.shootSpeed = 0f;
		}
	}
}