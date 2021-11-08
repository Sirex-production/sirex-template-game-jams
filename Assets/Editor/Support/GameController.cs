using System;

namespace Support
{
    /// <summary>
    /// Class that manages general game logic
    /// </summary>
    public class GameController : MonoSingleton<GameController>
    {
        public event Action<bool> OnLevelEnded;

        public void EndLevel(bool isVictory)
        {
            OnLevelEnded?.Invoke(isVictory);
        }
    }
}