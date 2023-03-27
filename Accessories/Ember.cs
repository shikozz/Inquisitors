using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Inquisitors.Projectiles;

namespace Inquisitors.Accessories
{
	public class Ember : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Piece of Ember, which was found after Lawrence`s executuions");
			DisplayName.SetDefault("Ember of Lawrence");
		}
        public override void SetDefaults()
        {
			Item.width = 20;
			Item.height = 20;
			Item.value = 2500;
			Item.rare = ItemRarityID.Red;
			Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (player.GetModPlayer<GlobalPlayer>().armorLaw == false)
			{
				player.onFire = true;
				player.onFire = hideVisual;
			}
			player.GetModPlayer<GlobalPlayer>().ember = true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LavaBucket, 5);
            recipe.AddIngredient(ItemID.SpookyWood, 150);
            recipe.AddIngredient(ItemID.Star, 5);
            recipe.AddIngredient(ItemID.AnkhCharm, 1);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}