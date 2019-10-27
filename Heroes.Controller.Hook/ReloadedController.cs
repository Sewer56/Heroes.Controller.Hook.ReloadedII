using Heroes.Controller.Hook.Interfaces.Structures;
using Heroes.Controller.Hook.Interfaces.Structures.Interfaces;
using Heroes.Controller.Hook.PostProcess.Configuration;
using Heroes.SDK.Classes.PseudoNativeClasses;
using Heroes.SDK.Definitions.Structures.Input;

namespace Heroes.Controller.Hook
{
    /// <summary>
    /// <see cref="ReloadedController"/> is an abstraction that writes Reloaded controls to Heroes' controller input struct.
    /// </summary>
    public unsafe class ReloadedController
    {
        // Sonic Heroes and XInput Controller
        private IInputs _lastFrameInputs = new Inputs();
        private int _port;
        private Config _config;

        /*
            ------------
            Constructors
            ------------
        */

        /// <summary>
        /// Creates a new <see cref="ReloadedController"/> that writes controls to Heroes' controller input struct.
        /// </summary>
        /// <param name="port">The controller port. Range 0-3.</param>
        /// <param name="modDirectory"></param>
        public ReloadedController(int port, string modDirectory)
        {
            _port = port;
            _config = new Configurator(modDirectory).GetConfiguration<Config>(port);
        }

        /*
            ---------
            Functions
            ---------
        */

        /// <summary>
        /// Sends the input into the game by writing them into memory.
        /// </summary>
        public void SendInputs(ControllerHook hook)
        {
            ref var heroesController = ref InputFunctions.Inputs[_port];
            IInputs inputs = _config.UseOriginalInputs
                ? Inputs.FromHeroesController(heroesController)
                : new Inputs();

            // Get inputs from subscribers.
            hook.InvokeSetInputs(ref inputs, _port);
            hook.InvokePostProcessInputs(ref inputs, _port);

            // Convert to game structure and write to memory.
            ((Inputs) inputs).ToHeroesController(out var newInputs);
            newInputs.Finalize((ButtonFlags) _lastFrameInputs.ButtonFlags);
            heroesController = newInputs;
            _lastFrameInputs = inputs;

            // Broadcast final inputs to subscribers.
            hook.InvokeOnInput(new ExtendedHeroesController(ref heroesController, inputs.LeftTriggerPressure, inputs.RightTriggerPressure), _port);
        }

        /// <summary>
        /// Writes the trigger pressures into game memory with data from the current frame.
        /// </summary>
        /// <remarks>
        ///     In a single game loop, the game will call the hook will execute <see cref="SendInputs"/>.
        ///     On return, the game will convert the internal structure into another structure <see cref="SkyPad"/>
        ///     supporting trigger pressure (while also doing some input post processing). We are modifying this structure
        ///     right before the game uses it to perform actions.
        /// </remarks>
        public void SetTriggers(SkyPad* skypad)
        {
            if ((_lastFrameInputs.ButtonFlags & (Interfaces.Definitions.ButtonFlags) ButtonFlags.CameraL) == 0)
                (*skypad).TriggerPressureL = _lastFrameInputs.LeftTriggerPressure;

            if ((_lastFrameInputs.ButtonFlags & (Interfaces.Definitions.ButtonFlags) ButtonFlags.CameraR) == 0)
                (*skypad).TriggerPressureR = _lastFrameInputs.RightTriggerPressure;
        }
    }
}