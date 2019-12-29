using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementView : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public float DespawnTime = 0.5f;
    public float DropTime = 0.2f;

    private int numberOfMoves;

    public void Spawn(Vector3 spawnPosition, float delay)
    {
        var color = SpriteRenderer.color;
        color.a = 0;
        SpriteRenderer.color = color;
        StartCoroutine(SpawnCoroutine(delay));
        StartCoroutine(MoveCoroutine(spawnPosition, delay));
        numberOfMoves++;
    }

    public void Move(Vector3 newPosition, float delay)
    {
        delay += DropTime * numberOfMoves;
        StartCoroutine(MoveCoroutine(newPosition, delay));
        numberOfMoves++;
    }

    public void Despawn()
    {
        StartCoroutine(DespawnCoroutine());
    }

    private IEnumerator SpawnCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        var color = SpriteRenderer.color;
        color.a = 1;
        SpriteRenderer.color = color;
    }

    private IEnumerator MoveCoroutine(Vector3 newPosition, float delay)
    {
        yield return new WaitForSeconds(delay);

        var time = DropTime;

        var startPosition = transform.position;

        do
        {
            var t = time / DropTime;
            var position = Vector3.Lerp(newPosition, startPosition, t);
            transform.position = position;
            yield return null;
        } while ((time -= Time.deltaTime) > 0);

        transform.position = newPosition;
        numberOfMoves--;
    }

    private IEnumerator DespawnCoroutine()
    {
        var time = DespawnTime;

        do
        {
            var t = time / DespawnTime;
            var color = SpriteRenderer.color;
            color.a = Mathf.Lerp(0, 1, t);
            SpriteRenderer.color = color;
            yield return null;
        } while ((time -= Time.deltaTime) > 0);
        Destroy(gameObject);
    }
}
