using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Alice
{
	public class GrimoireofAlice : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grimoire of Alice");
			Tooltip.SetDefault("Summon a doll to attack");
	    }		
		public override void SetDefaults()
		{
			item.damage = 35;
            item.mana = 8;
            item.summon = true;
			item.width = 28;
			item.height = 30;
			item.useStyle = 1;
			item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 1;
			item.value = 500000;
			item.rare = 5;
			item.autoReuse = false;
			item.useAnimation = 40;
			item.useTime = 40;
			item.shootSpeed = 10f;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("GrimoireofAlice");
		}
	}
}