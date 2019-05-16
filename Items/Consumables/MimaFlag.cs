using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace TouhouMod.Items.Consumables
{
	public class MimaFlag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mima's Flag");
			Tooltip.SetDefault("Summons the evil spirit, Mima");
	    }
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 19;
			item.maxStack  = 30;
			item.value = 2500;
			item.rare = 2;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = true;
		}
        /*public override bool CanUseItem(Player player)
		{
			//return !Main.dayTime && !NPC.AnyNPCs(mod.NPCType("Marisa"));
		}*/
        public override bool UseItem(Player player)
		{
 
            if (Main.netMode != 2)
            {
                string text = Language.GetTextValue("You can't summon what doesn't exist");
                Main.NewText(text, 125, 0, 175);
            }
            else
            {
                NetworkText text = NetworkText.FromKey("You can't summon what doesn't exist");
                NetMessage.BroadcastChatMessage(text, new Color(125, 0, 175));
            }
            //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Marisa"));
            return true;
        }
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(9);
			recipe.AddIngredient(ItemID.Silk , 3);
			recipe.AddIngredient(ItemID.FallenStar , 1);
            recipe.AddIngredient(ItemID.BeetleHusk, 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}