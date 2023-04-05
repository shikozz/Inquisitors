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
        public int immuneTime = 1;
        public int immuneCooldown = 500;
        public int immuneDelay = 100;
        public int burstDelay = 0;
        public int burstCooldown = 200;
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
			player.setBonus = "Now you able to use strength of Lawrence"
                +"\nProvides all the benefits of Ankh Charm and some more" +
                "\nCreates burst of fire when equipping" +
                "\nGrants temporal invicibility for short time";
            player.buffImmune[BuffID.OnFire] = true;
			player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Stoned] = true;
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
            if(player.GetModPlayer<GlobalPlayer>().showMsg||(player.statLife<=350&&burstDelay==0))
            {
                for (int i = 0; i <= 100; i++)
                {
                    burstDelay = burstCooldown;
                    int newProj = ModContent.ProjectileType<LawrenceProjectile>();
                    ModProjectile mp = ModContent.GetModProjectile(newProj);
                    Vector2 velocity = new Vector2(10f,10f);
                    Vector2 newVelocity = velocity.RotateRandom(MathHelper.ToDegrees(180));
                    int proj = Projectile.NewProjectile(Projectile.InheritSource(player), player.position, newVelocity, newProj, 1666, 20, player.whoAmI);
                    Main.projectile[proj].friendly = true;
                    Main.projectile[proj].timeLeft = 25;
                    Main.projectile[proj].penetrate = 100;
                    Main.projectile[proj].tileCollide = true;
                    player.GetModPlayer<GlobalPlayer>().altEffect = true;
                }
            }
            if (immuneDelay == 0)
            {
                immuneDelay = immuneCooldown;
                if(immuneTime>=1)
                {
                    player.immune = true;
                    player.immuneTime = 100;
                    immuneTime--;
                    //Main.NewText("armor_invul_test", 255, 255, 255);
                }
                else
                {
                    immuneTime = 500;
                }
            }
            immuneDelay--;
            if (burstDelay > 0)
            {
                burstDelay--;
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