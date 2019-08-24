using System;
using System.Diagnostics;
using System.IO.Pipes;
using Heroes.Controller.Hook.Interfaces;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Heroes.Controller.Hook
{
    public class Program : IMod, IExports
    {
        public static IReloadedHooks ReloadedHooks; // Reloaded.Shared.Hooks is not unloadable. Therefore not using WeakReference is justified.
        public const string ThisModId = "sonicheroes.controller.hook";

        private IModLoader _modLoader;
        private ControllerHook _hook;

        public void Start(IModLoaderV1 loader)
        {
            #if DEBUG
            Debugger.Launch();
            #endif
            _modLoader = (IModLoader)loader;
            _modLoader.GetController<IReloadedHooks>().TryGetTarget(out ReloadedHooks);

            /* Your mod code starts here. */
            _hook = new ControllerHook(_modLoader.GetDirectoryForModId(ThisModId));
            _modLoader.AddOrReplaceController<IControllerHook>(this, _hook);
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
