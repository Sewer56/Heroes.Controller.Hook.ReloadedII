using System.Reflection;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Reloaded.Mod.Loader.IO.Config;

namespace Heroes.Controller.Hook.Custom;

public class Program : IMod
{
    /// <summary>
    /// Your mod if from ModConfig.json, used during initialization.
    /// </summary>
    private const string MyModId = "sonicheroes.controller.hook.custom";
    private const string HookModId = "sonicheroes.controller.hook";

    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private IModLoader _modLoader;

    /// <summary>
    /// Stores a reference to the controller hook in other mod.
    /// </summary>
    private WeakReference<IControllerHook> _controllerHook;
    private Input _input;

    /// <summary>
    /// Entry point for your mod.
    /// </summary>
    public void Start(IModLoaderV1 loader)
    {
        _modLoader = (IModLoader)loader;

        /* Your mod code starts here. */
        _input = new Input(_modLoader.GetDirectoryForModId(MyModId), _modLoader.GetModConfigDirectory(MyModId));
        _modLoader.ModLoaded += ModLoaded;
        SetupController();
    }

    private void ModLoaded(IModV1 modInstance, IModConfigV1 modConfig)
    {
        if (modConfig.ModId == HookModId)
            SetupController();
    }

    private void SetupController()
    {
        _controllerHook = _modLoader.GetController<IControllerHook>();
        if (_controllerHook.TryGetTarget(out var target))
            target.SetInputs += SetInputs;
    }

    private void TearDownController()
    {
        if (_controllerHook.TryGetTarget(out var target))
            target.SetInputs -= SetInputs;
    }

    private void SetInputs(ref IInputs inputs, int port) => _input.SetInputs(ref inputs, port);

    /* Mod loader actions. */
    public void Suspend() => TearDownController();
    public void Resume() => SetupController();
    public void Unload() => TearDownController();

    /*  If CanSuspend == false, suspend and resume button are disabled in Launcher and Suspend()/Resume() will never be called.
        If CanUnload == false, unload button is disabled in Launcher and Unload() will never be called.
    */
    public bool CanUnload() => true;
    public bool CanSuspend() => true;

    /* Automatically called by the mod loader when the mod is about to be unloaded. */
    public Action Disposing { get; }

    /* This is a dummy for R2R (ReadyToRun) deployment.
       For more details see: https://github.com/Reloaded-Project/Reloaded-II/blob/master/Docs/ReadyToRun.md
    */
    [STAThread]
    public static void Main()
    {
        var userConfigDirectory = ModUserConfig.GetUserConfigFolderForMod(MyModId);

        var dllPath       = Assembly.GetExecutingAssembly().Location;
        var modFolderPath = Path.GetDirectoryName(dllPath);
        var folderName = Path.GetFileNameWithoutExtension(modFolderPath);

        // Navigate one folder up if R2R built.
        if (string.Equals(folderName, "x86", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(folderName, "x64", StringComparison.OrdinalIgnoreCase))
            modFolderPath = Path.GetDirectoryName(modFolderPath);

        var configurator = new Input(modFolderPath, userConfigDirectory);
        configurator.Configure();
    }
}