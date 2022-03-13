using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerRepairHelper : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject offPrefab;
    [SerializeField] private GameObject brokenPrefab;

    private Dictionary<GameObject, Vector3> helpers;

    private void Start()
    {
        helpers = new Dictionary<GameObject, Vector3>();
        Transformer.OnElectricityChanged += Transformer_OnElectricityChanged;
        Transformer.OnTransformerOff += Transformer_OnTransformerOff;
    }

    private void Update()
    {
        if (helpers.Count > 0)
        {
            foreach (GameObject go in helpers.Keys)
            {
                float distance = (helpers[go] - transform.position).sqrMagnitude;
                if (distance < 4 * radius * radius)
                {
                    go.SetActive(false);
                }
                else
                {
                    go.SetActive(true);
                    go.transform.position = transform.position + (helpers[go] - transform.position).normalized * radius;
                }
            }
        }
    }

    private void Transformer_OnTransformerOff(Vector3 position)
    {
        GameObject offed = Instantiate(offPrefab, transform.position, Quaternion.identity);
        Vector3 newPos = position;
        newPos.y = transform.position.y;
        helpers.Add(offed, newPos);
    }

    private void Transformer_OnElectricityChanged(bool electricityState)
    {
        if (!electricityState)
            return;

        helpers.Clear();
    }
}
