using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    private GameObject gameManager;
    //private GameManager gameManagerScript;

    [SetUp]
    public void Setup()
    {
        GameObject gameManagerPrefab = Resources.Load<GameObject>("Prefabs/GameManager");
        gameManager = MonoBehaviour.Instantiate(gameManagerPrefab);
        //GameManager gameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(gameManager);
    }

    /*
    [UnityTest]
    public IEnumerator PlayerShipTurnsLeft()
    {
        yield return null;
    }
    */
}
