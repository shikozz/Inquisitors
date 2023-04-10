using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Inquisitors.Projectiles;
using Terraria.Audio;

namespace Inquisitors.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class LawrenceLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Helmet of Lawrence with strange visor, which closes whole face, but Lawrence was able to swing his sword freely."
				+"\n Makes you mad without whole set");
			DisplayName.SetDefault("Lawrence Leggings");
		}
        public override void SetDefaults()
        {
			Item.width = 20;
			Item.height = 20;
			Item.value = 15000;
			Item.rare = ItemRarityID.Red;
			Item.defense = 25;
        }

		public override void UpdateEquip(Player player)
		{
			if (player.GetModPlayer<GlobalPlayer>().armorLaw == false)
			{
				player.AddBuff(BuffID.Confused, 10);
				player.AddBuff(BuffID.VortexDebuff, 10);
			}
			player.statLifeMax2 += 40;
			player.GetCritChance(DamageClass.Melee) += 10;
		}


        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 50);
            recipe.AddIngredient(ItemID.Star, 10);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}