using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class EnemyPlacementManager : MonoBehaviour
{
    public static Dictionary<DIRECTIONS, Vector3> placements = new Dictionary<DIRECTIONS, Vector3>()
    {
        [DIRECTIONS.UP] = new Vector3(0, 0, 5),
        [DIRECTIONS.DOWN] = new Vector3(0, 0, -5),
        [DIRECTIONS.LEFT] = new Vector3(-5, 0, 0),
        [DIRECTIONS.RIGHT] = new Vector3(5, 0, 0)
    };

    [SerializeField] int amount = 10;
    [SerializeField] float multiplier = 1.5f;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Object[] materials;
    [SerializeField] string materialFolder;
    [SerializeField] LevelManager levelManager;

    float currentMultiplier = 1f;


    // Start is called before the first frame update
    void Start()
    {
        materials = Resources.LoadAll(materialFolder, typeof(Material));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Material m = materials[Random.Range(0, materials.Length)] as Material;
            for (int i=0; i < amount; i++)
            {
                GameObject g =Instantiate(enemies[Random.Range(0, enemies.Length)], WFCMap2.entryPoints[Random.Range(0, WFCMap2.entryPoints.Count)], Quaternion.identity);
                g.GetComponent<AIMoveTowardsTarget>().setTarget(WFCMap2.target);
                g.transform.position = new Vector3(g.transform.position.x,g.GetComponent<AIMoveTowardsTarget>().getY(), g.transform.position.z);
                g.GetComponent<MaterialManager>().addMaterial(m, false, false);
                g.GetComponent<Enemy>().setLevelManager(levelManager);
                g.GetComponent<Enemy>().setDamage(g.GetComponent<Enemy>().getDamage() * currentMultiplier);
                g.GetComponent<Enemy>().setHealth(g.GetComponent<Enemy>().getHealth() * currentMultiplier);
                g.GetComponent<Enemy>().setWorth(Mathf.RoundToInt(g.GetComponent<Enemy>().getWorth() * currentMultiplier));
            }
        }
    }

    public void setAmount(int i)
    {
        amount = i;
    }

    public int getAmount()
    {
        return amount;
    }

    public void increaseMultiplier()
    {
        currentMultiplier *= multiplier;
    }
}
