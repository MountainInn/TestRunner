using UnityEngine;
using TMPro;

public class PlayerCube : MonoBehaviour
{
    private TextMeshPro scoreLabel;

    private int _score;
    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreLabel.text = _score.ToString();
        }
    }

    void Awake()
    {
        scoreLabel = GetComponentInChildren<TextMeshPro>();

        Score = 0;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Collectible collectible))
        {
            Score += 1;
        }
    }
}
