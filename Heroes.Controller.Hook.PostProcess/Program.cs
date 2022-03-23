using System;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
#if DEBUG
using System.Diagnostics;
#endif

namespace Heroes.Controller.Hook.PostProcess;

public class Program : IMod
{
    public static string MyModId = "sonicheroes.controller.hook.postprocess";
    private const string HookModId  = "sonicheroes.controller.hook";

    public static IModLoader ModLoader;
    public static ILogger Logger;

    private WeakReference<IControllerHook> _controllerHook;
    private PostProcess _postProcess;


    public void StartEx(IModLoaderV1 loader, IModConfigV1 config)
    {
#if DEBUG
        Debugger.Launch();
#endif
        ModLoader = (IModLoader)loader;
        Logger = (ILogger)ModLoader.GetLogger();

        /* Your mod code starts here. */
        MyModId = config.ModId;
        _postProcess = new PostProcess(ModLoader.GetDirectoryForModId(MyModId), ModLoader.GetModConfigDirectory(MyModId));
        ModLoader.ModLoaded += ModLoaded;
        SetupController();
    }

    private void ModLoaded(IModV1 modInstance, IModConfigV1 modConfig)
    {
        if (modConfig.ModId == HookModId)
            SetupController();
    }

    private void SetupController()
    {
        _controllerHook = ModLoader.GetController<IControllerHook>();
        if (_controllerHook.TryGetTarget(out var target))
            target.PostProcessInputs += TargetOnPostProcessInputs;
    }

    private void TearDownController()
    {
        if (_controllerHook.TryGetTarget(out var target))
            target.PostProcessInputs -= TargetOnPostProcessInputs;
    }

    private void TargetOnPostProcessInputs(ref IInputs inputs, int port)
    {
        _postProcess.PostProcessInputs(ref inputs, port);
    }

    /* Mod loader actions. */
    public void Suspend() => TearDownController();
    public void Resume()  => SetupController();
    public void Unload()  => TearDownController();

    public bool CanUnload()     => true;
    public bool CanSuspend()    => true;

    /* Automatically called by the mod loader when the mod is about to be unloaded. */
    public Action Disposing { get; }

    public static void Main() { }
}