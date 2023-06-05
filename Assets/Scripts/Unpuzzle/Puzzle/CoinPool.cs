using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    [SerializeField]
    private CoinReward coinPrefab;
    private int poolSize ;
    private List<CoinReward> coinPool;
    private void Start()
    {
        coinPool = new List<CoinReward>();

        for (int i = 0; i < poolSize; i++)
        {
            CoinReward coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.gameObject.SetActive(false);
            coinPool.Add(coin);
        }
    }
    public void Init(int poolSize/*,Action<int>setCoin*/)
    {
        this.poolSize = poolSize;
    }
    public CoinReward GetPooledCoin()
    {
        foreach (CoinReward coin in coinPool)
        {
            if (!coin.gameObject.activeInHierarchy)
            {
                coin.gameObject.SetActive(true);
                return coin;
            }
        }
        CoinReward newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        newCoin.gameObject.SetActive(true);
        coinPool.Add(newCoin);
        return newCoin;
    }
}
