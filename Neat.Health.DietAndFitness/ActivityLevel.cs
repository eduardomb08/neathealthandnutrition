using System.ComponentModel;

namespace Neat.Health.DietAndFitness
{
    public enum ActivityLevel
    {
        [Description("Sedentary (little to no exercise)")]
        Sedentary,
        [Description("Light Activy (light exercise 1-3 days / week)")]
        LightlyActive,
        [Description("Moderate Activity (moderate exercise 3-5 days / week)")]
        ModeratelyActive,
        [Description("Very Active (hard exercise 6-7 days / week)")]
        VeryActive,
        [Description("Extremely Active (very hard exercise 6-7 days / week and physical job)")]
        ExtremelyActive
    }

}