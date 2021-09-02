using UnityEngine;

public class Console : MonoBehaviour
{
    [Header("Layout option")] 
    [Tooltip("How many percent of the screen is taken by console vertically")]
    [SerializeField] [Range(0, 1)] private float verticalSpaceTaken = .1f;

    private string _history;
    private string _input = "";

    private void OnGUI()
    {
        GUI.color = new Color(0, 0, 0, 50);
        _history = GUI.TextField(new Rect(0, 0, Screen.width, Screen.height * verticalSpaceTaken), _input);
    }

    private void Update()
    {
        if (_input.Contains("\n"))
        {
            _history += _input;
            _input = "";
        }
    }
}
