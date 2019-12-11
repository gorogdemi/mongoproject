namespace MongoProject.WebApp.Data.Models
{
    public class StepperMotor : Component
    {
        public string Size { get; set; }
        public double Shaftdiameter { get; set; }
        public double Holdingtorque { get; set; }
        public double SPR { get; set; }
        public double Currentrating { get; set; }
        public double Voltagerating { get; set; }
    }
}
