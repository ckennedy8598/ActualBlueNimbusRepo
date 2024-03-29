using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    /// <summary>
    /// This is the script for the Archer enemy AI. I'll clean this up and add headers when I return, but for now
    /// this should be good for the FFP. 
    /// </summary>
    [Header("Animator Reference")]
    public Animator anim;

    public GameObject Arrow;
    public Transform ArrowPos;
    private GameObject player;

    private float timer;
    [SerializeField] private float shootInterval = 2;
    [SerializeField] private float sightRange = 4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        if (gameObject.GetComponent<Enemy>().currentHealth <= 0)
        {
            return;
        }

        if(player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < sightRange)
        {
            timer += Time.deltaTime;

            if (timer > shootInterval)
            {
                timer = 0;
                StartCoroutine(shootAnim());
                shoot();
            }
        }
        
    }
    private void shoot()
    {
        Instantiate(Arrow, ArrowPos.position, Quaternion.identity);
    }

    private IEnumerator shootAnim()
    {
        anim.SetTrigger("Attack");
        //anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1.0f);
    }
}
