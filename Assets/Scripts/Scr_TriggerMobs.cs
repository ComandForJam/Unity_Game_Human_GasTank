using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerMobs : MonoBehaviour
{
    public Scr_ManagerSound _manager;
    public List<Scr_BotAi> listBotsInArea;
    List<Scr_BotAi> listForDelete;
    List<Scr_BaseHero> heroes;
    AudioSource audioSource;
    BoxCollider2D boxcollider;

    public bool isAngry = false;
    bool isChangeInFight = false;

    void Start()
    {
        listBotsInArea = new List<Scr_BotAi>();
        boxcollider = GetComponent<BoxCollider2D>();

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position,boxcollider.size, transform.rotation.eulerAngles.z, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject mob = colliders[i].gameObject;
            if (mob != null)
            {
                AddMob(mob);
            }
        }
        listForDelete = new List<Scr_BotAi>();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        heroes = new();
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(isAngry)
        {
            foreach (var item in heroes)
            {
                if (item.stateCharacter == StateCharacter.isDeath)
                {

                    heroes.Clear();
                    isChangeInFight = true;
                    break;
                }
            }
            if (heroes.Count == 0)
            {
                isAngry = false;
                foreach (var bot in listBotsInArea)
                {
                    bot.TransformTarget = null;
                }
            }
        }
        
        UpdateFight();
        RemoveMobs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scr_BaseHero hero = collision.GetComponent<Scr_BaseHero>();
        Scr_BotAi bot = collision.GetComponent<Scr_BotAi>();

        if (hero != null) {
            isChangeInFight = true;
            heroes.Add(hero);
        }  else if (bot != null)
        {
            isChangeInFight = true;
            AddMob(collision.gameObject);
        }
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        Scr_BaseHero hero = collision.GetComponent<Scr_BaseHero>();

        if (hero != null)
        {
            isChangeInFight = true;
            if (heroes.Contains(hero))
            {
                heroes.Remove(hero);
            }
        }
    }*/

    public void AddMob(GameObject mob)
    {
        Scr_BotAi child = mob.GetComponent<Scr_BotAi>();
        if (child != null && !listBotsInArea.Contains(child))
        {
            listBotsInArea.Add(child);
            child.ownerTrigger = this;
            child.colliderOwner = boxcollider;
        }
    } // ƒобавл€ет новых мобов в основной список
    public void RemoveMob(Scr_BotAi child)
    {
        listForDelete.Add(child);
    } // ¬ызываетс€ мобами, когда те погибают : заносит их в список на удаление
    void RemoveMobs()
    {
        if (listForDelete.Count != 0)
        {
            isChangeInFight = true;
            foreach (var item in listForDelete)
            {
                listBotsInArea.Remove(item);
            }
        }
        listForDelete.Clear();
    } //¬ыполн€етс€ в конце каждого цикла : когда нужно, удал€ет из основного списка умерших мобов
    void UpdateFight()
    {
        if (isChangeInFight)
        {
            UpdateAudio();
            if (heroes.Count > 1)
            {
                isAngry = true;
                

                float fear1 = heroes[0].pointsFear;
                float fear2 = heroes[1].pointsFear;

                int countOnGastank = (int)(listBotsInArea.Count * (fear1 / (fear1 + fear2)));
                for (int i = 0; i < listBotsInArea.Count; i++)
                {
                    if (i < countOnGastank)
                    {
                        listBotsInArea[i].TransformTarget = heroes[1].transform;
                    }
                    else
                    {
                        listBotsInArea[i].TransformTarget = heroes[0].transform;
                    }
                }
            } else if (heroes.Count == 1)
            {
                isAngry = true;
                
                foreach (var bot in listBotsInArea)
                {
                    bot.TransformTarget = heroes[0].transform;
                }
            } else if (heroes.Count == 0)
            {
                isAngry = false;
                foreach (var bot in listBotsInArea)
                {
                    bot.TransformTarget = null;
                }
            }
            isChangeInFight = false;
        }
    }
    public void TargetNull(Scr_BaseHero hero)
    {
        heroes.Clear();
        /*
        if (heroes.Contains(hero))
        {
            heroes.Remove(hero);
            isChangeInFight = true;
        }*/

    }
    void UpdateAudio()
    {
        if (isAngry)
        {
            audioSource.Play();
            _manager.Stop();
        }
        else
        {
            audioSource.Stop();
            _manager.Play();
        }
            
    }
}
