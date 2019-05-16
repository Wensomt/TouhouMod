using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Parsee
{
	public class SCGreenEyedBeast : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jealousy Sign : Green Eyed Monster");
			Tooltip.SetDefault("Control your inter green-eyed beast");
	    }			
		public override void SetDefaults()
		{
			item.damage = 24;
			item.width = 30;
			item.height = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 1000;
			item.rare = 4;
			item.expert = true;
			item.autoReuse = false;
			item.consumable = true;
			item.useAnimation = 48;
			item.useTime = 48;
			item.maxStack = 99;
			item.shootSpeed = 8f;
			item.UseSound = SoundID.Item20;
			item.shoot = mod.ProjectileType("GreenEyedShotFollow");
		}	
	}
}