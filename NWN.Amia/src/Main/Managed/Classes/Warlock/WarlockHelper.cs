using System;
using NWN.Amia.Main.Managed.Spells.Commons;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Classes.Warlock
{
    public class WarlockHelper
    {
        private uint _creature;

        private const int WarlockClassId = 0;

        private const int BaseDc = 10;


        public WarlockHelper(uint creature)
        {
            _creature = creature;
        }

        public int CalculateBlastDice() => GetWarlockLevel() / 2;

        private int GetWarlockLevel() => NWScript.GetLevelByClass(WarlockClassId, _creature);

        public static int GetBeamVfxNumberFromElement(int elementConstant)
        {
            return elementConstant switch
            {
                NWScript.DAMAGE_TYPE_MAGICAL => NWScript.VFX_BEAM_ODD,
                NWScript.DAMAGE_TYPE_FIRE => 444,
                NWScript.DAMAGE_TYPE_ACID => NWScript.VFX_BEAM_DISINTEGRATE,
                NWScript.DAMAGE_TYPE_COLD => NWScript.VFX_BEAM_COLD,
                NWScript.DAMAGE_TYPE_NEGATIVE => NWScript.VFX_BEAM_EVIL,
                _ => NWScript.VFX_BEAM_ODD
            };
        }

        public static IntPtr GetImpactVfxFromBeam(int beamVfx)
        {
            return beamVfx switch
            {
                NWScript.VFX_BEAM_COLD => NWScript.EffectVisualEffect(NWScript.VFX_IMP_FROST_S),
                444 => NWScript.EffectVisualEffect(NWScript.VFX_IMP_FLAME_M),
                NWScript.VFX_BEAM_DISINTEGRATE => NWScript.EffectVisualEffect(NWScript.VFX_IMP_ACID_S),
                NWScript.VFX_BEAM_EVIL => NWScript.EffectVisualEffect(NWScript.VFX_IMP_NEGATIVE_ENERGY),
                NWScript.VFX_BEAM_ODD => NWScript.EffectVisualEffect(NWScript.VFX_IMP_MAGBLUE),
                _ => NWScript.EffectVisualEffect(NWScript.VFX_IMP_MAGBLUE)
            };
        }

        public static void ApplyBeShadowEffects(uint caster, uint target)
        {
            const int innateLevel = 4;
            int difficultyClass =
                BaseDc + innateLevel + NWScript.GetAbilityModifier(NWScript.ABILITY_CHARISMA, caster);

            bool targetFailedSavingThrow = SpellUtils.SavingThrow(NWScript.SAVING_THROW_FORT, target, difficultyClass,
                NWScript.SAVING_THROW_TYPE_SPELL, caster) == NWScript.FALSE;

            if (!targetFailedSavingThrow) return;

            IntPtr blind = NWScript.EffectBlindness();

            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_TEMPORARY,
                blind, target,
                NWScript.TurnsToSeconds(10));
        }

        public static void ApplyHellrimeEffects(uint caster, uint target)
        {
            const int innateSpellLevel = 4;
            int difficultyClass =
                BaseDc + innateSpellLevel + NWScript.GetAbilityModifier(NWScript.ABILITY_CHARISMA, caster);

            bool targetFailedSavingThrow = SpellUtils.SavingThrow(NWScript.SAVING_THROW_FORT, target, difficultyClass,
                NWScript.SAVING_THROW_TYPE_SPELL, caster) == NWScript.FALSE;

            if (!targetFailedSavingThrow) return;

            IntPtr dexDecrease = NWScript.EffectAbilityDecrease(NWScript.ABILITY_DEXTERITY, 4);
            IntPtr iceVfx = NWScript.EffectVisualEffect(NWScript.VFX_DUR_ICESKIN);

            IntPtr hellrimeEffect = NWScript.EffectLinkEffects(dexDecrease, iceVfx);
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_TEMPORARY,
                hellrimeEffect, target,
                NWScript.RoundsToSeconds(10));
        }

        public static void ApplyHinderingEffects(uint caster, uint target)
        {
            const int innateLevel = 4;
            int difficultyClass =
                BaseDc + innateLevel + NWScript.GetAbilityModifier(NWScript.ABILITY_CHARISMA, caster);

            bool targetFailedSavingThrow = SpellUtils.SavingThrow(NWScript.SAVING_THROW_WILL, target, difficultyClass,
                NWScript.SAVING_THROW_TYPE_SPELL, caster) == NWScript.FALSE;

            if (targetFailedSavingThrow)
            {
                NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_TEMPORARY, NWScript.EffectSlow(), target,
                    NWScript.RoundsToSeconds(10));
            }
        }

        public static void ApplyUtterdarkEffects(uint caster, uint target)
        {
            const int innateLevel = 8;
            int difficultyClass =
                BaseDc + innateLevel + NWScript.GetAbilityModifier(NWScript.ABILITY_CHARISMA, caster);

            bool targetFailedSavingThrow = SpellUtils.SavingThrow(NWScript.SAVING_THROW_FORT, target, difficultyClass,
                NWScript.SAVING_THROW_TYPE_SPELL, caster) == NWScript.FALSE;

            if (targetFailedSavingThrow)
            {
                NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_TEMPORARY, NWScript.EffectNegativeLevel(2), target,
                    NWScript.HoursToSeconds(1));
            }
        }

        public static void ApplyNoxiousEffects(uint caster, uint target)
        {
            const int innateLevel = 6;
            int difficultyClass =
                BaseDc + innateLevel + NWScript.GetAbilityModifier(NWScript.ABILITY_CHARISMA, caster);

            bool targetFailedSavingThrow = SpellUtils.SavingThrow(NWScript.SAVING_THROW_FORT, target, difficultyClass,
                NWScript.SAVING_THROW_TYPE_SPELL, caster) == NWScript.FALSE;

            if (targetFailedSavingThrow)
            {
                NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_TEMPORARY, NWScript.EffectDazed(), target,
                    NWScript.RoundsToSeconds(10));
            }
        }


        public static void ApplyBrimstoneEffects(uint caster, uint target)
        {
            bool alreadyOnFire = NWScript.GetLocalInt(target, "on_fire") == NWScript.TRUE;
            if (alreadyOnFire) return;

            const int innateLevel = 4;
            int difficultyClass =
                BaseDc + innateLevel + NWScript.GetAbilityModifier(NWScript.ABILITY_CHARISMA, caster);

            bool targetFailedSavingThrow = SpellUtils.SavingThrow(NWScript.SAVING_THROW_REFLEX, target, difficultyClass,
                NWScript.SAVING_THROW_TYPE_SPELL, caster) == NWScript.FALSE;

            IntPtr combustEffect = NWScript.EffectVisualEffect(NWScript.VFX_DUR_INFERNO_CHEST);

            if (!targetFailedSavingThrow) return;

            float delay = (float) 6.0;
            int timesToApply = NWScript.GetLevelByClass(WarlockClassId, caster) / 5;
            int maxBurnTicks = (timesToApply > 1 ? timesToApply : 1);

            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_TEMPORARY, combustEffect, target,
                NWScript.RoundsToSeconds(maxBurnTicks));
            for (int i = 0; i < maxBurnTicks; i++)
            {
                IntPtr fireDamage = NWScript.EffectDamage(2 * NWScript.d6(), NWScript.DAMAGE_TYPE_FIRE);
                NWScript.DelayCommand(delay,
                    () => NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, fireDamage, target));
                delay += (float) 6.0;
            }

            NWScript.SetLocalInt(target, "on_fire", NWScript.TRUE);

            NWScript.DelayCommand((float) (6.0 * maxBurnTicks),
                () => NWScript.SetLocalInt(target, "on_fire", NWScript.FALSE));
        }


        public static void ApplyVitriolicEffects(uint caster, uint target)
        {
            bool alreadyMelting = NWScript.GetLocalInt(target, "on_acid") == NWScript.TRUE;
            if (alreadyMelting) return;

            float delay = (float) 6.0;
            int timesToApply = NWScript.GetLevelByClass(WarlockClassId, caster) / 5;
            int maxBurnTicks = (timesToApply > 1 ? timesToApply : 1);

            for (int i = 0; i < maxBurnTicks; i++)
            {
                IntPtr acidDamage = NWScript.EffectDamage(2 * NWScript.d6(), NWScript.DAMAGE_TYPE_ACID);
                NWScript.DelayCommand(delay,
                    () => NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, acidDamage, target));
                delay += (float) 6.0;
            }

            NWScript.SetLocalInt(target, "on_acid", NWScript.TRUE);

            NWScript.DelayCommand((float) (6.0 * maxBurnTicks),
                () => NWScript.SetLocalInt(target, "on_acid", NWScript.FALSE));
        }
    }
}