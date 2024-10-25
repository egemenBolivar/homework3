using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_sc : MonoBehaviour
{

    List<int> genes = new List<int>();
    int dnaLength;
    int maxValues;

    public DNA_sc(int length, int max){
        dnaLength = length;
        maxValues = max;
        genes.Clear();
        for(int i = 0; i<dnaLength; i++){
            genes.Add(Random.Range(0,maxValues));
        }
    }

    public void Combine(DNA_sc dna1, DNA_sc dna2){
        for (int i=0; i<dnaLength; i++) {
            if(i<dnaLength/2.0)
            {
                genes[i]= dna1.genes[i];
            }
            else{
                genes[i]= dna2.genes[i];
            }
        }
    }

    public void Mutate(){
        genes[Random.Range(0,dnaLength)]= Random.Range(0,maxValues);
    }

    public void SetGene(int pos, int value){
        genes[pos] = value;
    }

    public int GetGene(int pos){
        return genes[pos];
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
