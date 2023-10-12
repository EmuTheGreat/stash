namespace Game.Models
{
    public interface ICreature
    {
        int idleFrames { get; }
        int runFrames { get; }
        int attackFrames { get; }
        int deathFrames { get; }
        int size { get; }
        int spriteSize { get; }
        int speed { get; }
        int delta { get; }
    }
}
