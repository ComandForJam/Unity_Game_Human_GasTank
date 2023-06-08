using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerMobs : MonoBehaviour
{
    public List<Scr_BotAi> listBotsInArea;
    List<Scr_BotAi> listForDelete;
    public Scr_Human_GasTank _gastank;
    Scr_Human_Chainsaw _chainsaw;

    bool isChangeInFight = false;

    void Start()
    {
        listBotsInArea = new List<Scr_BotAi>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Scr_BotAi child = transform.GetChild(i).gameObject.GetComponent<Scr_BotAi>();
            if (child != null)
            {
                listBotsInArea.Add(child);
            }
        }
        listForDelete = new List<Scr_BotAi>();
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        UpdateFight();
        RemoveMobs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scr_Human_Chainsaw chainsaw = collision.GetComponent<Scr_Human_Chainsaw>();
        Scr_Human_GasTank gasTank = collision.GetComponent<Scr_Human_GasTank>();
        Scr_BotAi bot = collision.GetComponent<Scr_BotAi>();

        if (chainsaw != null) {
            isChangeInFight = true;
            _chainsaw = chainsaw;
        } else if (gasTank != null)
        {
            isChangeInFight = true;
            _gastank = gasTank;
        } else if (bot != null)
        {
            isChangeInFight = true;
            AddMob(collision.gameObject);
        }
    }

    public void AddMob(GameObject mob)
    {
        Scr_BotAi child = mob.GetComponent<Scr_BotAi>();
        if (child != null)
        {
            listBotsInArea.Add(child);
            child.ownerTrigger = this;
        }
    } // ƒобавл€ет новых мобов в основной список
    public void RemoveMob(Scr_BotAi child)
    {
        listForDelete.Add(child);
    } // ¬ызываетс€ мобами, когда те погибают : заносит их в список на удаление
    void RemoveMobs()
    {
        isChangeInFight = true;
        if (listForDelete.Count != 0)
        {
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
            bool gastankNull = _gastank == null;
            bool chainsawNull = _chainsaw == null;

            if (!gastankNull && !chainsawNull)
            {
                int fearGastank = !gastankNull ? _gastank.pointsFear : 0;
                int fearChainsaw = !chainsawNull ? _chainsaw.pointsFear : 0;

                int countOnGastank = listBotsInArea.Count * (fearChainsaw / (fearChainsaw + fearGastank));

                for (int i = 0; i < listBotsInArea.Count; i++)
                {
                    if (i < countOnGastank)
                    {
                        listBotsInArea[i]._transformTarget = _gastank.transform;
                    }
                    else
                    {
                        listBotsInArea[i]._transformTarget = _chainsaw.transform;
                    }
                }
            } else if (!gastankNull)
            {
                foreach (var bot in listBotsInArea)
                {
                    bot._transformTarget = _gastank.transform;
                }
            } else if (!chainsawNull)
            {
                foreach (var bot in listBotsInArea)
                {
                    bot._transformTarget = _chainsaw.transform;
                }
            }
            isChangeInFight = false;
        }
    }
}
