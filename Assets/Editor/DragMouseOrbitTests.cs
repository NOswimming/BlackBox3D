using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using BlackBoxPlus;

public class DragMouseOrbitTests
{

    [Test]
    public void DragMouseOrbit_SnapTo90()
    {
        Assert.AreEqual(90, DragMouseOrbit.SnapTo90(45));
        Assert.AreEqual(90, DragMouseOrbit.SnapTo90(90));

        Assert.AreEqual(0, DragMouseOrbit.SnapTo90(0));
        Assert.AreEqual(0, DragMouseOrbit.SnapTo90(44));

        
        Assert.AreEqual(0, DragMouseOrbit.SnapTo90(-44));
        Assert.AreEqual(-90, DragMouseOrbit.SnapTo90(-90));

        Assert.AreEqual(-90, DragMouseOrbit.SnapTo90(-46));
    }
}
