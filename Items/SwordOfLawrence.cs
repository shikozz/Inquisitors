using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Inquisitors.Projectiles;
using Terraria.Audio;
using System.Security.Cryptography.X509Certificates;
using Terraria.DataStructures;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Inquisitors.Items
{
	public class SwordOfLawrence : ModItem
	{
        int altCool = 1000;
        int altDelay = 0;
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Sword of Lawrence, paladin who was sent to cure the presence of the cursed Queen");
			DisplayName.SetDefault("Sword Of Lawrence");
		}
        public override void SetDefaults()
        {
			Item.damage = 280;
			Item.DamageType = DamageClass.Melee;
			Item.width = 100;
			Item.height = 100;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10;
			Item.value = 10000;
            Item.shoot = ModContent.ProjectileType <LawrenceProjectile>();
            Item.shootSpeed = 13f;
            Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.useTime = 50;
        }

        /*public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return false;
        }*/

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 120);
			target.AddBuff(BuffID.CursedInferno, 120);
			target.AddBuff(BuffID.Ichor, 120);
			SoundEngine.PlaySound(SoundID.Item74);
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			Item.UseSound = SoundID.Item74;
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            target.AddBuff(BuffID.CursedInferno, 120);
            target.AddBuff(BuffID.Ichor, 120);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
		    int dust = Dust.NewDust(new Vector2(hitbox.X,hitbox.Y), hitbox.Width, hitbox.Height, DustID.FlameBurst, 0f, 0f, 0, Color.Yellow, 0.5f);
            Main.dust[dust].velocity *= 0.2f;
            Main.dust[dust].noGravity = true;

			int dust1 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.FlameBurst, 0f, 0f, 0, Color.Yellow, 1.5f);
            Main.dust[dust1].velocity *= 0.2f;
            Main.dust[dust1].noGravity = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && altDelay <= 0 && player.GetModPlayer<GlobalPlayer>().armorLaw)
            {
                altDelay = altCool;
                for (int i = 0; i <= 20; i++)
                {
                    altDelay = altCool;
                    int newProj = ModContent.ProjectileType<LawrenceProjectile>();
                    ModProjectile mp = ModContent.GetModProjectile(newProj);
                    Vector2 newVelocity = velocity.RotateRandom(MathHelper.ToRadians(30));
                    int proj = Projectile.NewProjectile(Projectile.InheritSource(player), player.position, newVelocity, newProj, 1666, knockback, player.whoAmI);
                    Main.projectile[proj].friendly = true;
                    Main.projectile[proj].timeLeft = 25;
                    player.GetModPlayer<GlobalPlayer>().altEffect = true;
                }
                return false;
            }
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.GetModPlayer<GlobalPlayer>().armorLaw == true)
            {
                if(player.GetModPlayer<GlobalPlayer>().DashAccessoryEquipped && player.GetModPlayer<GlobalPlayer>().ember)
                {
                    Item.rare=ItemRarityID.Master;
                }
                Item.crit = 33;
                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.autoReuse = true;
            }
            else
            {
                Item.autoReuse = false;
                Item.crit = 3;
                Item.useTime = 50;
                Item.useAnimation = 50;
                player.AddBuff(BuffID.BrokenArmor, 120);
            }
            altDelay--;
            if(altDelay<=0 && player.GetModPlayer<GlobalPlayer>().armorLaw)
            {
                player.yoraiz0rEye = Math.Max(player.yoraiz0rEye, 2);
            }
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