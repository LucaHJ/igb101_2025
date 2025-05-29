using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoTriggerOnSpline : MonoBehaviour
{
    [Header("Video Settings")]
    public VideoPlayer videoPlayer;          // Assign in inspector
    public SplineSwitcher splineSwitcher;    // Reference to your spline controller
    public int[] triggerSplineIndices;        // Array of spline indices that trigger video
    public float startDelay = 0f;              // Delay in seconds before video starts

    private bool hasTriggered = false;         // To prevent multiple triggers
    private Coroutine playDelayCoroutine = null;

    void Update()
    {
        if (splineSwitcher == null || videoPlayer == null)
            return;

        if (IsSplineTriggerMatch(splineSwitcher.CurrentSplineIndex))
        {
            if (!hasTriggered)
            {
                hasTriggered = true;

                if (playDelayCoroutine != null)
                    StopCoroutine(playDelayCoroutine);

                playDelayCoroutine = StartCoroutine(DelayedPlay());
            }
        }
        else
        {
            hasTriggered = false; // Reset so it can be triggered again if needed
        }
    }

    private bool IsSplineTriggerMatch(int splineIndex)
    {
        foreach (int triggerIndex in triggerSplineIndices)
        {
            if (splineIndex == triggerIndex)
                return true;
        }
        return false;
    }

    private IEnumerator DelayedPlay()
    {
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        videoPlayer.Play();
        Debug.Log($"Video started after {startDelay} seconds delay because spline {splineSwitcher.CurrentSplineIndex} was activated.");
    }
}
