﻿using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using RimWorld;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AbilityUser;

namespace TorannMagic
{
    public static class TM_Calc
    {
        public static bool IsRobotPawn(Pawn pawn)
        {
            bool flag_Core = pawn.RaceProps.IsMechanoid;
            bool flag_AndroidTiers = (pawn.def.defName == "Android1Tier" || pawn.def.defName == "Android2Tier" || pawn.def.defName == "Android3Tier" || pawn.def.defName == "Android4Tier" || pawn.def.defName == "Android5Tier" || pawn.def.defName == "M7Mech" || pawn.def.defName == "MicroScyther");
            bool flag_Androids = pawn.RaceProps.FleshType.defName == "ChJDroid" || pawn.def.defName == "ChjAndroid";
            bool isRobot = flag_Core || flag_AndroidTiers || flag_Androids;
            return isRobot;
        }

        public static bool IsUndead(Pawn pawn)
        {
            if (pawn != null)
            {
                bool flag_Hediff = false;
                if (pawn.health != null && pawn.health.hediffSet != null)
                {
                    if (pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_UndeadHD"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_UndeadAnimalHD"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_LichHD"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_UndeadStageHD"), false))
                    {
                        flag_Hediff = true;
                    }
                    Hediff hediff = null;
                    for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
                    {
                        hediff = pawn.health.hediffSet.hediffs[i];
                        if (hediff.def.defName.Contains("ROM_Vamp"))
                        {
                            flag_Hediff = true;
                        }
                    }
                }
                bool flag_DefName = false;
                if (pawn.def.defName == "SL_Runner" || pawn.def.defName == "SL_Peon" || pawn.def.defName == "SL_Archer" || pawn.def.defName == "SL_Hero")
                {
                    flag_DefName = true;
                }
                if(pawn.def == TorannMagicDefOf.TM_GiantSkeletonR || pawn.def == TorannMagicDefOf.TM_SkeletonR || pawn.def == TorannMagicDefOf.TM_SkeletonLichR)
                {
                    flag_DefName = true;
                }
                bool flag_Trait = false;
                if (pawn.story != null && pawn.story.traits != null)
                {
                    if (pawn.story.traits.HasTrait(TorannMagicDefOf.Undead))
                    {
                        flag_Trait = true;
                    }
                }
                bool isUndead = flag_Hediff || flag_DefName || flag_Trait;
                return isUndead;
            }
            return false;
        }

        public static bool IsElemental(Pawn pawn)
        {
            if (pawn != null)
            {
                bool flag_Def = false;
                if (pawn.def != null)
                {
                    if (pawn.def == TorannMagicDefOf.TM_LesserEarth_ElementalR || pawn.def == TorannMagicDefOf.TM_LesserFire_ElementalR || pawn.def == TorannMagicDefOf.TM_LesserWater_ElementalR || pawn.def == TorannMagicDefOf.TM_LesserWind_ElementalR ||
                        pawn.def == TorannMagicDefOf.TM_Earth_ElementalR || pawn.def == TorannMagicDefOf.TM_Fire_ElementalR || pawn.def == TorannMagicDefOf.TM_Water_ElementalR || pawn.def == TorannMagicDefOf.TM_Wind_ElementalR ||
                        pawn.def == TorannMagicDefOf.TM_GreaterEarth_ElementalR || pawn.def == TorannMagicDefOf.TM_GreaterFire_ElementalR || pawn.def == TorannMagicDefOf.TM_GreaterWater_ElementalR || pawn.def == TorannMagicDefOf.TM_GreaterWind_ElementalR)
                    {
                        flag_Def = true;
                    }
                }

                bool isElemental = flag_Def;
                return isElemental;
            }
            return false;
        }

        public static bool IsUndeadNotVamp(Pawn pawn)
        {
            if (pawn != null)
            {
                bool flag_Hediff = false;
                if (pawn.health != null)
                {
                    if (pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_UndeadHD"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_UndeadAnimalHD"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_LichHD"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_UndeadStageHD"), false))
                    {
                        flag_Hediff = true;
                    }
                }
                bool flag_DefName = false;
                if (pawn.def.defName == "SL_Runner" || pawn.def.defName == "SL_Peon" || pawn.def.defName == "SL_Archer" || pawn.def.defName == "SL_Hero")
                {
                    flag_DefName = true;
                }
                bool flag_Trait = false;
                if (pawn.story != null && pawn.story.traits != null)
                {
                    if (pawn.story.traits.HasTrait(TorannMagicDefOf.Undead))
                    {
                        flag_Trait = true;
                    }
                }
                bool isUndead = flag_Hediff || flag_DefName || flag_Trait;
                return isUndead;
            }
            return false;
        }

        public static bool HasHateHediff(Pawn pawn)
        {
            if(pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_HateHD_I"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_HateHD_II"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_HateHD_III"), false) ||
                pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_HateHD"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_HateHD_IV"), false) || pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_HateHD_V"), false))
            {
                return true;
            }
            return false;
        }

        public static bool IsMightUser(Pawn pawn)
        {
            if (pawn != null)
            {
                bool flag_Hediff = false;
                if (pawn.health != null)
                {
                    if (pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_MightUserHD"), false))
                    {
                        flag_Hediff = true;
                    }
                }
                bool flag_Need = false;
                if (pawn.needs != null)
                {
                    List<Need> needs = pawn.needs.AllNeeds;
                    for (int i = 0; i < needs.Count; i++)
                    {
                        if (needs[i].def.defName == "TM_Stamina")
                        {
                            flag_Need = true;
                        }
                    }
                }
                bool flag_Trait = false;
                if (pawn.story != null && pawn.story.traits != null)
                {
                    if (pawn.story.traits.HasTrait(TorannMagicDefOf.Bladedancer) || pawn.story.traits.HasTrait(TorannMagicDefOf.Gladiator) || pawn.story.traits.HasTrait(TorannMagicDefOf.Faceless) || 
                        pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Sniper) || pawn.story.traits.HasTrait(TorannMagicDefOf.Ranger) || pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Psionic) ||
                        pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Monk) || IsWayfarer(pawn))
                    { 
                        flag_Trait = true;
                    }
                }
                bool isMightUser = flag_Hediff || flag_Trait || flag_Need;
                return isMightUser;
            }
            return false;
        }

        public static TraitDef GetMightTrait(Pawn pawn)
        {
            if (pawn.story != null && pawn.story.traits != null)
            {
                for (int i = 0; i < pawn.story.traits.allTraits.Count; i++)
                {
                    TraitDef td = pawn.story.traits.allTraits[i].def;
                    if(td == TorannMagicDefOf.Bladedancer || td == TorannMagicDefOf.Gladiator || td == TorannMagicDefOf.Faceless || td == TorannMagicDefOf.TM_Sniper || td == TorannMagicDefOf.Ranger ||
                        td == TorannMagicDefOf.TM_Psionic || td == TorannMagicDefOf.TM_Monk || td == TorannMagicDefOf.TM_Wayfarer)
                    {
                        return td;
                    }
                }
            }
            return null;
        }

        public static bool IsMagicUser(Pawn pawn)
        {
            if (pawn != null)
            {
                bool flag_Hediff = false;
                if (pawn.health != null)
                {
                    if (pawn.health.hediffSet.HasHediff(HediffDef.Named("TM_MagicUserHD"), false))
                    {
                        flag_Hediff = true;
                    }
                }
                bool flag_Need = false;
                if (pawn.needs != null)
                {
                    List<Need> needs = pawn.needs.AllNeeds;
                    for (int i = 0; i < needs.Count; i++)
                    {
                        if (needs[i].def.defName == "TM_Mana")
                        {
                            flag_Need = true;
                        }
                    }
                }
                bool flag_Trait = false;
                if (pawn.story != null && pawn.story.traits != null)
                {
                    if (pawn.story.traits.HasTrait(TorannMagicDefOf.Technomancer) || pawn.story.traits.HasTrait(TorannMagicDefOf.Geomancer) || pawn.story.traits.HasTrait(TorannMagicDefOf.Warlock) || 
                        pawn.story.traits.HasTrait(TorannMagicDefOf.Succubus) || pawn.story.traits.HasTrait(TorannMagicDefOf.Faceless) || pawn.story.traits.HasTrait(TorannMagicDefOf.InnerFire) || 
                        pawn.story.traits.HasTrait(TorannMagicDefOf.HeartOfFrost) || pawn.story.traits.HasTrait(TorannMagicDefOf.StormBorn) || pawn.story.traits.HasTrait(TorannMagicDefOf.Arcanist) || 
                        pawn.story.traits.HasTrait(TorannMagicDefOf.Paladin) || pawn.story.traits.HasTrait(TorannMagicDefOf.Summoner) || pawn.story.traits.HasTrait(TorannMagicDefOf.Druid) || 
                        (pawn.story.traits.HasTrait(TorannMagicDefOf.Necromancer) || pawn.story.traits.HasTrait(TorannMagicDefOf.Lich)) || pawn.story.traits.HasTrait(TorannMagicDefOf.Priest) || 
                        pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Bard) || pawn.story.traits.HasTrait(TorannMagicDefOf.Chronomancer) || IsWanderer(pawn) ||
                        pawn.story.traits.HasTrait(TorannMagicDefOf.ChaosMage))
                    {
                        flag_Trait = true;
                    }
                    if(pawn.story.traits.HasTrait(TorannMagicDefOf.Faceless))
                    {
                        return false;
                    }
                }                
                bool isMagicUser = flag_Hediff || flag_Trait || flag_Need;
                return isMagicUser;
            }
            return false;
        }

        public static bool IsWanderer(Pawn pawn)
        {
            if(pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Wanderer))
            {
                return true;
            }
            else if(pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Wayfarer)) //pawn is a wayfarer with appropriate skill level
            {
                int lvl = pawn.GetComp<CompAbilityUserMight>().MightData.MightPowerSkill_FieldTraining.FirstOrDefault((MightPowerSkill x) => x.label == "TM_FieldTraining_eff").level;
                if (lvl >= 15)
                {
                    return true;
                }
            }            
            return false;
        }

        public static bool IsWayfarer(Pawn pawn)
        {
            if (pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Wayfarer))
            {
                return true;
            }
            else if (pawn.story.traits.HasTrait(TorannMagicDefOf.TM_Wanderer)) //pawn is a wanderer with appropriate skill level
            {
                int lvl = pawn.GetComp<CompAbilityUserMagic>().MagicData.MagicPowerSkill_Cantrips.FirstOrDefault((MagicPowerSkill x) => x.label == "TM_Cantrips_eff").level;
                if (lvl >= 15)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPawnInjured(Pawn targetPawn, float minInjurySeverity = 0)
        {
            float injurySeverity = 0;
            using (IEnumerator<BodyPartRecord> enumerator = targetPawn.health.hediffSet.GetInjuredParts().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    BodyPartRecord rec = enumerator.Current;
                    IEnumerable<Hediff_Injury> arg_BB_0 = targetPawn.health.hediffSet.GetHediffs<Hediff_Injury>();
                    Func<Hediff_Injury, bool> arg_BB_1;
                    arg_BB_1 = ((Hediff_Injury injury) => injury.Part == rec);

                    foreach (Hediff_Injury current in arg_BB_0.Where(arg_BB_1))
                    {
                        bool flag5 = current.CanHealNaturally() && !current.IsPermanent();
                        if (flag5)
                        {
                            injurySeverity += current.Severity;
                        }
                    }
                }
            }
            return injurySeverity > minInjurySeverity;
        }

        public static List<Hediff> GetPawnAfflictions(Pawn targetPawn)
        {
            List<Hediff> afflictionList = new List<Hediff>();
            afflictionList.Clear();
            using (IEnumerator<Hediff> enumerator = targetPawn.health.hediffSet.GetHediffs<Hediff>().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Hediff rec = enumerator.Current;
                    if (rec.def.isBad && rec.def.makesSickThought)
                    {
                        afflictionList.Add(rec);
                    }
                }
            }
            return afflictionList;
        }

        public static List<Hediff> GetPawnAddictions(Pawn targetPawn)
        {
            List<Hediff> addictionList = new List<Hediff>();
            addictionList.Clear();
            using (IEnumerator<Hediff_Addiction> enumerator = targetPawn.health.hediffSet.GetHediffs<Hediff_Addiction>().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Hediff_Addiction rec = enumerator.Current;
                    if (rec.Chemical.addictionHediff != null)
                    {
                        addictionList.Add(rec);
                    }
                }
            }
            return addictionList;
        }

        public static Vector3 GetVector(IntVec3 from, IntVec3 to)
        {
            Vector3 heading = (to - from).ToVector3();
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            return direction;
        }

        public static Vector3 GetVector(Vector3 from, Vector3 to)
        {
            Vector3 heading = (to - from);
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            return direction;
        }

        public static Pawn FindNearbyOtherPawn(Pawn pawn, int radius)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed)
                {
                    if (targetPawn != pawn && !targetPawn.HostileTo(pawn.Faction) && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        pawnList.Add(targetPawn);
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyPawn(Pawn pawn, int radius)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed)
                {
                    if (!targetPawn.HostileTo(pawn.Faction) && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        pawnList.Add(targetPawn);
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyFactionPawn(Pawn pawn, Faction faction, int radius)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed)
                {
                    if (targetPawn.Faction == faction && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        pawnList.Add(targetPawn);
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyMage(Pawn pawn, int radius, bool inCombat)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed && targetPawn.Faction != null)
                {
                    if(inCombat)
                    {
                        if (pawn != targetPawn && targetPawn.HostileTo(pawn.Faction) && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                        {
                            CompAbilityUserMagic targetComp = targetPawn.GetComp<CompAbilityUserMagic>();
                            if (targetComp != null && targetComp.IsMagicUser && !targetComp.Pawn.story.traits.HasTrait(TorannMagicDefOf.Faceless))
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                    }                    
                    else
                    {
                        if (pawn != targetPawn && !targetPawn.HostileTo(pawn.Faction) && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                        {
                            CompAbilityUserMagic targetComp = targetPawn.GetComp<CompAbilityUserMagic>();
                            if (targetComp != null && targetComp.IsMagicUser && !targetComp.Pawn.story.traits.HasTrait(TorannMagicDefOf.Faceless))
                            {
                                pawnList.Add(targetPawn);                                
                            }
                        }
                    }                    
                }
                targetPawn = null;
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static List<Pawn> FindNearbyMages(IntVec3 center, Map map, Faction faction, int radius, bool friendly)
        {
            List<Pawn> mapPawns = map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed && targetPawn.Faction != null)
                {
                    if (targetPawn.Drafted)
                    {
                        continue;
                    }
                    if(friendly && targetPawn.Faction != faction)
                    {
                        continue;
                    }
                    else if(!friendly && targetPawn.Faction == faction)
                    {
                        continue;
                    }
                    if((targetPawn.Position - center).LengthHorizontal > radius)
                    {
                        continue;
                    }
                    if(!TM_Calc.IsMagicUser(targetPawn))
                    {
                        continue;
                    }
                    pawnList.Add(targetPawn);
                }
            }
            return pawnList;
        }

        public static Pawn FindNearbyFighter(Pawn pawn, int radius, bool inCombat)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (pawn != targetPawn && targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed && targetPawn.Faction != null)
                {
                    if (inCombat)
                    {
                        if (targetPawn.HostileTo(pawn.Faction) && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                        {
                            CompAbilityUserMight targetComp = targetPawn.GetComp<CompAbilityUserMight>();
                            if (targetComp != null && targetComp.IsMightUser)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                    }
                    else
                    {
                        if (pawn != targetPawn && !targetPawn.HostileTo(pawn.Faction) && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                        {
                            CompAbilityUserMight targetComp = targetPawn.GetComp<CompAbilityUserMight>();
                            if (targetComp != null && targetComp.IsMightUser)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                    }
                }
                targetPawn = null;
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyInjuredPawn(Pawn pawn, int radius, float minSeverity)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !TM_Calc.IsUndead(targetPawn))
                {
                    if (targetPawn.IsColonist && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        float injurySeverity = 0;
                        using (IEnumerator<BodyPartRecord> enumerator = targetPawn.health.hediffSet.GetInjuredParts().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BodyPartRecord rec = enumerator.Current;
                                IEnumerable<Hediff_Injury> arg_BB_0 = targetPawn.health.hediffSet.GetHediffs<Hediff_Injury>();
                                Func<Hediff_Injury, bool> arg_BB_1;
                                arg_BB_1 = ((Hediff_Injury injury) => injury.Part == rec);

                                foreach (Hediff_Injury current in arg_BB_0.Where(arg_BB_1))
                                {
                                    bool flag5 = current.CanHealNaturally() && !current.IsPermanent();
                                    if (flag5)
                                    {
                                        injurySeverity += current.Severity;
                                    }                                        
                                }                                
                            }
                        }
                        if (minSeverity != 0)
                        {
                            if (injurySeverity >= minSeverity)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                        else
                        {
                            if (injurySeverity != 0)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyInjuredPawnOther(Pawn pawn, int radius, float minSeverity)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !TM_Calc.IsUndead(targetPawn))
                {
                    if (targetPawn.IsColonist && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius && targetPawn != pawn)
                    {
                        float injurySeverity = 0;
                        using (IEnumerator<BodyPartRecord> enumerator = targetPawn.health.hediffSet.GetInjuredParts().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BodyPartRecord rec = enumerator.Current;
                                IEnumerable<Hediff_Injury> arg_BB_0 = targetPawn.health.hediffSet.GetHediffs<Hediff_Injury>();
                                Func<Hediff_Injury, bool> arg_BB_1;
                                arg_BB_1 = ((Hediff_Injury injury) => injury.Part == rec);

                                foreach (Hediff_Injury current in arg_BB_0.Where(arg_BB_1))
                                {
                                    bool flag5 = current.CanHealNaturally() && !current.IsPermanent();
                                    if (flag5)
                                    {
                                        injurySeverity += current.Severity;
                                    }
                                }
                            }
                        }
                        if (minSeverity != 0)
                        {
                            if (injurySeverity >= minSeverity)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                        else
                        {
                            if (injurySeverity != 0)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyPermanentlyInjuredPawn(Pawn pawn, int radius, float minSeverity)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !TM_Calc.IsUndead(targetPawn))
                {
                    if (targetPawn.IsColonist && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        float injurySeverity = 0;
                        using (IEnumerator<BodyPartRecord> enumerator = targetPawn.health.hediffSet.GetInjuredParts().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BodyPartRecord rec = enumerator.Current;
                                IEnumerable<Hediff_Injury> arg_BB_0 = targetPawn.health.hediffSet.GetHediffs<Hediff_Injury>();
                                Func<Hediff_Injury, bool> arg_BB_1;
                                arg_BB_1 = ((Hediff_Injury injury) => injury.Part == rec);

                                foreach (Hediff_Injury current in arg_BB_0.Where(arg_BB_1))
                                {
                                    bool flag5 = !current.CanHealNaturally() && current.IsPermanent();
                                    if (flag5)
                                    {
                                        injurySeverity += current.Severity;
                                    }
                                }
                            }
                        }
                        if (minSeverity != 0)
                        {
                            if (injurySeverity >= minSeverity)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                        else
                        {
                            if (injurySeverity != 0)
                            {
                                pawnList.Add(targetPawn);
                            }
                        }
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyAfflictedPawn(Pawn pawn, int radius, List<string> validAfflictionDefnames)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed)
                {
                    if (targetPawn.IsColonist && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        using (IEnumerator<Hediff> enumerator = targetPawn.health.hediffSet.GetHediffs<Hediff>().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Hediff rec = enumerator.Current;
                                for(int j =0; j < validAfflictionDefnames.Count; j++)
                                {
                                    if (rec.def.defName.Contains(validAfflictionDefnames[j]) && rec.def.isBad)
                                    {
                                        pawnList.Add(targetPawn);
                                    }
                                }                                    
                                
                            }
                        }
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyAfflictedPawnAny(Pawn pawn, int radius)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed)
                {
                    if (targetPawn.IsColonist && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        using (IEnumerator<Hediff> enumerator = targetPawn.health.hediffSet.GetHediffs<Hediff>().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Hediff rec = enumerator.Current;
                                if (rec.def.PossibleToDevelopImmunityNaturally() && rec.def.isBad)
                                {
                                    pawnList.Add(targetPawn);
                                }                               
                            }
                        }
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                //Log.Message("returning pawn list containing " + pawnList.RandomElement().LabelShort);
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyAddictedPawn(Pawn pawn, int radius, List<string> validAddictionDefnames)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed)
                {
                    if (targetPawn.IsColonist && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        using (IEnumerator<Hediff_Addiction> enumerator = targetPawn.health.hediffSet.GetHediffs<Hediff_Addiction>().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Hediff_Addiction rec = enumerator.Current;
                                for (int j = 0; j < validAddictionDefnames.Count; j++)
                                {
                                    if (rec.Chemical.defName.Contains(validAddictionDefnames[j]))
                                    {
                                        pawnList.Add(targetPawn);
                                    }
                                }

                            }
                        }
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static Pawn FindNearbyEnemy(Pawn pawn, int radius)
        {
            return FindNearbyEnemy(pawn.Position, pawn.Map, pawn.Faction, radius, 0);
        }

        public static Pawn FindNearbyEnemy(IntVec3 position, Map map, Faction faction, float radius, float minRange)
        {
            List<Pawn> mapPawns = map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed)
                {
                    if (targetPawn.Position != position && targetPawn.HostileTo(faction) && (position - targetPawn.Position).LengthHorizontal <= radius && (position - targetPawn.Position).LengthHorizontal > minRange)
                    {
                        pawnList.Add(targetPawn);
                        targetPawn = null;
                    }
                    else
                    {
                        targetPawn = null;
                    }
                }                
            }
            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static List<Pawn> FindPawnsNearTarget(Pawn pawn, int radius, IntVec3 targetCell, bool hostile)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed && !targetPawn.Downed)
                {
                    if (targetPawn != pawn && (targetCell - targetPawn.Position).LengthHorizontal <= radius)
                    {
                        if (hostile && targetPawn.HostileTo(pawn.Faction))
                        {
                            pawnList.Add(targetPawn);
                        }
                        else if(!hostile && !targetPawn.HostileTo(pawn.Faction))
                        {
                            pawnList.Add(targetPawn);
                        }
                    }
                    targetPawn = null;                    
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList;
            }
            else
            {
                return null;
            }
        }

        public static bool HasLoSFromTo(IntVec3 root, LocalTargetInfo targ, Thing caster, float minRange, float maxRange)
        {
            float range = (targ.Cell - root).LengthHorizontal;
            if (targ.HasThing && targ.Thing.Map != caster.Map)
            {
                return false;
            }
            if (range <= minRange || range >= maxRange)
            {
                return false;
            }
            CellRect cellRect = (!targ.HasThing) ? CellRect.SingleCell(targ.Cell) : targ.Thing.OccupiedRect();
            if (caster is Pawn)
            {
                if (GenSight.LineOfSight(caster.Position, targ.Cell, caster.Map, skipFirstCell: true))
                {
                    return true;
                }
                List<IntVec3> tempLeanShootSources = new List<IntVec3>();
                ShootLeanUtility.LeanShootingSourcesFromTo(root, cellRect.ClosestCellTo(root), caster.Map, tempLeanShootSources);
                for (int i = 0; i < tempLeanShootSources.Count; i++)
                {
                    IntVec3 intVec = tempLeanShootSources[i];
                    if (GenSight.LineOfSight(intVec, targ.Cell, caster.Map, skipFirstCell: true))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if(GenSight.LineOfSight(root, targ.Cell, caster.Map, skipFirstCell: true))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<Thing> FindNearbyDamagedBuilding(Pawn pawn, int radius)
        {
            List<Thing> mapBuildings = pawn.Map.listerBuildingsRepairable.RepairableBuildings(pawn.Faction);
            List<Thing> buildingList = new List<Thing>();
            Building building= null;
            buildingList.Clear();
            for (int i = 0; i < mapBuildings.Count; i++)
            {
                building = mapBuildings[i] as Building;
                if (building != null && (building.Position - pawn.Position).LengthHorizontal <= radius && building.def.useHitPoints && building.HitPoints != building.MaxHitPoints)
                {
                    if (pawn.Drafted && building.def.designationCategory == DesignationCategoryDefOf.Security || building.def.building.ai_combatDangerous)
                    {
                        buildingList.Add(building);
                    }
                    else if(!pawn.Drafted)
                    {
                        buildingList.Add(building);
                    }
                }
                building = null;                
            }

            if (buildingList.Count > 0)
            {
                return buildingList;
            }
            else
            {
                return null;
            }
        }

        public static Thing FindNearbyDamagedThing(Pawn pawn, int radius)
        {
            List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
            List<Thing> pawnList = new List<Thing>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && targetPawn.Spawned && !targetPawn.Dead && !targetPawn.Destroyed)
                {
                    //Log.Message("evaluating targetpawn " + targetPawn.LabelShort);
                    //Log.Message("pawn faction is " + targetPawn.Faction);
                    //Log.Message("pawn position is " + targetPawn.Position);
                    //Log.Message("pawn is robot: " + TM_Calc.IsRobotPawn(targetPawn));
                    if (targetPawn.Faction != null && targetPawn.Faction == pawn.Faction && (pawn.Position - targetPawn.Position).LengthHorizontal <= radius && TM_Calc.IsRobotPawn(targetPawn))
                    {
                        float injurySeverity = 0;
                        using (IEnumerator<BodyPartRecord> enumerator = targetPawn.health.hediffSet.GetInjuredParts().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                BodyPartRecord rec = enumerator.Current;
                                IEnumerable<Hediff_Injury> arg_BB_0 = targetPawn.health.hediffSet.GetHediffs<Hediff_Injury>();
                                Func<Hediff_Injury, bool> arg_BB_1;
                                arg_BB_1 = ((Hediff_Injury injury) => injury.Part == rec);

                                foreach (Hediff_Injury current in arg_BB_0.Where(arg_BB_1))
                                {
                                    bool flag5 = !current.IsPermanent();
                                    if (flag5)
                                    {
                                        injurySeverity += current.Severity;
                                    }
                                }
                            }
                        }
                        
                        if (injurySeverity != 0)
                        {
                            pawnList.Add(targetPawn as Thing);
                        }
                    }
                    targetPawn = null;                    
                }
            }

            List<Thing> buildingList = TM_Calc.FindNearbyDamagedBuilding(pawn, radius);
            if (buildingList != null)
            {
                for (int i = 0; i < buildingList.Count; i++)
                {
                    pawnList.Add(buildingList[i]);
                }
            }

            if (pawnList.Count > 0)
            {
                return pawnList.RandomElement();
            }
            else
            {
                return null;
            }
        }

        public static List<Pawn> FindAllPawnsAround(Map map, IntVec3 center, float radius, Faction faction = null, bool sameFaction = false)
        {
            List<Pawn> mapPawns = map.mapPawns.AllPawnsSpawned;
            List<Pawn> pawnList = new List<Pawn>();
            Pawn targetPawn = null;
            pawnList.Clear();
            for (int i = 0; i < mapPawns.Count; i++)
            {
                targetPawn = mapPawns[i];
                if (targetPawn != null && !targetPawn.Dead && !targetPawn.Destroyed)
                {
                    if (faction != null && !sameFaction)
                    {
                        if ((targetPawn.Position - center).LengthHorizontal <= radius)
                        {
                            if (targetPawn.Faction != null)
                            {
                                if (targetPawn.Faction != faction)
                                {
                                    pawnList.Add(targetPawn);
                                    targetPawn = null;
                                }
                                else
                                {
                                    targetPawn = null;
                                }
                            }
                            else
                            {
                                pawnList.Add(targetPawn);
                                targetPawn = null;
                            }
                        }
                        else
                        {
                            targetPawn = null;
                        }
                    }
                    else if(faction != null && sameFaction)
                    {
                        if (targetPawn.Faction != null && targetPawn.Faction == faction && (targetPawn.Position - center).LengthHorizontal <= radius)
                        {
                            pawnList.Add(targetPawn);
                            targetPawn = null;
                        }
                        else
                        {
                            targetPawn = null;
                        }
                    }
                    else
                    {
                        if((targetPawn.Position - center).LengthHorizontal <= radius)
                        {
                            pawnList.Add(targetPawn);
                            targetPawn = null;
                        }
                        else
                        {
                            targetPawn = null;
                        }
                    }
                }
            }
            if (pawnList.Count > 0)
            {
                return pawnList;
            }
            else
            {
                return null;
            }
        }

        public static IntVec3 TryFindSafeCell(Pawn pawn, IntVec3 currentPos, int radius, int maxThreats, int attempts = 1)
        {
            //Log.Message("attempting to find safe cell");
            for (int i = 0; i < attempts; i++)
            {
                IntVec3 tmp = currentPos;
                tmp.x += (Rand.Range(-radius, radius));
                tmp.z += Rand.Range(-radius, radius);
                if (tmp.InBounds(pawn.Map) && tmp.IsValid && tmp.Walkable(pawn.Map) && tmp.DistanceToEdge(pawn.Map) > 8)
                {
                    List<Pawn> threatCount = TM_Calc.FindPawnsNearTarget(pawn, 4, tmp, true);
                    if (threatCount != null)
                    {
                        if (threatCount.Count <= maxThreats)
                        {
                            //Log.Message("returning safe cell  (pawns)" + tmp);
                            return tmp;
                        }
                    }
                    else
                    {
                        //Log.Message("returning safe cell  " + tmp);
                        return tmp;
                    }
                }
            }
            return default(IntVec3);
        }

        public static float GetArcaneResistance(Pawn pawn, bool includePsychicSensitivity)
        {
            float resistance = 0;
            CompAbilityUserMagic compMagic = pawn.GetComp<CompAbilityUserMagic>();
            if(compMagic != null)
            {
                resistance += (compMagic.arcaneRes - 1);
            }

            CompAbilityUserMight compMight = pawn.GetComp<CompAbilityUserMight>();
            if(compMight != null)
            {
                resistance += (compMight.arcaneRes - 1);
            }

            if (includePsychicSensitivity && resistance == 0f)
            {
                resistance += (1 - pawn.GetStatValue(StatDefOf.PsychicSensitivity, false))/2;
            }

            if (pawn.health != null && pawn.health.capacities != null)
            {
                resistance += (pawn.health.capacities.GetLevel(PawnCapacityDefOf.Consciousness) - 1);
            }

            return resistance;
        }

        public static float GetSpellPenetration(Pawn pawn)
        {
            float penetration = 0;
            CompAbilityUserMagic compMagic = pawn.GetComp<CompAbilityUserMagic>();
            if (compMagic != null)
            {
                penetration += (compMagic.arcaneDmg - 1);
            }

            CompAbilityUserMight compMight = pawn.GetComp<CompAbilityUserMight>();
            if (compMight != null)
            {
                penetration += (compMight.mightPwr - 1);
            }

            if (pawn.health != null && pawn.health.capacities != null)
            {
                penetration += (pawn.health.capacities.GetLevel(PawnCapacityDefOf.Consciousness) - 1);
            }

            return penetration;
        }

        public static float GetSpellSuccessChance(Pawn caster, Pawn victim, bool usePsychicSensitivity = true)
        {
            float successChance;
            float penetration = TM_Calc.GetSpellPenetration(caster);
            float resistance = TM_Calc.GetArcaneResistance(victim, usePsychicSensitivity);
            successChance = 1f + penetration - resistance;
            return successChance;
        }

        public static List<ThingDef> GetAllRaceBloodTypes()
        {
            List<ThingDef> bloodTypes = new List<ThingDef>();
            bloodTypes.Clear();

            IEnumerable<ThingDef> enumerable = from def in DefDatabase<ThingDef>.AllDefs
                                                  where (def.race != null && def.race.BloodDef != null)
                                                  select def;

            foreach (ThingDef current in enumerable)
            {
                bloodTypes.AddDistinct(current.race.BloodDef);                
            }

            //for(int i =0; i< bloodTypes.Count; i++)
            //{
            //    Log.Message("blood type includes " + bloodTypes[i].defName);
            //}
            return bloodTypes;
        }

        public static HediffDef GetBloodLossTypeDef(List<Hediff> hediffList)
        {
            List<TM_CustomDef> bltd = DefDatabase<TM_CustomDef>.AllDefsListForReading;
            List<string> bltdHediffs = TM_CustomDef.Named("TM_CustomDef").BloodLossHediffs;
            for (int i = 0; i < bltdHediffs.Count; i++)
            {
                for (int j = 0; j < hediffList.Count; j++)
                {
                    if (bltdHediffs[i].ToString() == hediffList[j].def.defName.ToString())
                    {
                        return hediffList[j].def;
                    }
                }                
            }
            return null;
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (System.Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        //Rand.Chance(((settingsRef.baseFighterChance * 6) + (settingsRef.baseMageChance * 6) + (8 * settingsRef.advFighterChance) + (16 * settingsRef.advMageChance)) / (allTraits.Count))
        public static float GetRWoMTraitChance()
        {
            ModOptions.SettingsRef settingsRef = new ModOptions.SettingsRef();
            List<TraitDef> allTraits = DefDatabase<TraitDef>.AllDefsListForReading;
            float chance = ((settingsRef.baseFighterChance * 6) + (settingsRef.baseMageChance * 6) + (9 * settingsRef.advFighterChance) + (18 * settingsRef.advMageChance)) / (allTraits.Count);
            return Mathf.Clamp01(chance);
        }
        
        public static float GetMagePrecurserChance()
        {
            float chance = 0f;
            ModOptions.SettingsRef settingsRef = new ModOptions.SettingsRef();
            chance = (settingsRef.baseMageChance * 6) / ((settingsRef.baseFighterChance * 6) + (settingsRef.baseMageChance * 6) + (9 * settingsRef.advFighterChance) + (18 * settingsRef.advMageChance));
            chance *= GetRWoMTraitChance();
            return chance;
        }

        public static float GetFighterPrecurserChance()
        {
            float chance = 0f;
            ModOptions.SettingsRef settingsRef = new ModOptions.SettingsRef();
            chance = (settingsRef.baseFighterChance * 6) / ((settingsRef.baseFighterChance * 6) + (settingsRef.baseMageChance * 6) + (9 * settingsRef.advFighterChance) + (18 * settingsRef.advMageChance));
            chance *= GetRWoMTraitChance();
            return chance;
        }

        public static float GetMageSpawnChance()
        {
            float chance = 0f;
            ModOptions.SettingsRef settingsRef = new ModOptions.SettingsRef();
            chance = (settingsRef.advMageChance * 16) / ((settingsRef.baseFighterChance * 6) + (settingsRef.baseMageChance * 6) + (9 * settingsRef.advFighterChance) + (18 * settingsRef.advMageChance));
            chance *= GetRWoMTraitChance();
            return chance;
        }

        public static float GetFighterSpawnChance()
        {
            float chance = 0f;
            ModOptions.SettingsRef settingsRef = new ModOptions.SettingsRef();
            chance = (settingsRef.advFighterChance * 8) / ((settingsRef.baseFighterChance * 6) + (settingsRef.baseMageChance * 6) + (9 * settingsRef.advFighterChance) + (18 * settingsRef.advMageChance));
            chance *= GetRWoMTraitChance();
            return chance;
        }

        public static Area GetSpriteArea()
        {
            Area spriteArea = null;
            List<Area> allAreas = Find.CurrentMap.areaManager.AllAreas;
            if (allAreas != null && allAreas.Count > 0)
            {
                for (int i = 0; i < allAreas.Count; i++)
                {
                    if(allAreas[i].Label == "earth sprites")
                    {
                        spriteArea = allAreas[i];
                    }
                }
            }
            if(spriteArea == null)
            {
                Area_Allowed newArea = null;
                if(Find.CurrentMap.areaManager.TryMakeNewAllowed(out newArea))
                {
                    newArea.SetLabel("earth sprites");                    
                }
            }
            return spriteArea;
        }

        public static List<Apparel> GetNecroticOrbs(Pawn pawn)
        {
            List<Apparel> orbs = new List<Apparel>();
            orbs.Clear();
            if (pawn.Map != null)
            {
                List<Pawn> mapPawns = pawn.Map.mapPawns.AllPawnsSpawned;
                for (int i = 0; i < mapPawns.Count; i++)
                {
                    if (mapPawns[i].RaceProps.Humanlike && mapPawns[i].apparel != null && mapPawns[i].Faction == pawn.Faction && mapPawns[i].apparel.WornApparelCount > 0)
                    {
                        List<Apparel> apparelList = mapPawns[i].apparel.WornApparel;
                        for (int j = 0; j < apparelList.Count; j++)
                        {
                            if (apparelList[j].def == TorannMagicDefOf.TM_Artifact_NecroticOrb)
                            {
                                orbs.Add(apparelList[j]);
                            }
                        }
                    }
                }
            }
            else if (pawn.ParentHolder.ToString().Contains("Caravan"))
            {
                foreach (Pawn current in pawn.holdingOwner)
                {
                    if (current != null)
                    {
                        if (current.RaceProps.Humanlike && current.Faction == pawn.Faction && current.apparel != null && current.apparel.WornApparelCount > 0)
                        {
                            List<Apparel> apparelList = current.apparel.WornApparel;
                            for (int j = 0; j < apparelList.Count; j++)
                            {
                                if (apparelList[j].def == TorannMagicDefOf.TM_Artifact_NecroticOrb)
                                {
                                    orbs.Add(apparelList[j]);
                                }
                            }
                        }
                    }
                }
            }
            return orbs;
        }

        public static LocalTargetInfo FindWalkableCellNextTo(IntVec3 cell, Map map)
        {
            List<IntVec3> cellList = GenAdjFast.AdjacentCells8Way(cell);
            for(int i = 0; i < cellList.Count; i++)
            {
                if(cellList[i] != default(IntVec3) && cellList[i].InBounds(map) && cellList[i].Walkable(map) && !cellList[i].Fogged(map))
                {
                    cell = cellList[i];
                    break;
                }
            }
            return cell;
        }

        public static TMAbilityDef GetCopiedMightAbility(Pawn targetPawn, Pawn caster)
        {
            CompAbilityUserMight mightPawn = targetPawn.GetComp<CompAbilityUserMight>();
            TMAbilityDef tempAbility = null;
            if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Gladiator) && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
            {
                int rnd = Rand.RangeInclusive(0, 1);
                if (rnd == 0)
                {
                    int level = mightPawn.MightData.MightPowersG[2].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_Grapple;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_Grapple_I;
                            break;
                        case 2:
                            tempAbility = TorannMagicDefOf.TM_Grapple_II;
                            break;
                        case 3:
                            tempAbility = TorannMagicDefOf.TM_Grapple_III;
                            break;
                    }
                }
                else
                {
                    tempAbility = TorannMagicDefOf.TM_Whirlwind;
                }
            }
            else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.TM_Sniper) && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
            {
                int rnd = Rand.RangeInclusive(0, 2);
                if (rnd == 0)
                {
                    tempAbility = TorannMagicDefOf.TM_AntiArmor;
                }
                else if (rnd == 1)
                {
                    int level = mightPawn.MightData.MightPowersS[2].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_DisablingShot;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_DisablingShot_I;
                            break;
                        case 2:
                            tempAbility = TorannMagicDefOf.TM_DisablingShot_II;
                            break;
                        case 3:
                            tempAbility = TorannMagicDefOf.TM_DisablingShot_III;
                            break;
                    }
                }
                else
                {
                    tempAbility = TorannMagicDefOf.TM_Headshot;
                }

            }
            else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Bladedancer) && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
            {
                int rnd = Rand.RangeInclusive(0, 2);
                if (rnd == 0)
                {
                    tempAbility = TorannMagicDefOf.TM_SeismicSlash;
                }
                else if (rnd == 1)
                {
                    int level = mightPawn.MightData.MightPowersB[4].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_PhaseStrike;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_PhaseStrike_I;
                            break;
                        case 2:
                            tempAbility = TorannMagicDefOf.TM_PhaseStrike_II;
                            break;
                        case 3:
                            tempAbility = TorannMagicDefOf.TM_PhaseStrike_III;
                            break;
                    }
                }
                else
                {
                    tempAbility = TorannMagicDefOf.TM_BladeSpin;
                }
            }
            else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Ranger) && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
            {
                int rnd = Rand.RangeInclusive(0, 1);
                if (rnd == 0)
                {
                    int level = mightPawn.MightData.MightPowersB[4].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_ArrowStorm;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_ArrowStorm_I;
                            break;
                        case 2:
                            tempAbility = TorannMagicDefOf.TM_ArrowStorm_II;
                            break;
                        case 3:
                            tempAbility = TorannMagicDefOf.TM_ArrowStorm_III;
                            break;
                    }
                }
                else
                {
                    tempAbility = TorannMagicDefOf.TM_PoisonTrap;
                }
            }
            else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.TM_Psionic))
            {
                int rnd = Rand.RangeInclusive(0, 3);
                if ((rnd == 0 || rnd == 3) && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                {
                    int level = mightPawn.MightData.MightPowersP[1].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_PsionicBlast;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_PsionicBlast_I;
                            break;
                        case 2:
                            tempAbility = TorannMagicDefOf.TM_PsionicBlast_II;
                            break;
                        case 3:
                            tempAbility = TorannMagicDefOf.TM_PsionicBlast_III;
                            break;
                    }
                }
                else if (rnd == 1 && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                {
                    tempAbility = TorannMagicDefOf.TM_PsionicDash;
                }
                else
                {
                    int level = mightPawn.MightData.MightPowersP[3].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_PsionicBarrier;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_PsionicBarrier_Projected;
                            break;
                    }
                }
            }
            else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.DeathKnight))
            {
                int rnd = Rand.RangeInclusive(1, 2);
                if (rnd == 1 || caster.story.WorkTagIsDisabled(WorkTags.Violent))
                {
                    tempAbility = TorannMagicDefOf.TM_WaveOfFear;
                }
                else
                {
                    int level = mightPawn.MightData.MightPowersDK[4].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_GraveBlade;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_GraveBlade_I;
                            break;
                        case 2:
                            tempAbility = TorannMagicDefOf.TM_GraveBlade_II;
                            break;
                        case 3:
                            tempAbility = TorannMagicDefOf.TM_GraveBlade_III;
                            break;
                    }
                }
            }
            else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.TM_Monk))
            {
                int rnd = Rand.RangeInclusive(3, 5);
                if (rnd == 3)
                {
                    tempAbility = TorannMagicDefOf.TM_TigerStrike;
                }
                else if (rnd == 4)
                {
                    tempAbility = TorannMagicDefOf.TM_DragonStrike;
                }
                else
                {
                    tempAbility = TorannMagicDefOf.TM_ThunderStrike;
                }
            }
            else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Faceless))
            {

            }
            return tempAbility;
        }

        public static TMAbilityDef GetCopiedMagicAbility(Pawn targetPawn, Pawn caster)
        {
            CompAbilityUserMagic magicPawn = targetPawn.GetComp<CompAbilityUserMagic>();
            TMAbilityDef tempAbility = null;
            if (magicPawn != null)
            {
                if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Arcanist))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 3);
                        if (rnd == 0 && magicPawn.MagicData.MagicPowersA[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersA[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Shadow;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Shadow_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Shadow_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Shadow_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 1 && magicPawn.MagicData.MagicPowersA[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            int level = magicPawn.MagicData.MagicPowersA[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_MagicMissile;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_MagicMissile_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_MagicMissile_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_MagicMissile_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersA[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersA[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Blink;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Blink_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Blink_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Blink_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersA[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersA[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Summon;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Summon_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Summon_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Summon_III;
                                    break;
                            }
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.StormBorn))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 3);
                        if (rnd == 0 && magicPawn.MagicData.MagicPowersSB[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersSB[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_AMP;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_AMP_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_AMP_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_AMP_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 1 && magicPawn.MagicData.MagicPowersSB[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_LightningBolt;
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersSB[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_LightningCloud;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersSB[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_LightningStorm;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.InnerFire))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 3);
                        if (rnd == 0 && magicPawn.MagicData.MagicPowersIF[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersIF[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_RayofHope;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_RayofHope_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_RayofHope_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_RayofHope_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 1 && magicPawn.MagicData.MagicPowersIF[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Firebolt;
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersIF[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Fireclaw;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersIF[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Fireball;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.HeartOfFrost))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 4);
                        if (rnd == 0 && magicPawn.MagicData.MagicPowersHoF[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersHoF[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Soothe;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Soothe_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Soothe_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Soothe_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 1 && magicPawn.MagicData.MagicPowersHoF[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_Rainmaker;
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersHoF[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Icebolt;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersHoF[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            int level = magicPawn.MagicData.MagicPowersHoF[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_FrostRay;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_FrostRay_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_FrostRay_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_FrostRay_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 4 && magicPawn.MagicData.MagicPowersHoF[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Snowball;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Druid))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 3);
                        if (rnd == 0 && magicPawn.MagicData.MagicPowersD[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Poison;
                            i = 5;
                        }
                        else if (rnd == 1 && magicPawn.MagicData.MagicPowersD[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersD[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_SootheAnimal;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_SootheAnimal_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_SootheAnimal_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_SootheAnimal_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersD[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_Regenerate;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersD[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_CureDisease;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Necromancer))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(1, 3);
                        if (rnd == 1 && magicPawn.MagicData.MagicPowersN[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            int level = magicPawn.MagicData.MagicPowersN[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_DeathMark;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_DeathMark_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_DeathMark_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_DeathMark_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersN[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_FogOfTorment;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersN[rnd + 1].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            int level = magicPawn.MagicData.MagicPowersN[rnd + 1].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_CorpseExplosion;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_CorpseExplosion_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_CorpseExplosion_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_CorpseExplosion_III;
                                    break;
                            }
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Paladin))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 3);
                        if (rnd == 1 && magicPawn.MagicData.MagicPowersP[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersP[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Shield;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Shield_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Shield_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Shield_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 0 && magicPawn.MagicData.MagicPowersP[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_Heal;
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersP[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_ValiantCharge;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersP[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Overwhelm;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Priest))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 3);
                        if (rnd == 3 && magicPawn.MagicData.MagicPowersPR[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersPR[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_BestowMight;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_BestowMight_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_BestowMight_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_BestowMight_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersPR[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersPR[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_HealingCircle;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_HealingCircle_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_HealingCircle_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_HealingCircle_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 1 && magicPawn.MagicData.MagicPowersPR[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_Purify;
                            i = 5;
                        }
                        else if (rnd == 0 && magicPawn.MagicData.MagicPowersPR[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_AdvancedHeal;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Summoner))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(1, 3);
                        if (rnd == 1 && magicPawn.MagicData.MagicPowersS[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_SummonPylon;
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersS[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_SummonExplosive;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersS[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_SummonElemental;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.TM_Bard))
                {
                    int level = magicPawn.MagicData.MagicPowersB[3].level;
                    switch (level)
                    {
                        case 0:
                            tempAbility = TorannMagicDefOf.TM_Lullaby;
                            break;
                        case 1:
                            tempAbility = TorannMagicDefOf.TM_Lullaby_I;
                            break;
                        case 2:
                            tempAbility = TorannMagicDefOf.TM_Lullaby_II;
                            break;
                        case 3:
                            tempAbility = TorannMagicDefOf.TM_Lullaby_III;
                            break;
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Warlock))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(1, 3);
                        if (rnd == 1 && magicPawn.MagicData.MagicPowersWD[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            int level = magicPawn.MagicData.MagicPowersWD[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersWD[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Dominate;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersWD[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersWD[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Repulsion;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Repulsion_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Repulsion_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Repulsion_III;
                                    break;
                            }
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Succubus))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(1, 3);
                        if (rnd == 1 && magicPawn.MagicData.MagicPowersSD[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            int level = magicPawn.MagicData.MagicPowersSD[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_ShadowBolt_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 2 && magicPawn.MagicData.MagicPowersSD[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_Dominate;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersSD[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersSD[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Attraction;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Attraction_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Attraction_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Attraction_III;
                                    break;
                            }
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Geomancer))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(0, 2);
                        if (rnd == 2)
                        {
                            rnd = 3;
                        }
                        if (rnd == 0 && magicPawn.MagicData.MagicPowersG[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_Stoneskin;
                            i = 5;
                        }
                        else if (rnd == 1 && magicPawn.MagicData.MagicPowersG[rnd].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersG[rnd].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Encase;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Encase_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Encase_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Encase_III;
                                    break;
                            }
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersG[rnd].learned && !caster.story.WorkTagIsDisabled(WorkTags.Violent))
                        {
                            tempAbility = TorannMagicDefOf.TM_EarthernHammer;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Technomancer))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(3, 5);
                        if (rnd == 3 && magicPawn.MagicData.MagicPowersT[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_TechnoShield;
                            i = 5;
                        }
                        else if (rnd == 4 && magicPawn.MagicData.MagicPowersT[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_Sabotage;
                            i = 5;
                        }
                        else if (rnd == 5 && magicPawn.MagicData.MagicPowersT[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_Overdrive;
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Enchanter))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (magicPawn.MagicData.MagicPowersE[4].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersE[4].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_Polymorph;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_Polymorph_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_Polymorph_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_Polymorph_III;
                                    break;
                            }
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.Chronomancer))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int rnd = Rand.RangeInclusive(2, 4);
                        if (rnd == 2 && magicPawn.MagicData.MagicPowersC[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_AccelerateTime;
                            i = 5;
                        }
                        else if (rnd == 3 && magicPawn.MagicData.MagicPowersC[rnd].learned)
                        {
                            tempAbility = TorannMagicDefOf.TM_ReverseTime;
                            i = 5;
                        }
                        else if (magicPawn.MagicData.MagicPowersC[4].learned)
                        {
                            int level = magicPawn.MagicData.MagicPowersC[4].level;
                            switch (level)
                            {
                                case 0:
                                    tempAbility = TorannMagicDefOf.TM_ChronostaticField;
                                    break;
                                case 1:
                                    tempAbility = TorannMagicDefOf.TM_ChronostaticField_I;
                                    break;
                                case 2:
                                    tempAbility = TorannMagicDefOf.TM_ChronostaticField_II;
                                    break;
                                case 3:
                                    tempAbility = TorannMagicDefOf.TM_ChronostaticField_III;
                                    break;
                            }
                            i = 5;
                        }
                    }
                }
                else if (targetPawn.story.traits.HasTrait(TorannMagicDefOf.BloodMage))
                {
                    Messages.Message("TM_CannotMimicBloodMage".Translate(
                        ), MessageTypeDefOf.RejectInput);
                }
            }
            return tempAbility;
        }

        public static int GetMightSkillLevel(Pawn caster, List<MightPowerSkill> power, string skillLabel, string suffix, bool canCopy = true)
        {
            int val = 0;
            string label = skillLabel + suffix;
            CompAbilityUserMight comp = caster.GetComp<CompAbilityUserMight>();
            val = power.FirstOrDefault((MightPowerSkill x) => x.label == label).level;
            if (canCopy)
            {
                if (caster.story.traits.HasTrait(TorannMagicDefOf.Faceless))
                {
                    label = "TM_Mimic" + suffix;
                    val = comp.MightData.MightPowerSkill_Mimic.FirstOrDefault((MightPowerSkill x) => x.label == label).level;
                }
                if (caster.story.traits.HasTrait(TorannMagicDefOf.TM_Wayfarer) || (caster.story.traits.HasTrait(TorannMagicDefOf.ChaosMage) && comp.MightData.MightPowersW.FirstOrDefault((MightPower x) => x.abilityDef == TorannMagicDefOf.TM_WayfarerCraft).learned))
                {
                    label = "TM_FieldTraining" + suffix;

                    if (comp.MightData.MightPowerSkill_FieldTraining.FirstOrDefault((MightPowerSkill x) => x.label == label).level >= 13)
                    {
                        val = 2;
                    }
                    else if (comp.MightData.MightPowerSkill_FieldTraining.FirstOrDefault((MightPowerSkill x) => x.label == label).level >= 9)
                    {
                        val = 1;
                    }
                }                
            }
            ModOptions.SettingsRef settingsRef = new ModOptions.SettingsRef();
            if (settingsRef.AIHardMode && !caster.IsColonist)
            {
                val = 3;
            }
            return val;
        }

        public static bool IsIconAbility_02(AbilityDef def)
        {
            if((def == TorannMagicDefOf.TM_RayofHope || def == TorannMagicDefOf.TM_RayofHope_I || def == TorannMagicDefOf.TM_RayofHope_II ||
                        def == TorannMagicDefOf.TM_Soothe || def == TorannMagicDefOf.TM_Soothe_I || def == TorannMagicDefOf.TM_Soothe_II ||
                        def == TorannMagicDefOf.TM_Shadow || def == TorannMagicDefOf.TM_Shadow_I || def == TorannMagicDefOf.TM_Shadow_II ||
                        def == TorannMagicDefOf.TM_AMP || def == TorannMagicDefOf.TM_AMP_I || def == TorannMagicDefOf.TM_AMP_II ||
                        def == TorannMagicDefOf.TM_Shield || def == TorannMagicDefOf.TM_Shield_I || def == TorannMagicDefOf.TM_Shield_II ||
                        def == TorannMagicDefOf.TM_Blink || def == TorannMagicDefOf.TM_Blink_I || def == TorannMagicDefOf.TM_Blink_II ||
                        def == TorannMagicDefOf.TM_Summon || def == TorannMagicDefOf.TM_Summon_I || def == TorannMagicDefOf.TM_Summon_II ||
                        def == TorannMagicDefOf.TM_MagicMissile || def == TorannMagicDefOf.TM_MagicMissile_I || def == TorannMagicDefOf.TM_MagicMissile_II ||
                        def == TorannMagicDefOf.TM_FrostRay || def == TorannMagicDefOf.TM_FrostRay_I || def == TorannMagicDefOf.TM_FrostRay_II ||
                        def == TorannMagicDefOf.TM_SootheAnimal || def == TorannMagicDefOf.TM_SootheAnimal_I || def == TorannMagicDefOf.TM_SootheAnimal_II ||
                        def == TorannMagicDefOf.TM_DeathMark || def == TorannMagicDefOf.TM_DeathMark_I || def == TorannMagicDefOf.TM_DeathMark_II ||
                        def == TorannMagicDefOf.TM_ConsumeCorpse || def == TorannMagicDefOf.TM_ConsumeCorpse_I || def == TorannMagicDefOf.TM_ConsumeCorpse_II ||
                        def == TorannMagicDefOf.TM_CorpseExplosion || def == TorannMagicDefOf.TM_CorpseExplosion_I || def == TorannMagicDefOf.TM_CorpseExplosion_II ||
                        def == TorannMagicDefOf.TM_DeathBolt || def == TorannMagicDefOf.TM_DeathBolt_I || def == TorannMagicDefOf.TM_DeathBolt_II ||
                        def == TorannMagicDefOf.TM_HealingCircle || def == TorannMagicDefOf.TM_HealingCircle_I || def == TorannMagicDefOf.TM_HealingCircle_II ||
                        def == TorannMagicDefOf.TM_Lullaby || def == TorannMagicDefOf.TM_Lullaby_I || def == TorannMagicDefOf.TM_Lullaby_II ||
                        def == TorannMagicDefOf.TM_ShadowBolt || def == TorannMagicDefOf.TM_ShadowBolt_I || def == TorannMagicDefOf.TM_ShadowBolt_II ||
                        def == TorannMagicDefOf.TM_Attraction || def == TorannMagicDefOf.TM_Attraction_I || def == TorannMagicDefOf.TM_Attraction_II ||
                        def == TorannMagicDefOf.TM_Repulsion || def == TorannMagicDefOf.TM_Repulsion_I || def == TorannMagicDefOf.TM_Repulsion_II ||
                        def == TorannMagicDefOf.TM_Encase || def == TorannMagicDefOf.TM_Encase_I || def == TorannMagicDefOf.TM_Encase_II ||
                        def == TorannMagicDefOf.TM_Meteor || def == TorannMagicDefOf.TM_Meteor_I || def == TorannMagicDefOf.TM_Meteor_II ||
                        def == TorannMagicDefOf.TM_OrbitalStrike || def == TorannMagicDefOf.TM_OrbitalStrike_I || def == TorannMagicDefOf.TM_OrbitalStrike_II ||
                        def == TorannMagicDefOf.TM_Rend || def == TorannMagicDefOf.TM_Rend_I || def == TorannMagicDefOf.TM_Rend_II ||
                        def == TorannMagicDefOf.TM_BloodMoon || def == TorannMagicDefOf.TM_BloodMoon_I || def == TorannMagicDefOf.TM_BloodMoon_II ||
                        def == TorannMagicDefOf.TM_Polymorph || def == TorannMagicDefOf.TM_Polymorph_I || def == TorannMagicDefOf.TM_Polymorph_II ||
                        def == TorannMagicDefOf.TM_BestowMight || def == TorannMagicDefOf.TM_BestowMight_I || def == TorannMagicDefOf.TM_BestowMight_II ||
                        def == TorannMagicDefOf.TM_ChronostaticField || def == TorannMagicDefOf.TM_ChronostaticField_I || def == TorannMagicDefOf.TM_ChronostaticField_II))
            {
                return true;
            }
            return false;
        }

        public static bool IsIconAbility_03(AbilityDef def)
        {
            if(TM_Calc.IsIconAbility_02(def))
            {
                return true;
            }
            else if ((def == TorannMagicDefOf.TM_RayofHope_III || def == TorannMagicDefOf.TM_Soothe_III || def == TorannMagicDefOf.TM_Shadow_III ||
                        def == TorannMagicDefOf.TM_AMP_III || def == TorannMagicDefOf.TM_Shield_III || def == TorannMagicDefOf.TM_Blink_III ||
                        def == TorannMagicDefOf.TM_Summon_III || def == TorannMagicDefOf.TM_MagicMissile_III || def == TorannMagicDefOf.TM_FrostRay_III ||
                        def == TorannMagicDefOf.TM_SootheAnimal_III || def == TorannMagicDefOf.TM_DeathMark_III || def == TorannMagicDefOf.TM_ConsumeCorpse_III ||
                        def == TorannMagicDefOf.TM_CorpseExplosion_III || def == TorannMagicDefOf.TM_DeathBolt_III || def == TorannMagicDefOf.TM_HealingCircle_III ||
                        def == TorannMagicDefOf.TM_Lullaby_III || def == TorannMagicDefOf.TM_ShadowBolt_III || def == TorannMagicDefOf.TM_Attraction_III ||
                        def == TorannMagicDefOf.TM_Repulsion_III || def == TorannMagicDefOf.TM_Encase_III || def == TorannMagicDefOf.TM_Meteor_III ||
                        def == TorannMagicDefOf.TM_OrbitalStrike_III || def == TorannMagicDefOf.TM_Rend_III || def == TorannMagicDefOf.TM_BloodMoon_III ||
                        def == TorannMagicDefOf.TM_Polymorph_III || def == TorannMagicDefOf.TM_BestowMight_III || def == TorannMagicDefOf.TM_ChronostaticField_III))
            {
                return true;
            }
            return false;
        }

        public static bool IsMasterAbility(AbilityDef def)
        {
            if(def == TorannMagicDefOf.TM_Firestorm ||
                def == TorannMagicDefOf.TM_Blizzard  ||
                def == TorannMagicDefOf.TM_EyeOfTheStorm ||
                def == TorannMagicDefOf.TM_FoldReality ||
                def == TorannMagicDefOf.TM_RegrowLimb ||
                def == TorannMagicDefOf.TM_LichForm ||
                def == TorannMagicDefOf.TM_SummonPoppi ||
                def == TorannMagicDefOf.TM_BattleHymn ||
                def == TorannMagicDefOf.TM_Scorn ||
                def == TorannMagicDefOf.TM_PsychicShock  ||
                def == TorannMagicDefOf.TM_Meteor || def == TorannMagicDefOf.TM_Meteor_I || def == TorannMagicDefOf.TM_Meteor_II || def == TorannMagicDefOf.TM_Meteor_III ||
                def == TorannMagicDefOf.TM_OrbitalStrike || def == TorannMagicDefOf.TM_OrbitalStrike_I || def == TorannMagicDefOf.TM_OrbitalStrike_II || def == TorannMagicDefOf.TM_OrbitalStrike_III ||
                def == TorannMagicDefOf.TM_BloodMoon || def == TorannMagicDefOf.TM_BloodMoon_I || def == TorannMagicDefOf.TM_BloodMoon_II || def == TorannMagicDefOf.TM_BloodMoon_III ||
                def == TorannMagicDefOf.TM_Shapeshift ||
                def == TorannMagicDefOf.TM_Recall  ||
                def == TorannMagicDefOf.TM_HolyWrath ||
                def == TorannMagicDefOf.TM_Resurrection)
            {
                return true;
            }
            return false;
        }

        public static void AssignChaosMagicPowers(CompAbilityUserMagic comp, bool forAI = false)
        {
            
            if (comp != null)
            {
                int count = 2 + comp.MagicData.MagicPowerSkill_ChaosTradition.FirstOrDefault((MagicPowerSkill x) => x.label == "TM_ChaosTradition_ver").level;
                for(int x =0; x < comp.MagicData.AllMagicPowersForChaosMage.Count; x++)
                {
                    MagicPower mp = comp.MagicData.AllMagicPowersForChaosMage[x];
                    mp.level = 0;
                    mp.autocast = false;
                }
                comp.MagicData.ResetAllSkills();
                comp.chaosPowers = new List<TM_ChaosPowers>();
                comp.chaosPowers.Clear();
                comp.RemovePowers(false);
                comp.MagicData.MagicAbilityPoints = comp.MagicData.MagicUserLevel;
                if(forAI)
                {
                    MagicPower p = comp.MagicData.AllMagicPowersForChaosMage.FirstOrDefault((MagicPower x) => x.abilityDef == TorannMagicDefOf.TM_Heal);
                    p.learned = true;
                    comp.chaosPowers.Add(new TM_ChaosPowers((TMAbilityDef)p.abilityDef, TM_Calc.GetAssociatedMagicPowerSkill(comp, p)));
                    comp.AddPawnAbility(p.abilityDef);
                    p = comp.MagicData.AllMagicPowersForChaosMage.FirstOrDefault((MagicPower x) => x.abilityDef == TorannMagicDefOf.TM_LightningBolt);
                    p.learned = true;
                    comp.chaosPowers.Add(new TM_ChaosPowers((TMAbilityDef)p.abilityDef, TM_Calc.GetAssociatedMagicPowerSkill(comp, p)));
                    comp.AddPawnAbility(p.abilityDef);
                    p = comp.MagicData.AllMagicPowersForChaosMage.FirstOrDefault((MagicPower x) => x.abilityDef == TorannMagicDefOf.TM_Shield);
                    p.learned = true;
                    comp.chaosPowers.Add(new TM_ChaosPowers((TMAbilityDef)p.abilityDef, TM_Calc.GetAssociatedMagicPowerSkill(comp, p)));
                    comp.AddPawnAbility(p.abilityDef);
                    p = comp.MagicData.AllMagicPowersForChaosMage.FirstOrDefault((MagicPower x) => x.abilityDef == TorannMagicDefOf.TM_Fireball);
                    p.learned = true;
                    comp.chaosPowers.Add(new TM_ChaosPowers((TMAbilityDef)p.abilityDef, TM_Calc.GetAssociatedMagicPowerSkill(comp, p)));
                    comp.AddPawnAbility(p.abilityDef);
                    p = TM_Calc.GetRandomMagicPower(comp); ;
                    comp.chaosPowers.Add(new TM_ChaosPowers((TMAbilityDef)p.abilityDef, null));
                }
                for (int i = 0; i < 5; i++)
                {
                    MagicPower power = TM_Calc.GetRandomMagicPower(comp);
                    if (i < count)
                    {
                        power.learned = true;
                        comp.chaosPowers.Add(new TM_ChaosPowers((TMAbilityDef)power.abilityDef, TM_Calc.GetAssociatedMagicPowerSkill(comp, power)));
                        if (power.abilityDef != TorannMagicDefOf.TM_TechnoBit && power.abilityDef != TorannMagicDefOf.TM_WandererCraft && power.abilityDef != TorannMagicDefOf.TM_Cantrips)
                        {
                            comp.AddPawnAbility(power.abilityDef);
                        }
                    }
                    else
                    {
                        comp.chaosPowers.Add(new TM_ChaosPowers((TMAbilityDef)power.abilityDef, null));
                    }
                }
                comp.InitializeSpell();
            }
            else
            {
                Log.Message("failed chaos mage ability assignment - null comp");
            }
        }

        public static MagicPower GetRandomMagicPower(CompAbilityUserMagic comp, bool includeMasterPower = false)
        {
            bool isDuplicate = true;
            MagicPower power = null;
            while (isDuplicate)
            {
                isDuplicate = false;
                power = comp.MagicData.AllMagicPowersForChaosMage.RandomElement();
                for (int i = 0; i < comp.chaosPowers.Count; i++)
                {
                    if (comp.chaosPowers[i].Ability == power.abilityDef)
                    {
                        isDuplicate = true;
                    }
                }
            }
            return power;
        }

        public static List<MagicPowerSkill> GetAssociatedMagicPowerSkill(CompAbilityUserMagic comp, MagicPower power)
        {
            string str = power.TMabilityDefs.FirstOrDefault().defName.ToString() + "_";
            List<MagicPowerSkill> skills = new List<MagicPowerSkill>();
            skills.Clear();
            for (int i = 0; i < comp.MagicData.AllMagicPowerSkills.Count; i++)
            {
                MagicPowerSkill mps = comp.MagicData.AllMagicPowerSkills[i];
                if (mps.label.Contains(str))
                {
                    skills.Add(mps);
                }
            }
            return skills;
        }
    }
}
