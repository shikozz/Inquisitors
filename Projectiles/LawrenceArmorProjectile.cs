using ReLogic.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Inquisitors.Projectiles;

namespace Inquisitors.Projectiles
{
    public class LawrenceArmorProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sword Of Lawrence Cursed Flame");
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.damage = 160;
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 50;
            Projectile.light = 1.5f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.stepSpeed = 13f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            target.AddBuff(BuffID.CursedInferno, 120);
            target.AddBuff(BuffID.Ichor, 120);
            SoundEngine.PlaySound(SoundID.Item74);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            target.AddBuff(BuffID.CursedInferno, 120);
            target.AddBuff(BuffID.Ichor, 120);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            target.AddBuff(BuffID.CursedInferno, 120);
            target.AddBuff(BuffID.Ichor, 120);
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item20);
            int dust = Dust.NewDust(new Vector2(Projectile.position.X - 50, Projectile.position.Y - 50), 170, 170, DustID.Lava, 0f, 0f, 0, Color.Red, 1.5f);
            Main.dust[dust].noGravity = true;

            int dust2 = Dust.NewDust(new Vector2(Projectile.position.X - 50, Projectile.position.Y - 50), 170, 170, DustID.FlameBurst, 0f, 0f, 0, Color.Red, 2f);
            Main.dust[dust2].noGravity = true;

            int newProj = ModContent.ProjectileType<LawrenceProjectileLight>();
            Vector2 newVelocity = Projectile.velocity * 0.4f;
            int lilProj = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, newVelocity, newProj, 80, 4, Projectile.owner);
            Main.projectile[lilProj].stepSpeed = 0.2f;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X - 30, Projectile.position.Y - 30), 100, 100, DustID.Lava, 0f, 0f, 0, Color.Yellow, 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.2f;

            int dust2 = Dust.NewDust(new Vector2(Projectile.position.X - 30, Projectile.position.Y - 30), 100, 100, DustID.FlameBurst, 0f, 0f, 0, Color.Yellow, 1.5f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.2f;
        }
    }
}