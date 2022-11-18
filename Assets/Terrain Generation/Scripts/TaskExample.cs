using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskExample : MonoBehaviour
{
    //Will be used to hold a reference to the running coroutine
    private Task task;
    
    void Start()
    {
        task = new Task(DoStuff());
        task.Finished += AfterFinish;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (task.Paused)
                task.Unpause();
            else
                task.Pause();
        }
    }

    IEnumerator DoStuff()
    {
        float time = 0f;
        while (time < 8f)
        {
            time += Time.deltaTime;
            Debug.Log("Coroutine is executing at elapsed time: " + time);
            yield return null;
        }
    }

    private void AfterFinish(bool manuallyStopped)
    {
        Debug.Log("Follow-up task");
    }
}
