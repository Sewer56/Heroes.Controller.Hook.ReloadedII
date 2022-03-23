using Heroes.Controller.Hook.PostProcess.Structures;
using Heroes.Controller.Hook.Shared.Configuration;

namespace Heroes.Controller.Hook.PostProcess;

public class Config : Configurable<Config>
{
    public StickDeadzone LeftStickXDeadzone         { get; set; } = new StickDeadzone();
    public StickDeadzone LeftStickYDeadzone         { get; set; } = new StickDeadzone();

    public StickDeadzone RightStickXDeadzone        { get; set; } = new StickDeadzone();
    public StickDeadzone RightStickYDeadzone        { get; set; } = new StickDeadzone();

    public TriggerDeadzone LeftTriggerDeadzone      { get; set; } = new TriggerDeadzone();
    public TriggerDeadzone RightTriggerDeadzone     { get; set; } = new TriggerDeadzone();

    public bool SwapTriggers                        { get; set; } = false;
}