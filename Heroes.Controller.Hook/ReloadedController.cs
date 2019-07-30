using System.Runtime.CompilerServices;
using Heroes.Controller.Hook.Heroes;
using Heroes.Controller.Hook.Interfaces;
using Heroes.Controller.Hook.Interfaces.Enums;
using Heroes.Controller.Hook.Interfaces.Structures;

namespace Heroes.Controller.Hook
{
    /// <summary>
    /// <see cref="ReloadedController"/> is an abstraction that writes Reloaded controls to Heroes' controller input struct.
    /// </summary>
    public unsafe class ReloadedController
    {
        // Sonic Heroes and XInput Controller
        private HeroesController* _heroesControllerPtr;
        private Inputs _lastFrameInputs;
        private int _port;

        /*
            ------------
            Constructors
            ------------
        */

        /// <summary>
        /// Creates a new <see cref="ReloadedController"/> that writes controls to Heroes' controller input struct.
        /// </summary>
        /// <param name="port">The controller port. Range 0-3.</param>
        public ReloadedController(int port)
        {
            _port = port;
            _heroesControllerPtr = HeroesControllerFactory.GetController(_port);
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
            var inputs = new Inputs();

            // Get from and broadcast final inputs to subscribers.
            hook.InvokeSetInputs(ref inputs, _port);
            hook.InvokePostProcessInputs(ref inputs, _port);
            hook.InvokeOnInput(inputs, _port);

            // Convert to game structure and write to memory.
            HeroesController.FromInputs(ref inputs, out var newInputs);
            newInputs.Finalize(_lastFrameInputs.ButtonFlags);

            *_heroesControllerPtr = newInputs;
            _lastFrameInputs = inputs;
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
            if ((_lastFrameInputs.ButtonFlags & ButtonFlags.CameraL) == 0)
                (*skypad).TriggerPressureL = _lastFrameInputs.LeftTriggerPressure;

            if ((_lastFrameInputs.ButtonFlags & ButtonFlags.CameraR) == 0)
                (*skypad).TriggerPressureR = _lastFrameInputs.RightTriggerPressure;
        }
    }
}