using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleFlowers : MonoBehaviour
{
    [SerializeField] GameObject SelectField;
    List<SelectOnTime> allselectors = new List<SelectOnTime>();

    private List<FlowerNumber> children = new List<FlowerNumber>();

    [SerializeField] float switchtingSpeed = 2;

    [SerializeField] bool ThreeDMode = true;

    [SerializeField] float WaitTillShuffel = 12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(SelectOnTime child in SelectField.GetComponentsInChildren<SelectOnTime>())
        {
            allselectors.Add(child);
        }
        foreach (FlowerNumber child in GetComponentsInChildren<FlowerNumber>())
        {
            children.Add(child);
        }
        StartCoroutine(SwitchTreeTimes());
    }
    IEnumerator SwitchTreeTimes()
    {
        yield return new WaitForSeconds(WaitTillShuffel);


        foreach (FlowerNumber child in children)
        {
            if (ThreeDMode)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                child.GetComponent<SpriteRenderer>().color = Color.white;
            }
           
        }
        for(int i = 0; i < 3; i++)
        {
            SwitchPositions();
            yield return new WaitForSeconds(switchtingSpeed*1.1f);
        }
    }

    public void SwitchPositions()
    {
        List<(int index, Vector3 position)> childPositions = new List<(int, Vector3)>();
        for (int i = 0; i < children.Count; i++)
        {
            childPositions.Add((children[i].transform.GetSiblingIndex(), children[i].gameObject.transform.position));
        }

        ShuffleList(childPositions);


        for (int i = 0; i < children.Count; i++)
        {
            (int index, Vector3 position) target = childPositions[i];

            children[i].gameObject.transform.SetSiblingIndex(target.index);

            StartCoroutine(MoveToPosition(children[i].gameObject.transform, target.position, switchtingSpeed));
        }
        UpdatingSelectIndexs();
    }


    private IEnumerator MoveToPosition(Transform child, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = child.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            child.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        child.position = targetPosition; // Ensure it ends exactly at the target position
    }
    private void ShuffleList(List<(int index, Vector3 position)> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            var temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    private void UpdatingSelectIndexs()
    {
        foreach (FlowerNumber child in children)
        {
            allselectors[child.transform.GetSiblingIndex()].SetValue((int)child.Flower);
        }
    }
}
