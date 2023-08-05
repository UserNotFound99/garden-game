using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTab : MonoBehaviour
{
    [System.NonSerialized] public Garden garden;
    public bool shown = false;
    [SerializeField] Vector2 displacement;
    protected Vector2 onCoords, offCoords;
    public int result = -1;

    void Start()
    {
        garden = FindObjectOfType<Garden>();
        onCoords = (Vector2)transform.position;
        offCoords = onCoords + displacement;
        transform.position = offCoords;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Toggle()
    {
        shown = !shown;
        StartCoroutine(Move(shown ? onCoords : offCoords, 1f));
    }

    public void Up()
    {
        if (!shown) StartCoroutine(Move(onCoords, 1f));
        shown = true;
    }

    public void Down()
    {
        if (shown) StartCoroutine(Move(offCoords, 1f));
        shown = false;
    }

    public IEnumerator Move(Vector2 end, float seconds)
    {
        result = -1;
        float elapsedTime = 0;
        Vector2 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector2.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
    }
}
