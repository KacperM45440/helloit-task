using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;

public class ToastSpawner : MonoBehaviour
{
    [SerializeField] private Transform breadParent;
    [SerializeField] private Transform secondBreadParent;
    [SerializeField] private Transform particleParent;

    private Vector3 toasterLeftPos = new Vector3(-13.4589996f, 5.27780819f, -7.93316507f);
    private Vector3 toasterRightPos = new Vector3(-13.3520002f, 5.27780819f, -8.30700016f);
    private Dictionary<int, Vector3> spawnPositions;
    private IEnumerator ToastRoutine = null;

    private void Start()
    {
        PrepareSpawnPoints();
    }

    public void PopToasts(int amount, float time)
    {
        ToastRoutine = ToastCoroutine(amount, time);
        StartCoroutine(ToastRoutine);
    }

    public void ShootToast(int toastID, bool toastType)
    {
        Vector3 position;

        if (toastID % 2 == 0)
        {
            position = toasterLeftPos;
        }
        else
        {
            position = toasterRightPos;
        }

        GameObject selectedToast = secondBreadParent.GetChild(toastID).gameObject;
        selectedToast.GetComponent<ShootableToastScript>().Surface(toastType, position);
    }

    private void ReadyToast(int toastID, Vector3 position, bool toastType)
    {
        GameObject selectedToast = breadParent.GetChild(toastID).gameObject;
        selectedToast.GetComponent<ToastScript>().SpawnToast(toastType, position);
        selectedToast.SetActive(true);
    }

    private void ReadyParticle(int particleID, Vector3 position, bool toastType)
    {
        GameObject selectedParticle = particleParent.GetChild(particleID).gameObject;
        selectedParticle.GetComponent<ParticleScript>().SpawnParticle(toastType, position);
        selectedParticle.SetActive(true);
    }

    private List<Vector3> GetToastPositions(int positionAmount)
    {
        if (positionAmount > spawnPositions.Count)
        {
            return null;
        }

        List<Vector3> positions = new(spawnPositions.Values);

        for (int i = positions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (positions[j], positions[i]) = (positions[i], positions[j]);
        }

        return positions.GetRange(0, positionAmount);
    }

    private void PrepareSpawnPoints()
    {
        spawnPositions = new()
        {
            { 0, new Vector3(8.3f, 20f, 9f) },
            { 1, new Vector3(8.3f, 20f, 6f) },
            { 2, new Vector3(8.3f, 20f, 3f) },
            { 3, new Vector3(8.3f, 20f, 0f) },
            { 4, new Vector3(8.3f, 20f, -3f) },
            { 5, new Vector3(8.3f, 20f, -6f) },
            { 6, new Vector3(8.3f, 20f, -9f) },
            { 7, new Vector3(8.3f, 20f, -12f) },

            { 8, new Vector3(5f, 20f, 9f) },
            { 9, new Vector3(5f, 20f, 6f) },
            { 10, new Vector3(5f, 20f, 3f) },
            { 11, new Vector3(5f, 20f, 0f) },
            { 12, new Vector3(5f, 20f, -3f) },
            { 13, new Vector3(5f, 20f, -6f) },
            { 14, new Vector3(5f, 20f, -9f) },
            { 15, new Vector3(5f, 20f, -12f) },

            { 16, new Vector3(1.7f, 20f, 9f) },
            { 17, new Vector3(1.7f, 20f, 6f) },
            { 18, new Vector3(1.7f, 20f, 3f) },
            { 19, new Vector3(1.7f, 20f, 0f) },
            { 20, new Vector3(1.7f, 20f, -3f) },
            { 21, new Vector3(1.7f, 20f, -6f) },
            { 22, new Vector3(1.7f, 20f, -9f) },
            { 23, new Vector3(1.7f, 20f, -12f) },

            { 24, new Vector3(-1.7f, 20f, 9f) },
            { 25, new Vector3(-1.7f, 20f, 6f) },
            { 26, new Vector3(-1.7f, 20f, 3f) },
            { 27, new Vector3(-1.7f, 20f, 0f) },
            { 28, new Vector3(-1.7f, 20f, -3f) },
            { 29, new Vector3(-1.7f, 20f, -6f) },
            { 30, new Vector3(-1.7f, 20f, -9f) },
            { 31, new Vector3(-1.7f, 20f, -12f) },

            { 32, new Vector3(-5f, 20f, 9f) },
            { 33, new Vector3(-5f, 20f, 6f) },
            { 34, new Vector3(-5f, 20f, 3f) },
            { 35, new Vector3(-5f, 20f, 0f) },
            { 36, new Vector3(-5f, 20f, -3f) },
            { 37, new Vector3(-5f, 20f, -6f) },
            { 38, new Vector3(-5f, 20f, -9f) },
            { 39, new Vector3(-5f, 20f, -12f) },

            { 40, new Vector3(-8.3f, 20f, 9f) },
            { 41, new Vector3(-8.3f, 20f, 6f) },
            { 42, new Vector3(-8.3f, 20f, 3f) },
            { 43, new Vector3(-8.3f, 20f, 0f) },
            { 44, new Vector3(-8.3f, 20f, -3f) },
            { 45, new Vector3(-8.3f, 20f, -6f) },
            { 46, new Vector3(-8.3f, 20f, -9f) },
            { 47, new Vector3(-8.3f, 20f, -12f) },
        };
    }

    public bool IsToastRoutineNull()
    {
        return ToastRoutine == null;
    }

    private IEnumerator ToastCoroutine(int amount, float time)
    {
        List<Vector3> toastsToSpawn = GetToastPositions(amount);
        
        for (int i = 0; i < amount; i++)
        {
            bool toastType;
            int toastChance = Random.Range(0, 101);

            if (toastChance <= 90)
            {
                toastType = true;
            }
            else
            {
                toastType = false;
            }

            yield return new WaitForSeconds(time / amount);
            ShootToast(i, toastType);
            ReadyToast(i, toastsToSpawn[i], toastType);
            ReadyParticle(i, toastsToSpawn[i], toastType);
        }

        ToastRoutine = null;
    }

    public void ResetScene()
    {
        foreach (Transform t in breadParent)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in secondBreadParent)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in particleParent)
        {
            t.gameObject.SetActive(false);
        }

        if (ToastRoutine != null)
        {
            StopCoroutine(ToastRoutine);
        }
        
        ToastRoutine = null;
    }

}
