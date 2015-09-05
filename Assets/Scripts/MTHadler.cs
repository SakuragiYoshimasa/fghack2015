using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MTHandler : MonoBehaviour
{
    Queue<System.Action> queue = new Queue<System.Action>();

    /// <summary>
    /// メインスレッドに回したい処理をキューイング
    /// </summary>
    public void Enqueue(System.Action action)
    {
        queue.Enqueue(action);
    }

    void Update()
    {
        while (queue.Count > 0)
        {
            System.Action action = queue.Dequeue();
            if (action != null)
            {
                action();
            }
        }
    }
}
