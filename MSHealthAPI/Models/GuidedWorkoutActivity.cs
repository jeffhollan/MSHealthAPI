namespace MSHealthAPI.Models
{
    public class GuidedWorkoutActivity : Activity
    {
        public int? cyclesPerformed { get; set; }
        public int? roundsPerformed { get; set; }
        public int? repetitionsPerformed { get; set; }
        public string workoutPlanId { get; set; }
    }
}