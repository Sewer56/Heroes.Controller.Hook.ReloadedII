using System;
using System.Diagnostics;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Structures;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace Heroes.Controller.Hook.PostProcess
{
    public class Program : IMod
    {
        private const string HookModId  = "sonicheroes.controller.hook";
        private const string MyModId    = "sonicheroes.controller.hook.postprocess";

        private IModLoader _modLoader;
        private WeakReference<IControllerHook> _controllerHook;
        private PostProcess _postProcess;

        public void Start(IModLoaderV1 loader)
        {
            #if DEBUG
            Debugger.Launch();
            #endif
            _modLoader = (IModLoader)loader;

            /* Your mod code starts here. */
            _postProcess = new PostProcess(_modLoader.GetDirectoryForModId(MyModId));
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
                target.PostProcessInputs += TargetOnPostProcessInputs;
        }

        private void TearDownController()
        {
            if (_controllerHook.TryGetTarget(out var target))
                target.PostProcessInputs -= TargetOnPostProcessInputs;
        }

        private void TargetOnPostProcessInputs(ref Inputs inputs, int port)
        {
            _postProcess.PostProcessInputs(ref inputs, port);
        }

        /* Mod loader actions. */
        public void Suspend() => TearDownController();
        public void Resume()  => SetupController();
        public void Unload()  => TearDownController();

        public bool CanUnload()     => false;
        public bool CanSuspend()    => false;

        /* Automatically called by the mod loader when the mod is about to be unloaded. */
        public Action Disposing { get; }
    }
}
