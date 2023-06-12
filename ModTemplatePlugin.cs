using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;

namespace ModTemplate;

[BepInPlugin(ModGuid, ModName, ModVersion)]
public class ModTemplatePlugin : BaseUnityPlugin
{
    public const string ModName = "ModTemplate";
    public const string ModVersion = "1.0.0";
    private const string ModAuthor = "RepaceMe";
    private const string ModGuid = ModAuthor + "." + ModName;

    private readonly Harmony _harmony = new(ModGuid);

    [UsedImplicitly] public static readonly ManualLogSource ModTemplateLogger =
        BepInEx.Logging.Logger.CreateLogSource(ModName);

    private void Awake()
    {
        // Plugin startup logic
        Logger.LogInfo($"Plugin {ModGuid} is loaded!");

        Assembly assembly = Assembly.GetExecutingAssembly();
        _harmony.PatchAll(assembly);
    }
}