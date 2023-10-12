using Game.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;

namespace Game
{
    public interface IEntity
    {
        int healthPoint { get; set; }
        bool isAlive { get; set; }

        int posX { get; set; }
        int posY { get; set; }

        int dirX { get; set; }
        int dirY { get; set; }
        int speed { get; set; }

        bool isMovingLeft { get; }
        bool isMovingRight { get; }
        bool isMovingUp { get; }
        bool isMovingDown { get; }
        bool isAttack { get; }
        bool deathAnimationFlag { get; }

        int currentFrame { get; set; }
        int currentAnimation { get; set; }
        int currentLimit { get; set; }
        int idleFrames { get; set; }
        int runFrames { get; set; }
        int attackFrames { get; set; }
        int deathFrames { get; set; }

        int spriteSize { get; set; }
        int size { get; set; }
        int flip { get; set; }
        int delta { get; set; }

        Image spriteSheet { get; set; }
        Rectangle hitBox { get; }

        void Update();

        bool IsMoving();

        void PlayAnimation(Graphics g);

        void SetRunAnimation();

        void SetAnimationAfterAttack();

        void SetAnimation(int currentAnimation);

        void StopEntity();

        void Attack();
    }
}
