using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;

namespace NoSkillDrain;

public static class NoSkillDrain
{
    private static float ApplyModifierValue(float targetValue, float value)
    {
        if (value <= -100)
            value = -100;
        return targetValue + targetValue / 100 * value;
    }

    [HarmonyPatch(typeof(Skills), nameof(Skills.OnDeath))]
    public static class NoSkillDrainTranspiler
    {
        private static readonly MethodInfo MethodSkillsLowerAllSkills =
            AccessTools.Method(typeof(Skills), nameof(Skills.LowerAllSkills));

        private static readonly MethodInfo MethodLowerAllSkills =
            AccessTools.Method(typeof(NoSkillDrainTranspiler), nameof(LowerAllSkills));

        [HarmonyTranspiler]
        [UsedImplicitly]
        public static IEnumerable<CodeInstruction> Transpile(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> il = instructions.ToList();
            foreach (var t in il.Where(t => t.Calls(MethodSkillsLowerAllSkills)))
            {
                t.operand = MethodLowerAllSkills;
            }

            return il.AsEnumerable();
        }

        public static void LowerAllSkills(Skills instance, float factor)
        {
            if (NoSkillDrainPlugin.SkillDrainMultiplier.Value > -100.0f)
            {
                instance.LowerAllSkills(ApplyModifierValue(factor,
                    NoSkillDrainPlugin.SkillDrainMultiplier.Value));
                NoSkillDrainPlugin.NoSkillDrainLogger.LogInfo(
                    $"skills reduced by factor {NoSkillDrainPlugin.SkillDrainMultiplier.Value}");
            }
            else
            {
                NoSkillDrainPlugin.NoSkillDrainLogger.LogInfo("no skill drain applied on death");
            }
        }
    }
}