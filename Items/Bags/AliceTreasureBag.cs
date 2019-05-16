using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Items.Bags
{
	public class AliceTreasureBag : ModItem
	{
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 38;
			item.height = 32;
			item.rare = 6;
			item.expert = true;
			bossBagNPC = mod.NPCType("Alice");
		}
	
	    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag : Alice");
			Tooltip.SetDefault("Right click to open");
	    }
        
		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("AliceTrophy"));
			}
			int x = Main.rand.Next(5);
			for (int i = 0; i < 3; i++)
			{
				if (x == 0)
				{
					player.QuickSpawnItem(mod.ItemType("HouraiDoll"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 1)
				{
					player.QuickSpawnItem(mod.ItemType("ElegantBow"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 2)
				{
					player.QuickSpawnItem(mod.ItemType("ShanghaiDoll"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 3)
				{
					player.QuickSpawnItem(mod.ItemType("GrimoireofAlice"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
				}
				else if (x == 4)
				{
					player.QuickSpawnItem(mod.ItemType("ExplosiveDoll"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
				}
			}
			x = (Main.rand.Next(10) + 6);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("DollFragment"));
			x = (Main.rand.Next(3) + 1);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("SCArtfulSacrifice"));
		}
	}
}