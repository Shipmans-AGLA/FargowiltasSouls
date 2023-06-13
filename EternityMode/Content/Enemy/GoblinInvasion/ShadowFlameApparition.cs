﻿using FargowiltasSouls.EternityMode.NPCMatching;
using FargowiltasSouls.Content.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Buffs.Masomode;

namespace FargowiltasSouls.EternityMode.Content.Enemy.GoblinInvasion
{
    public class ShadowFlameApparition : EModeNPCBehaviour
    {
        public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(NPCID.ShadowFlameApparition);

        public override void AI(NPC npc)
        {
            base.AI(npc);

            EModeGlobalNPC.Aura(npc, 100, ModContent.BuffType<ShadowflameBuff>(), false, DustID.Shadowflame);
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            base.OnHitPlayer(npc, target, damage, crit);

            target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300);
        }
    }
}
