using ReLogic.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Inquisitors.Projectiles;
using System;
using IL.Terraria.GameContent;

namespace Inquisitors.Projectiles
{
    public class LawrenceShieldProjectile : ModProjectile
    {
        public float rotateRate;
        public bool rotateTop = true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Shield of Lawrence");
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.damage = 50;
            Projectile.width = 18;
            Projectile.height = 41;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.light = 1.5f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
            target.AddBuff(BuffID.CursedInferno, 120);
            target.AddBuff(BuffID.Ichor, 120);
            SoundEngine.PlaySound(SoundID.Item74);
        }

        public override void Kill(int timeLeft)
        {

        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;

            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }
         
            if (player.direction==1)
            {
                Projectile.Center=new Vector2(player.Center.X+20f, player.Center.Y);
                Projectile.rotation = 3.1f;
               //Projectile.rotation += MathHelper.ToRadians(10);    
            }
            else
            {
                Projectile.Center = new Vector2(player.Center.X - 20f, player.Center.Y);
                Projectile.rotation = 0f;
            }
        }
    }
}