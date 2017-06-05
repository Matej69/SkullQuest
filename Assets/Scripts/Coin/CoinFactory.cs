using UnityEngine;
using System.Collections;


public class CoinFactory : MonoBehaviour {

    static private CoinFactory refrence;

    public GameObject pref_coin;


    public void Awake()
    {
        refrence = this;
    }






    public static void SpawnCoins(Vector2 _origin, int _allVal)
    {
        //separate value on 3 coins
        bool spawnAnother = true;
        int numOfCoins = Random.Range(2, 5);
        int valueStep = (_allVal / numOfCoins == 0) ? 1 : _allVal / numOfCoins;
        int spawnedValues = 0;
        while (spawnAnother)
        {
            int coinVal = (spawnedValues + valueStep < _allVal) ? valueStep : (_allVal - spawnedValues);
            spawnedValues += coinVal;

            CreateCoin(_origin, coinVal);

            if (spawnedValues >= _allVal)
                spawnAnother = false;                     
        }
    }

    static private void CreateCoin(Vector2 _originPos, int _coinVal)
    {

        GameObject coin = (GameObject)Instantiate(CoinFactory.refrence.pref_coin, _originPos, Quaternion.identity);
        Vector2 targetPos = new Vector3(_originPos.x + Random.Range(-1.8f, 1.8f),_originPos.y + Random.Range(-1.8f, 1.8f), coin.transform.position.z);
        coin.GetComponent<Coin>().SetProperties(_coinVal, targetPos);
    }




}
