using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    List<Transform> spawnPoints;

    [SerializeField]
    private GameObject 
    agentprefab, 
    winScreen;

    List<GameObject> activeAgents;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new List<Transform>();
        activeAgents = new List<GameObject>();

        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
    }

    public void StartWaves()
    {
        StartCoroutine(HandleWaves());
    }

    private IEnumerator HandleWaves()
    {
        for (int stage = 0; stage <= 3; stage++)
        {
            //Debug.Log("WAVE STARTING");
            yield return StartCoroutine(Wave(stage)); // Wait for each wave to complete before starting the next
            yield return StartCoroutine(WaveCompletionHandler());
        }

        // Win condition
        StartCoroutine(Win());
    }

    private IEnumerator Wave(int stage)
    {
        yield return new WaitForSeconds(10);

        //Debug.Log("COROUTINE1 STARTING");

        for (int i = 0; i < 1 + stage * 2; i++)
        { // Wait for previous agent to spawn before spawning another
            yield return StartCoroutine(SpawnAgent(spawnPoints[Random.Range(0, spawnPoints.Count)]));
        }
    }

    private IEnumerator SpawnAgent(Transform pos)
    { // Instantiate agent at spawn point
        GameObject agent = Instantiate(agentprefab, pos.position, Quaternion.identity);
        activeAgents.Add(agent);
        
        yield return new WaitForSeconds(1);

        agent.GetComponent<AgentController>().stateMachine.ChangeState(AIStateID.Chasing);
    }

    private IEnumerator WaveCompletionHandler()
    {
        while (activeAgents.Count > 0)
        {
            for (int i = activeAgents.Count - 1; i >= 0; i--)
            {
                GameObject agent = activeAgents[i];
                if (agent == null || agent.GetComponent<AgentController>().stateMachine.currentState == AIStateID.Dead)
                {
                    activeAgents.RemoveAt(i);
                }
            }
            yield return null;
        }
    }

    private IEnumerator Win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(5);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Title Screen");
    }
}
