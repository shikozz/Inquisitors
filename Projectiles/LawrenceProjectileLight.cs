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
        public int velocity;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Flame Fireball");
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

            for (int i = 0; i <= 10; i++)
            {
                int dust = Dust.NewDust(Projectile.position, 40, 40, DustID.Lava, 0f, 0f, 0, Color.Red, 1f);
                Main.dust[dust].noGravity = true;

                int dust2 = Dust.NewDust(Projectile.position, 40, 40, DustID.FlameBurst, 0f, 0f, 0, Color.Red, 1f);
                Main.dust[dust2].noGravity = true;
            }
        }

        public override void AI()
        {
			int dust = Dust.NewDust(Projectile.position, Projectile.height, Projectile.width, DustID.Lava, 0f, 0f, 0, Color.Yellow,0.5f);
			Main.dust[dust].noGravity=true;
            Main.dust[dust].velocity *= 0.2f;

            int dust2 = Dust.NewDust(Projectile.position, Projectile.height, Projectile.width, DustID.FlameBurst, 0f, 0f, 0, Color.Yellow, 1f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.2f;

            float maxDetectRadius = 400f; // The maximum radius at which a projectile can detect a target
            float projSpeed = 13f; // The speed at which the projectile moves towards the target

            // Trying to find NPC closest to the projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            //Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }
            return closestNPC;
        }

    }
}