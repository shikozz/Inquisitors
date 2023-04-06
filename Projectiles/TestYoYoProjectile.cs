using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Inquisitors.Projectiles
{
    public class TestYoYoProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Adhesive");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 8;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 260;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15;
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
        }

        public override void PostAI()
        {
            int newProj = ModContent.ProjectileType<LawrenceProjectileLight>();
            ModProjectile nmpl = ModContent.GetModProjectile(newProj);
            Vector2 newVelocity = Projectile.velocity.RotateRandom(MathHelper.ToRadians(90)) * 0.4f;
            int lilProj = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, newVelocity, newProj, 80, 4, Projectile.owner);
            Main.projectile[lilProj].stepSpeed = 0.2f;
        }
    }
}