using System;

namespace Support
{
    public class GameController : MonoSingleton<GameController>
    {
        public event Action<bool> OnLevelEnded;

        public void EndLevel(bool isVictory)
        {
            OnLevelEnded?.Invoke(isVictory);
        }
    }
}