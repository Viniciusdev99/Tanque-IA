using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }
    public void GoToHeli()
    {
        //Passa ao m�todo os pontos atuais e alvo para mover o agente [1]
        g.AStar(currentNode, wps[1]);
        //Zera o contador de movimento
        currentWP = 0;
    }
    //M�todo para se mover ao ponto ruina
    public void GoToRuin()
    {
        //Passa ao m�todo os pontos atuais e alvo para mover o agente [6]
        g.AStar(currentNode, wps[6]);
        //Zera o contador de movimento
        currentWP = 0;
    }

    public void GoToPlant()
    {
        //Passa ao m�todo os pontos atuais e alvo para mover o agente [9]
        g.AStar(currentNode, wps[9]);
        //Zera o contador de movimento
        currentWP = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        //Define o node atual
        currentNode = g.getPathPoint(currentWP);
        //Se estiver pr�ximo o bastante do n� o tanque se mover� para o pr�ximo
        if (Vector3.Distance(
        g.getPathPoint(currentWP).transform.position,
        transform.position) < accuracy)
        {
            currentWP++;
        }

        //Enquanto o valor do node atual for menor que a quantidade de nodes no caminho, move o tanque
        if (currentWP < g.getPathLength())
        {
            //Define proximo ponto alvo do movimento
            goal = g.getPathPoint(currentWP).transform;
            //Aloca pr�ximo ponto em um vetor
            Vector3 lookAtGoal = new Vector3(goal.position.x,
            this.transform.position.y,
            goal.position.z);
            //Utiliza o vetor para rotacionar em dire��o ao alvo
            Vector3 direction = lookAtGoal - this.transform.position;
            //Rotaciona e move o objeto
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            rotSpeed * Time.deltaTime);
        }
    }
}