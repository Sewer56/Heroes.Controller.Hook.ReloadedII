using System;
using System.Diagnostics;
using Heroes.Controller.Hook.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Heroes.Controller.Hook
{
    public class Program : IMod, IExports
    {
        private IModLoader _modLoader;
        private ControllerHook _hook;

        public void Start(IModLoaderV1 loader)
        {
            #if DEBUG
            Debugger.Launch();
            #endif
            _modLoader = (IModLoader)loader;

            /* Your mod code starts here. */
            _hook = new ControllerHook();
            _modLoader.AddOrReplaceController<IControllerHook>(this, _hook);
        }

        /* Mod loader actions. */
        public void Suspend()   => _hook.Suspend();
        public void Resume()    => _hook.Resume();
        public void Unload()    => Suspend();

        public bool CanUnload()  => false;
        public bool CanSuspend() => false;
        public Action Disposing { get; }
        public Type[] GetTypes() => new [] { typeof(IControllerHook) };
    }
}
