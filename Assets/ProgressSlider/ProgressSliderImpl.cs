using System.Collections;
using UnityEngine;

public class ProgressSliderImpl : MonoBehaviour
{
    private float requiredScore = 1000;

    private IEnumerator Start()
    {
        ProgressSlider progressSlider = ProgressSlider.instance;
        progressSlider.maximumProgress = requiredScore;

        for (int i = 0; i < requiredScore; i += 2)
        {
            yield return new WaitForEndOfFrame();
            progressSlider.currentProgress = i;
            progressSlider.updateProgressBar();
            Debug.Log("Player completed " + progressSlider.percentageCompleted + "% of the level.");
        }
    }
}