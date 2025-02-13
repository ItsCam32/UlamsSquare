using UnityEngine;
using TMPro;
using System;

public class UlamSquare : MonoBehaviour
{
    // vv Private Exposed vv //

    [Range(100, 1000000)]
    [SerializeField]
    private int iterations;

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private GameObject quadPrefab;

    [SerializeField]
    private TextMeshProUGUI numbersText;

    [SerializeField]
    private TextMeshProUGUI primesText;

    // vv Private vv //

    private Vector3[] directionVectors = new Vector3[]
    {
        new Vector3(1.0f, 0.0f, 0.0f), // Right
        new Vector3(0.0f, 1.0f, 0.0f), // Up
        new Vector3(-1.0f, 0.0f, 0.0f), // Left
        new Vector3(0.0f, -1.0f, 0.0f) // Down
    };

    ////////////////////////////////////////

    #region Private Functions

    private void Start()
    {
        Simulate();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (cam.position.z < 0.0f)
            {
                cam.position += new Vector3(0.0f, 0.0f, 500.0f * Time.deltaTime);
            }
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (cam.position.z > -700.0f)
            {
                cam.position -= new Vector3(0.0f, 0.0f, 500.0f * Time.deltaTime);
            }
        }
    }

    private void Simulate()
    {
        int num = 0;
        int primes = 0;
        int RequiredIterationsForDirection = 1;
        int IterationsInDirection = 0;
        int directionChangeCount = 0;
        int directionIndex = 0;
        Vector3 currentPos = directionVectors[0];

        for (int i = 0; i < iterations; i++)
        {
            num++;

            // Spawn if prime
            if (IsPrime(num))
            {
                MeshRenderer renderer = Instantiate(quadPrefab, currentPos, Quaternion.identity).GetComponent<MeshRenderer>();
                primes++;
            }

            // If we now need to change direction
            IterationsInDirection++;
            if (IterationsInDirection == RequiredIterationsForDirection)
            {
                IterationsInDirection = 0;

                // Every other time we change direction, the side of the square gets longer, so add 1 to required iterations
                directionChangeCount++;
                if (directionChangeCount == 2)
                {
                    RequiredIterationsForDirection++;
                    directionChangeCount = 0;
                }

                // Next direction
                directionIndex++;
                if (directionIndex == directionVectors.Length)
                {
                    directionIndex = 0;
                }
            }

            currentPos += directionVectors[directionIndex];
        }

        numbersText.text = $"Numbers {num.ToString()}";
        primesText.text = $"Primes {primes.ToString()}";
    }

    private bool IsPrime(int num)
    {
        if (num < 2) return false;
        if (num == 2 || num == 3) return true;
        if (num % 2 == 0 || num % 3 == 0) return false;

        int limit = (int)Mathf.Sqrt(num);
        for (int i = 5; i <= limit; i += 6)
        {
            if (num % i == 0 || num % (i + 2) == 0)
                return false;
        }
        return true;
    }
    #endregion

    #region Public Functions

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    #endregion
}
