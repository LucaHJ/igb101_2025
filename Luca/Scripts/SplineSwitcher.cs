using UnityEngine;
using UnityEngine.Splines;

public class SplineSwitcher : MonoBehaviour
{
    [Header("Spline Animate Reference")]
    public SplineAnimate splineAnimate;

    [Header("Spline Paths")]
    public SplineContainer[] splinePaths;

    [Header("Settings")]
    public bool restartOnSwitch = true;
    public float startTimeOnSwitch = 0f;

    private int currentSplineIndex = 0;

    // ✅ Public read-only access to the current spline index
    public int CurrentSplineIndex => currentSplineIndex;

    // ✅ Control to block movement until user input
    private bool hasStartedMovement = false;

    void Start()
    {
        if (splineAnimate == null || splinePaths.Length == 0)
        {
            Debug.LogError("Please assign the SplineAnimate and at least one SplineContainer.");
            enabled = false;
            return;
        }

        // Set the starting spline, but do not start moving yet
        currentSplineIndex = FindNextValidSpline(-1, wantOdd: true, forward: true);
    }

    void Update()
    {
        // Wait for the first input to begin
        if (!hasStartedMovement)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                hasStartedMovement = true;
                AssignSpline(currentSplineIndex); // Start the animation when the first input happens
            }
            return; // Block Update until movement starts
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            int nextForward = FindNextValidSpline(currentSplineIndex, wantOdd: true, forward: true);
            if (nextForward != -1)
            {
                currentSplineIndex = nextForward;
                AssignSpline(currentSplineIndex);
            }
            else
            {
                // Wrap to first forward spline
                int firstForward = FindNextValidSpline(-1, wantOdd: true, forward: true);
                if (firstForward != -1)
                {
                    currentSplineIndex = firstForward;
                    AssignSpline(currentSplineIndex);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            int previousBackward = FindNextValidSpline(currentSplineIndex, wantOdd: false, forward: false);
            if (previousBackward != -1)
            {
                currentSplineIndex = previousBackward;
                AssignSpline(currentSplineIndex);
            }
            else
            {
                // Wrap to last backward spline
                int lastBackward = FindLastValidSpline(wantOdd: false);
                if (lastBackward != -1)
                {
                    currentSplineIndex = lastBackward;
                    AssignSpline(currentSplineIndex);
                }
            }
        }
    }

    private void AssignSpline(int index)
    {
        splineAnimate.Container = splinePaths[index];

        if (restartOnSwitch)
        {
            splineAnimate.NormalizedTime = startTimeOnSwitch;
            splineAnimate.Restart(true);
        }

        Debug.Log($"Switched to spline: {splinePaths[index].name} | Index: {index}");
    }

    private int FindNextValidSpline(int startIndex, bool wantOdd, bool forward)
    {
        if (forward)
        {
            for (int i = startIndex + 1; i < splinePaths.Length; i++)
            {
                if ((i % 2 != 0) == wantOdd)
                    return i;
            }
        }
        else
        {
            for (int i = startIndex - 1; i >= 0; i--)
            {
                if ((i % 2 != 0) == wantOdd)
                    return i;
            }
        }
        return -1;
    }

    private int FindLastValidSpline(bool wantOdd)
    {
        for (int i = splinePaths.Length - 1; i >= 0; i--)
        {
            if ((i % 2 != 0) == wantOdd)
                return i;
        }
        return -1;
    }
}
