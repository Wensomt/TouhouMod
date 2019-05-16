using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Items.Bags
{
	public class MarisaTreasureBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag : Marisa");
			Tooltip.SetDefault("Right click to open");
	    }

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 38;
			item.height = 32;
			item.rare = 7;
			item.expert = true;
			bossBagNPC = mod.NPCType("Marisa");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("MarisaTrophy"));
			}
			int x = Main.rand.Next(5);
			for (int i = 0; i < 3; i++)
			{
				if (x == 0)
				{
					player.QuickSpawnItem(mod.ItemType("FistoftheNorthStar"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 1)
				{
					player.QuickSpawnItem(mod.ItemType("StarDuster"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 2)
				{
					player.QuickSpawnItem(mod.ItemType("StarBurstStaff"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 3)
				{
					player.QuickSpawnItem(mod.ItemType("StarryNight"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
					}
				else if (x == 4)
				{
					player.QuickSpawnItem(mod.ItemType("ThrowingStar"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
					}
				}
			
			x = (Main.rand.Next(10) + 6);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("StarDust"));
			
			x = (Main.rand.Next(2) + 1);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("SCMasterSpark"));
		}
	}
}