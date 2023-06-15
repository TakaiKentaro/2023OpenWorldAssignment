public interface IActorStatus
{
    int Health { get; set; }
    int Attack { get; set; }
    int Speed { get; set; }

    void SetStatus(int health, int attack, int speed);
}