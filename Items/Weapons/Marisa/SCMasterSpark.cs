using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Marisa
{
	public class SCMasterSpark : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Love Colored Sign : Master Spark");
			Tooltip.SetDefault("Honestly, how could you not see this coming?");
	    }			
		public override void SetDefaults()
		{
			item.damage = 184;
			item.width = 30;
			item.height = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 7500;
			item.rare = 8;
			item.expert = true;
			item.autoReuse = false;
			item.consumable = true;
			item.useAnimation = 80;
			item.useTime = 80;
			item.maxStack = 99;
			item.shootSpeed = 8f;
			item.UseSound = SoundID.Item20;
			item.shoot = mod.ProjectileType("Hakkero");
		}	
	}
}