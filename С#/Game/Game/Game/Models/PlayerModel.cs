namespace Game.Models
{
    public class PlayerModel : ICreature
    {
        public int size => 192;

        public int speed => 4;

        public int spriteSize => 48;

        int ICreature.idleFrames => 6;

        int ICreature.runFrames => 6;

        int ICreature.attackFrames => 4;

        int ICreature.deathFrames => 4;

        int ICreature.delta => 12;
    }
}
