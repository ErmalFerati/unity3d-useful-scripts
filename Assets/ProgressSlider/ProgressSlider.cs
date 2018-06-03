using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    public static ProgressSlider instance;

    //The current level text gameobject
    [SerializeField]
    private Text currentLevelText;

    //The next level text gameobject
    [SerializeField]
    private Text nextLevelText;

    //The slider used to show the progress
    [SerializeField]
    private Slider progressSlider;

    public float percentageCompleted
    {
        get
        {
            return (currentProgress / maximumProgress) * 100f;
        }
    }

    //The current point of progress of the player
    public float currentProgress;

    //The maximum point of progress of the player
    public float maximumProgress;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// The update method, works when the instance variables are used
    /// </summary>
    public void updateProgressBar()
    {
        progressSlider.value = currentProgress / maximumProgress;
    }

    /// <summary>
    /// The update method, works independently from the instance variables
    /// </summary>
    /// <param name="currentProgress">The current point of progress of the player</param>
    /// <param name="maximumProgress">The maximum point of progress of the player</param>
    public void updateProgressBar(float currentProgress, float maximumProgress)
    {
        progressSlider.value = currentProgress / maximumProgress;
    }
}