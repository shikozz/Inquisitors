using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Inquisitors.Projectiles;
using Terraria.Audio;

namespace Inquisitors.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class LawrenceHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Helmet of Lawrence with strange visor, which closes whole face, but Lawrence was able to swing his sword freely."
				+"\nGreatly increases damage without whole set, but provides no vision");
			DisplayName.SetDefault("Lawrence Helemt");
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
				player.GetDamage(DamageClass.Melee) += 1f;
                player.AddBuff(BuffID.Obstructed, 10);
            }
			player.statLifeMax2 += 40;
			player.GetCritChance(DamageClass.Melee) += 10f;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
			return body.type == ModContent.ItemType<LawrenceChestplate>() && legs.type == ModContent.ItemType<LawrenceLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
			player.setBonus = "Now you able to use strength of Lawrence";
            player.buffImmune[BuffID.OnFire] = true;
			player.AddBuff(BuffID.Ironskin, 1);
            player.GetDamage(DamageClass.Melee)+=0.4f;
			player.GetModPlayer<GlobalPlayer>().armorLawSet = true;
			int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Ash, 0f, 0f, 0,default,0.7f) ;
			if(player.velocity.X!=0||player.velocity.Y!=0)
			{
                int dust1 = Dust.NewDust(player.position, player.width, player.height, DustID.FlameBurst, 0f, 0f, 0, default, 0.7f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity *= 0f;
            }
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0f;	
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