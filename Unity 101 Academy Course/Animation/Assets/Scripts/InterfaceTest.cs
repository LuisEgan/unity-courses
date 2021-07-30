using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceTest : MonoBehaviour, ITestInterface
{
    private IEnumerator test;

    // Start is called before the first frame update
    void Start()
    {
        test = TestEnumarator();

        test.MoveNext();
//        print(test.Current);
//        print(test.Current);

        test.MoveNext();
        print(test.Current);

        test.Reset();

        while (test.MoveNext())
        {
            print(test.Current);
        }

        test.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (test.Current is string)
        {
//            print(test.Current);
            test.MoveNext();
        }
        else if (test.Current is int)
        {
//            print(test.Current);
        }
    }

    IEnumerator TestEnumarator()
    {
        yield return ":p";
        yield return 4;
        yield return "aayye";
    }

    public void DoSomething()
    {
        // when inheriting from an interface, ALL of it's methods must be implements (declared)
    }
}