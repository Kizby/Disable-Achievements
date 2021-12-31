using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace XRL.DisableAchievements
{
    [HarmonyPatch(typeof(LeaderboardManager), "SubmitResult")]
    public static class PatchSubmitResult
    {
        public static bool Prefix(ref string __result)
        {
            // skip submission to the leaderboard
            __result = null;
            return false;
        }
    }

    [HarmonyPatch]
    public static class PatchAchievementManager
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            return AccessTools.TypeByName("AchievementManager")
                .GetMethods()
                .Where(method => !method.IsConstructor && method.ReturnType == typeof(void))
                .Cast<MethodBase>();
        }

        public static bool Prefix()
        {
            // skip all achievement manager stuff
            return false;
        }
    }

}
