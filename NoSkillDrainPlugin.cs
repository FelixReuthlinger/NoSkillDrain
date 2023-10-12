using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using ServerSync;

namespace NoSkillDrain;

[BepInPlugin(ModGuid, ModName, ModVersion)]
public class NoSkillDrainPlugin : BaseUnityPlugin
{
    public const string ModName = "NoSkillDrain";
    public const string ModVersion = "1.2.1";
    private const string ModAuthor = "FixItFelix";
    private const string ModGuid = ModAuthor + "." + ModName;

    [UsedImplicitly] public static readonly ManualLogSource NoSkillDrainLogger =
        BepInEx.Logging.Logger.CreateLogSource(ModName);
    
    private static readonly ConfigSync ConfigSync = new(ModGuid)
        { DisplayName = ModName, CurrentVersion = ModVersion };
    
    private static ConfigEntry<bool> _configLocked = null!;
    public static ConfigEntry<float> SkillDrainMultiplier = null!;

    private void Awake()
    {

        _configLocked = CreateConfig("1 - General", "Lock Configuration", true,
            "If 'true' and playing on a server, config can only be changed on server-side configuration, " +
            "clients cannot override");
        ConfigSync.AddLockingConfigEntry(_configLocked);
            
        SkillDrainMultiplier = CreateConfig("1 - General", "Skill Drain Multiplier", -100f,
            "If set to -100 it will not drain skills at all, other settings will apply " +
            "the % multiplier accordingly.");

        Assembly assembly = Assembly.GetExecutingAssembly();
        Harmony harmony = new Harmony(ModGuid);
        harmony.PatchAll(assembly);
    }

    private ConfigEntry<T> CreateConfig<T>(string group, string parameterName, T value,
        ConfigDescription description,
        bool synchronizedSetting = true)
    {
        ConfigEntry<T> configEntry = Config.Bind(group, parameterName, value, description);

        SyncedConfigEntry<T> syncedConfigEntry = ConfigSync.AddConfigEntry(configEntry);
        syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

        return configEntry;
    }

    private ConfigEntry<T> CreateConfig<T>(string group, string parameterName, T value, string description,
        bool synchronizedSetting = true) => CreateConfig(group, parameterName, value,
        new ConfigDescription(description), synchronizedSetting);

}