using System;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Spells.Commons
{
    public static class SpellUtils
    {
        private const int SpellResistance = 1;
        private const int GlobeOfInvulnerability = 2;
        private const int SpellMantle = 3;

        public static int ResistSpell(uint caster, uint spellTarget, float effectApplicationDelay = 0)
        {
            if (effectApplicationDelay > 0.5)
            {
                effectApplicationDelay = (float) (effectApplicationDelay - 0.1);
            }

            int nResist = NWScript.ResistSpell(caster, spellTarget);
            IntPtr spellResistanceVfx = NWScript.EffectVisualEffect(NWScript.VFX_IMP_MAGIC_RESISTANCE_USE);
            IntPtr globeOfInvulnVfx = NWScript.EffectVisualEffect(NWScript.VFX_IMP_GLOBE_USE);
            IntPtr mantleUsevfx = NWScript.EffectVisualEffect(NWScript.VFX_IMP_SPELL_MANTLE_USE);
            switch (nResist)
            {
                case SpellResistance:
                    NWScript.DelayCommand(effectApplicationDelay,
                        () => NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, spellResistanceVfx,
                            spellTarget));
                    break;
                case GlobeOfInvulnerability:
                    NWScript.DelayCommand(effectApplicationDelay,
                        () => NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, globeOfInvulnVfx,
                            spellTarget));
                    break;
                case SpellMantle:
                {
                    if (effectApplicationDelay > 0.5)
                    {
                        effectApplicationDelay -= (float) 0.1;
                    }

                    NWScript.DelayCommand(effectApplicationDelay,
                        () => NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, mantleUsevfx, spellTarget));
                    break;
                }
            }

            return nResist;
        }


        public static int SavingThrow(int nSavingThrow, uint oTarget, int nDC,
            int nSaveType = NWScript.SAVING_THROW_TYPE_NONE,
            uint oSaveVersus = NWScript.OBJECT_INVALID, float fDelay = 0)
        {
            nDC = ResolveDcIntOverflow(nDC);

            IntPtr eVis = NWScript.EffectVisualEffect(NWScript.VFX_NONE);
            int isValid = NWScript.FALSE;
            switch (nSavingThrow)
            {
                case NWScript.SAVING_THROW_FORT:
                {
                    isValid = NWScript.FortitudeSave(oTarget, nDC, nSaveType, oSaveVersus);
                    if (isValid == 1)
                    {
                        eVis = NWScript.EffectVisualEffect(NWScript.VFX_IMP_FORTITUDE_SAVING_THROW_USE);
                    }

                    break;
                }
                case NWScript.SAVING_THROW_REFLEX:
                {
                    isValid = NWScript.ReflexSave(oTarget, nDC, nSaveType, oSaveVersus);
                    if (isValid == 1)
                    {
                        eVis = NWScript.EffectVisualEffect(NWScript.VFX_IMP_REFLEX_SAVE_THROW_USE);
                    }

                    break;
                }
                case NWScript.SAVING_THROW_WILL:
                {
                    isValid = NWScript.WillSave(oTarget, nDC, nSaveType, oSaveVersus);
                    if (isValid == 1)
                    {
                        eVis = NWScript.EffectVisualEffect(NWScript.VFX_IMP_WILL_SAVING_THROW_USE);
                    }

                    break;
                }
            }

            int nSpellId = NWScript.GetSpellId();

            switch (isValid)
            {
                case 0:
                {
                    if ((nSaveType == NWScript.SAVING_THROW_TYPE_DEATH
                         || nSpellId == NWScript.SPELL_WEIRD
                         || nSpellId == NWScript.SPELL_FINGER_OF_DEATH) &&
                        nSpellId != NWScript.SPELL_HORRID_WILTING)
                    {
                        eVis = NWScript.EffectVisualEffect(NWScript.VFX_IMP_DEATH);
                        NWScript.DelayCommand(fDelay,
                            () => NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, eVis, oTarget));
                    }

                    break;
                }
                case 1:
                case 2:
                {
                    if (isValid == 2)
                    {
                        eVis = NWScript.EffectVisualEffect(NWScript.VFX_IMP_MAGIC_RESISTANCE_USE);
                        isValid = NWScript.FALSE;
                    }

                    NWScript.DelayCommand(fDelay,
                        () => NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, eVis, oTarget));
                    break;
                }
            }

            return isValid;
        }

        private static int ResolveDcIntOverflow(int difficultyClass) =>
            difficultyClass switch
            {
                < 1 => 1,
                > 255 => 255,
                _ => difficultyClass
            };
    }
}