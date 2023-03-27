using ReLogic.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Inquisitors.Projectiles;

namespace Inquisitors.Projectiles
{
	public class LawrenceProjectileLight : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Flame fire");
		}

        public override void SetDefaults()
        {
    		Projectile.DamageType = DamageClass.Melee;
	    	Projectile.damage = 80;
    		Projectile.width = 40;
            Projectile.height = 40;
		    Projectile.aiStyle = 1;
            Projectile.friendly = true;
		    Projectile.hostile = false;
		    Projectile.penetrate = 1;
		    Projectile.timeLeft = 200;
		    Projectile.light = 0.5f;
		    Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.stepSpeed = 0.2f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            SoundEngine.PlaySound(SoundID.Item74);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }

        public override void Kill(int timeLeft)
        {
            
            SoundEngine.PlaySound(SoundID.Item20);
            int dust = Dust.NewDust(new Vector2(Projectile.position.X-50, Projectile.position.Y-50), 170, 170, DustID.Lava, 0f, 0f, 0, Color.Red, 2f);
            Main.dust[dust].noGravity = true;

            int dust2 = Dust.NewDust(new Vector2(Projectile.position.X - 50, Projectile.position.Y - 50), 170, 170, DustID.FlameBurst, 0f, 0f, 0, Color.Red, 3f);
            Main.dust[dust2].noGravity = true;
        }

        public override void AI()
        {
			int dust = Dust.NewDust(Projectile.position, Projectile.height, Projectile.width, DustID.Lava, 0f, 0f, 0, Color.Yellow,0.2f);
			Main.dust[dust].noGravity=true;
            Main.dust[dust].velocity *= 0.2f;

            int dust2 = Dust.NewDust(Projectile.position, Projectile.height, Projectile.width, DustID.FlameBurst, 0f, 0f, 0, Color.Yellow, 0.5f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.2f;
        }
    }
}