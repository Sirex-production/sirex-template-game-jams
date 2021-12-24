using NaughtyAttributes;
using Support.Extensions;
using TMPro;
using UnityEngine;

namespace Support.Tests.Manual
{
    public class SpawningTextTest : MonoBehaviour
    {
        [Required]
        [SerializeField] private TMP_Text textArea;
        [SerializeField] [Min(0)] float pauseBetweenSpawningText = .01f;

        private string _initialTextAreaContent;
        private void Awake()
        {
            _initialTextAreaContent = textArea.text;
        }

        private void Start()
        {
            SpawnTextRoutine();
        }

        [Button("Spawn random text")]
        private void SpawnTextRoutine()
        {
            textArea.SetText("");
            textArea.color = Color.white;
            textArea.SpawnTextCoroutine(_initialTextAreaContent, pauseBetweenSpawningText, () => textArea.color = Color.red);
        }
    }
}