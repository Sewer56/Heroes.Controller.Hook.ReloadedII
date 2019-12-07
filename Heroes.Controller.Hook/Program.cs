using System;
using System.Diagnostics;
using System.IO.Pipes;
using Heroes.Controller.Hook.Interfaces;
using Heroes.SDK;
using Heroes.SDK.Definitions.Structures.Input;
using Reloaded.Hooks;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Heroes.Controller.Hook
{
    public class Program : IMod, IExports
    {
        public static IReloadedHooks ReloadedHooks; // Reloaded.Shared.Hooks is not unloadable. Therefore not using WeakReference is justified.
        public const string ThisModId = "sonicheroes.controller.hook";

        public static IModLoader ModLoader;
        public static ILogger    Logger;

        private ControllerHook _hook;

        public void Start(IModLoaderV1 loader)
        {
            #if DEBUG
            Debugger.Launch();
            #endif
            ModLoader = (IModLoader)loader;
            Logger = (ILogger) ModLoader.GetLogger();

            ModLoader.GetController<IReloadedHooks>().TryGetTarget(out ReloadedHooks);
            SDK.SDK.Init(ReloadedHooks);

            /* Your mod code starts here. */
            _hook = new ControllerHook(ModLoader.GetDirectoryForModId(ThisModId));
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
}
