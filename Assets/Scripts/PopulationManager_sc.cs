using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager_sc : MonoBehaviour
{

    public GameObject botPrefab;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;

    public float trialTime = 5;

    int generation = 1;
    GUIStyle guiStyle = new GUIStyle();
    void OnGUI(){
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10,10,250,150));
        GUI.Box(new Rect(0,0,140,140), "Stats", guiStyle);
        GUI.Label(new Rect(10,25, 200, 30), "Generation: " +  generation, guiStyle);
        GUI.Label(new Rect(10,25, 200, 30), "Time: {0:0.00} " +  elapsed, guiStyle);
        GUI.Label(new Rect(10,25, 200, 30), "Population: " +  population.Count, guiStyle);

        GUI.EndGroup();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<populationSize; i++){
            Vector3 startPos = new Vector3(this.transform.position.x + Random.Range(-2,2), 
                                                    this.transform.position.y,
                                                    this.transform.position.z + Random.Range(-2,2));
           GameObject bot = Instantiate(botPrefab, startPos,this.transform.rotation);
           bot.GetComponent<Brain_sc>().Init();
           population.Add(bot);
        }
        
    }

    void BreedNewPopulation(){

        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain_sc>().timeAlive).ToList();
        population.Clear();
        for(int i = ((int) (sortedList.Count/2.0f))-1;i<sortedList.Count -1; i++ ){
            population.Add(Breed(sortedList[i], sortedList[i+1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }
        for (int i = 0; i<sortedList.Count; i ++){
            Destroy(sortedList[i]);

        }
        generation ++;

       
    }

    GameObject Breed(GameObject parent1, GameObject parent2){

           Vector3 startPos = new Vector3(this.transform.position.x + Random.Range(-2,2), 
                                                    this.transform.position.y,
                                                    this.transform.position.z + Random.Range(-2,2));
           GameObject offspring = Instantiate(botPrefab, startPos,this.transform.rotation);
          Brain_sc brain_sc =  offspring.GetComponent<Brain_sc>();
           if(Random.Range(0,100) == 1){
            brain_sc.Init();
            brain_sc.dna_sc.Mutate();
           } else 
           {
            brain_sc.Init();
            brain_sc.dna_sc.Combine(parent1.GetComponent<Brain_sc>().dna_sc,
                                      parent2.GetComponent<Brain_sc>().dna_sc );
           }
           return offspring;

    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= trialTime){
            BreedNewPopulation();
            elapsed = 0;
        }
        
    }
}
