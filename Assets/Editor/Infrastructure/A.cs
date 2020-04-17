using Assets.Editor.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class A 
{
    public static HeartBuilder Heart()
    {
        return new HeartBuilder();
    }

    public static HeartContainerBuilder HeartContainer()
    {
        return new HeartContainerBuilder();
    }
}
