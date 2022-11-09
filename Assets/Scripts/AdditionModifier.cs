public class AdditionModifier : Modifier {

    public float Amount { get; set; }

    public override void RegisterEventHandlers() {
        Stat.CalculateStatHandlers.Add(new PlayerCalculateStatHandler {
            CalculationType = CalculationType.Addition,
            HandleStatCalculation = HandleStatCalculation
        });
    }

    private float HandleStatCalculation(float statValue) {
        return statValue + Amount;
    }

}
