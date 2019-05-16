using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Alice
{
	public class HouraiDoll : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hourai Doll");
			Tooltip.SetDefault("A cursed doll... on a leash...");
	    }			
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Chik);
			item.damage = 48;
			item.melee = true;
			item.width = 30;
			item.height = 26;
			item.rare = 5;
			item.value = 500000;
			item.channel = true;
			item.useTime = 25;
			item.useAnimation = 25;
			item.shoot = mod.ProjectileType("HouraiDoll");
		}
		
	}	
}