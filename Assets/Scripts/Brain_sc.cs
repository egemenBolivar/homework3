using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Brain_sc : MonoBehaviour
{



    public int DNALength = 1;

    public float timeAlive;

    public DNA_sc dna_sc;

    private ThirdPersonCharacter m_Character;

    private Vector3 m_Move;

    private bool m_Jump;

    bool isAlive = true;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "dead")
        {
            isAlive = false;
        }

    }

    public void Init()
    {
        //initialise DNA
        //0 forward
        //1 back
        //1/2 left
        //1/3 right
        //1/4 jump
        //5 crouch
        
        dna_sc = new DNA_sc(DNALength, 6);
        m_Character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        isAlive = true;
    }

    void FixedUpdate(){
        float horizontal = 0;
        float vertical = 0;
        bool crouch = false;
        if(dna_sc.GetGene(0)==0)
            vertical = 1;
            else if(dna_sc.GetGene(0)==1)
                vertical = -1;
                else if(dna_sc.GetGene(0)==3)
                    horizontal = 1;
                    else if (dna_sc.GetGene(0)==4)
                        m_Jump = true;
                        else if(dna_sc.GetGene(0)==5)
                            crouch = true;

                            m_Move = Vector3.forward * vertical + Vector3.right * horizontal;
                            m_Character.Move(m_Move, crouch, m_Jump);
                            m_Jump = false;
                            if(isAlive)
                            timeAlive += Time.deltaTime;


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
