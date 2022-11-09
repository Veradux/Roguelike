public abstract class Modifier {

    protected Stat Stat { get; set; }
    private float ModifierValue { get; set; }

    public Modifier RegisterDependencies(Stat stat) {
        Stat = stat;

        return this;
    }

    public abstract void RegisterEventHandlers();

    public Modifier SetModifierValue(float value) {
        ModifierValue = value;

        return this;
    }

}
