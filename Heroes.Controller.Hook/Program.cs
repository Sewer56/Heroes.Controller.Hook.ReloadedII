using System;
#if DEBUG
using System.Diagnostics;
#endif
using Heroes.Controller.Hook.Interfaces;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Heroes.Controller.Hook;

public class Program : IMod, IExports
{
    public static IReloadedHooks ReloadedHooks; // Reloaded.Shared.Hooks is not unloadable. Therefore not using WeakReference is justified.
    public const string ThisModId = "sonicheroes.controller.hook";

    public static IModLoader ModLoader;
    public static ILogger    Logger;

    private ControllerHook _hook;

    public static void Main() { }
    public void StartEx(IModLoaderV1 loader, IModConfigV1 config)
    {
#if DEBUG
        Debugger.Launch();
#endif
        ModLoader = (IModLoader)loader;
        Logger = (ILogger)ModLoader.GetLogger();

        ModLoader.GetController<IReloadedHooks>().TryGetTarget(out ReloadedHooks);
        SDK.SDK.Init(ReloadedHooks, null);

        /* Your mod code starts here. */
        _hook = new ControllerHook(ModLoader.GetDirectoryForModId(config.ModId), ModLoader.GetModConfigDirectory(config.ModId));
        ModLoader.AddOrReplaceController<IControllerHook>(this, _hook);
    }

    /* Mod loader actions. */
    public void Suspend()   => _hook.Suspend();
    public void Resume()    => _hook.Resume();
    public void Unload()    => Suspend();

    public bool CanUnload()  => true;
    public bool CanSuspend() => true;
    public Action Disposing { get; }

    public Type[] GetTypes() => new [] { typeof(IControllerHook) };
}