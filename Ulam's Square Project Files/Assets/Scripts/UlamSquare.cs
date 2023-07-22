using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UlamSquare : MonoBehaviour
{
    public GameObject sphere, primeSphere;
    public TextMeshProUGUI totalText, primesText;

    float camZ = -400f;
    int count = 1;
    int primes = 0;
    int numSpheresInDirection = 1;
    int numCount = 0;
    Vector3 currentPos = new Vector3(0f, 0f, 0f);
    Vector3[] directionVectors = new Vector3[]
    {
      new Vector3(2.2f, 0f, 0f), // Right
      new Vector3(0f, 2.2f, 0f), // Up
      new Vector3(-2.2f, 0f, 0f), // Left
      new Vector3(0f, -2.2f, 0f) // Down
    };

    private void Start()
    {
      StartCoroutine(GenerateSquare());
    }

    private void Update()
    {
        totalText.text = "Total Numbers " + count.ToString();
        primesText.text = "Primes " + primes.ToString();

      if (Input.GetKey(KeyCode.Mouse0))
      {
        camZ += 100f * Time.deltaTime;
        Camera.main.transform.position = new Vector3(0f, 0f, camZ);
      }

      else if (Input.GetKey(KeyCode.Mouse1))
      {
        camZ -= 100f * Time.deltaTime;
        Camera.main.transform.position = new Vector3(0f, 0f, camZ);
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
        Application.Quit();
      }
    }

    public IEnumerator GenerateSquare()
    {
      for (int i = 0; i < directionVectors.Length; i++) // Step through directions, looping back
      {
        for (int j = 0; j < numSpheresInDirection; j++) // Each direction should have an increasing amount of spheres
        {
          currentPos += directionVectors[i];

          if (IsPrime(count) == true)
          {
            Instantiate(primeSphere, currentPos, Quaternion.identity);
            primes++;
          }
          else
          {
            Instantiate(sphere, currentPos, Quaternion.identity);
          }

          yield return new WaitForSeconds(0f);
          count++;
        }

        numCount++;

        if (numCount == 2)
        {
          numSpheresInDirection++;
          numCount = 0;
        }

        if (i == directionVectors.Length - 1)
        {
          i = -1;
        }
      }
    }

    public static bool IsPrime(int number)
    {
      if (number <= 1) return false;
      if (number == 2) return true;
      if (number % 2 == 0) return false;

      var boundary = (int)System.Math.Floor(System.Math.Sqrt(number));

      for (int i = 3; i <= boundary; i += 2)
      {
        if (number % i == 0)
        {
          return false;
        }
      }

      return true;
    }
}
