﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Content.Projectiles.Masomode
{
	public class CelestialRuneAncientVision : ModProjectile
	{
		public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/AncientVision";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ancient Vision");
			ProjectileID.Sets.MinionShot[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.aiStyle = 0;
			AIType = ProjectileID.Bullet;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = 10;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 240;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
			Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
		}

		public override void AI()
		{
			//dust!
			int dustId = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, DustID.GoldFlame, Projectile.velocity.X * 0.2f,
				Projectile.velocity.Y * 0.2f, 100, default, 2f);
			Main.dust[dustId].noGravity = true;
			int dustId3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width, Projectile.height + 5, DustID.GoldFlame, Projectile.velocity.X * 0.2f,
				Projectile.velocity.Y * 0.2f, 100, default, 2f);
			Main.dust[dustId3].noGravity = true;

			//direction meme
			Projectile.spriteDirection = -Projectile.direction;

			const int aislotHomingCooldown = 0;
			int homingDelay = 10;
			const float desiredFlySpeedInPixelsPerFrame = 50;
			const float amountOfFramesToLerpBy = 50; // minimum of 1, please keep in full numbers even though it's  float!

			Projectile.ai[aislotHomingCooldown]++;
			if (Projectile.ai[aislotHomingCooldown] > homingDelay)
			{
				Projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 

				NPC n = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(Projectile.Center, 1000));
				if (n.Alive())
				{
					Vector2 desiredVelocity = Projectile.SafeDirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
					Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
				}
			}
		}

		public override void OnKill(int timeleft)
		{
			for (int num468 = 0; num468 < 20; num468++)
			{
				int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.GoldFlame, -Projectile.velocity.X * 0.2f,
					-Projectile.velocity.Y * 0.2f, 100, default, 2f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 2f;
				num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.RedTorch, -Projectile.velocity.X * 0.2f,
					-Projectile.velocity.Y * 0.2f, 100);
				Main.dust[num469].velocity *= 2f;
			}
		}
	}
}

