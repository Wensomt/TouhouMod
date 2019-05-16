using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Marisa
{
	public class FistoftheNorthStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fist of the North Star");
			Tooltip.SetDefault("The best way to attack enemies? Lunge yourself at them!");
	    }			
		public override void SetDefaults(){
			item.damage = 120;
            item.mana = 60;
			item.melee = true;
			item.width = 20;
			item.height = 20;
			item.useTime = 80;
			item.useAnimation = 80;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 750000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("FistoftheNorthStar");
			item.shootSpeed = 16f;
			item.noMelee = false;
			item.noUseGraphic = true;
			item.useTurn = true;
		}
		public override bool CanUseItem(Player player)
		{
			return player.FindBuffIndex(mod.BuffType("MotionSickness")) == -1;
		}
	}
}
