using UniRx;

public class ActorStatusBase
{
    private readonly ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    private readonly ReactiveProperty<int> _attack = new ReactiveProperty<int>();
    private readonly ReactiveProperty<int> _defense = new ReactiveProperty<int>();
    private readonly ReactiveProperty<int> _agility = new ReactiveProperty<int>();

    public IReadOnlyReactiveProperty<int> HP => _hp;
    public IReadOnlyReactiveProperty<int> Attack => _attack;
    public IReadOnlyReactiveProperty<int> Defense => _defense;
    public IReadOnlyReactiveProperty<int> Agility => _agility;

    public ActorStatusBase(int hp, int attack, int defense, int agility)
    {
        _hp.Value = hp;
        _attack.Value = attack;
        _defense.Value = defense;
        _agility.Value = agility;
    }
}