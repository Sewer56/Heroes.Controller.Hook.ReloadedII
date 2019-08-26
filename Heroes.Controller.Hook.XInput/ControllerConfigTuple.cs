using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Controller.Hook.XInput
{
    public class ControllerConfigTuple
    {
        public SharpDX.XInput.Controller Controller     { get; private set; }
        public Config                    Config         { get; private set; }

        public ControllerConfigTuple(SharpDX.XInput.Controller controller, Config config)
        {
            Controller = controller;
            Config = config;
        }
    }
}
