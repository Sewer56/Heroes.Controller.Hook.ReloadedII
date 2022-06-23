using Heroes.Controller.Hook.PostProcess.Structures;
using Heroes.Controller.Hook.Shared.Configuration;
using System.ComponentModel;

namespace Heroes.Controller.Hook.PostProcess;

public class Config : Configurable<Config>
{
    [DisplayName("Left Stick (Horizontal) Settings")]
    public StickDeadzone LeftStickXDeadzone         { get; set; } = new StickDeadzone();

    [DisplayName("Left Stick (Vertical) Settings")]
    public StickDeadzone LeftStickYDeadzone         { get; set; } = new StickDeadzone();

    [DisplayName("Right Stick (Horizontal) Settings")]
    public StickDeadzone RightStickXDeadzone        { get; set; } = new StickDeadzone();

    [DisplayName("Right Stick (Vertical) Settings")]
    public StickDeadzone RightStickYDeadzone        { get; set; } = new StickDeadzone();

    [DisplayName("Left Trigger Settings")]
    public TriggerDeadzone LeftTriggerDeadzone      { get; set; } = new TriggerDeadzone();

    [DisplayName("Right Trigger Settings")]
    public TriggerDeadzone RightTriggerDeadzone     { get; set; } = new TriggerDeadzone();

    [DisplayName("Swap Triggers")]
    [Description("Swaps the left and right trigger.")]
    [DefaultValue(false)]
    public bool SwapTriggers                        { get; set; } = false;
}