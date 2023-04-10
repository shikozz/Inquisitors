using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Inquisitors.Projectiles;

namespace Inquisitors.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class LawrenceShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shield of Lawrence, can freely be used with greatswords");
			DisplayName.SetDefault("Lawrence Shield");
		}
        public override void SetDefaults()
        {
			Item.width = 20;
			Item.height = 20;
			Item.value = 2500;
			Item.rare = ItemRarityID.Red;
			Item.accessory = true;
            Item.defense = 20;
            Item.lifeRegen = 10;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<GlobalPlayer>().armorLaw == false)
            {
                player.onFire = true;
                player.onFire = hideVisual;
            }
            player.GetModPlayer<GlobalPlayer>().DashAccessoryEquipped = true;

            int newProj = ModContent.ProjectileType<LawrenceShieldProjectile>();
            Vector2 velocity = new Vector2(0f, 0f);
            Vector2 newVelocity = velocity.RotateRandom(MathHelper.ToDegrees(180));
            int proj = Projectile.NewProjectile(Projectile.InheritSource(player), player.position, newVelocity, newProj, 50, 0, player.whoAmI);
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].timeLeft = 2;
            Main.projectile[proj].penetrate = 100;
            Main.projectile[proj].tileCollide = false;
            player.noKnockback = true;
            player.AddBuff(BuffID.PaladinsShield, 1);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PaladinsShield);
            recipe.AddIngredient(ItemID.Star, 5);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}