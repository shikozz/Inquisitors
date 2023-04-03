using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Inquisitors.Projectiles;
using Terraria.Audio;

namespace Inquisitors.Armor
{
		[AutoloadEquip(EquipType.Body)]
		public class LawrenceChestplate : ModItem
		{
		public bool text = false;
			public override void SetStaticDefaults()
			{
				Tooltip.SetDefault("Chestplate of Lawrence, really light but was able to withstand strongest strikes"
					+"\n Provides extra mobility"+
					"\n Without whole set you will bleed");
				DisplayName.SetDefault("Lawrence Chestplate");
			}
            public override void SetDefaults()
            {
			Item.width = 20;
			Item.height = 20;
			Item.value = 20000;
			Item.rare = ItemRarityID.Red;
			Item.defense = 30;
            }

        public override void UpdateEquip(Player player)
        {
			player.moveSpeed += 0.7f;
            if (player.GetModPlayer<GlobalPlayer>().armorLaw == false)
            {
                player.AddBuff(BuffID.Bleeding, 10);
				player.AddBuff(BuffID.Suffocation, 10);
				text = true;
            }
            player.statLifeMax2 += 40;
			}


        public override void AddRecipes()
			{
				Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.MeteoriteBar, 150);
                recipe.AddIngredient(ItemID.HallowedBar, 50);
                recipe.AddIngredient(ItemID.Star, 10);
                recipe.AddIngredient(ItemID.AnkhCharm, 1);
                recipe.AddTile(TileID.Anvils);
				recipe.Register();
			}
        }
}