using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using BlackBoxPlus;

public class VectorInt3Tests
{
    [Test]
    public void CrossProductRelativeDirections_Forward()
    {
        // Create forward Vector
        var forward = VectorInt3.FORWARD;

        // Calculate cross products to get the relative direction
        var UpX = VectorInt3.CrossProduct(forward, VectorInt3.UP);
        var DownX = VectorInt3.CrossProduct(forward, VectorInt3.DOWN);
        var RightX = VectorInt3.CrossProduct(forward, VectorInt3.RIGHT);
        var LeftX = VectorInt3.CrossProduct(forward, VectorInt3.LEFT);
        var ForwardX = VectorInt3.CrossProduct(forward, VectorInt3.FORWARD);
        var BackwardX = VectorInt3.CrossProduct(forward, VectorInt3.BACKWARD);

        // Verify relative directions are correct
        Assert.AreEqual(VectorInt3.LEFT, UpX);
        Assert.AreEqual(VectorInt3.RIGHT, DownX);
        Assert.AreEqual(VectorInt3.UP, RightX);
        Assert.AreEqual(VectorInt3.DOWN, LeftX);
        Assert.AreEqual(VectorInt3.ZERO, ForwardX);
        Assert.AreEqual(VectorInt3.ZERO, BackwardX);
    }

    [Test]
    public void CrossProductRelativeDirections_Backward()
    {
        // Create forward Vector
        var backward = VectorInt3.BACKWARD;

        // Calculate cross products to get the relative direction
        var UpX = VectorInt3.CrossProduct(backward, VectorInt3.UP);
        var DownX = VectorInt3.CrossProduct(backward, VectorInt3.DOWN);
        var RightX = VectorInt3.CrossProduct(backward, VectorInt3.RIGHT);
        var LeftX = VectorInt3.CrossProduct(backward, VectorInt3.LEFT);
        var ForwardX = VectorInt3.CrossProduct(backward, VectorInt3.FORWARD);
        var BackwardX = VectorInt3.CrossProduct(backward, VectorInt3.BACKWARD);

        // Verify relative directions are correct
        Assert.AreEqual(VectorInt3.RIGHT, UpX);
        Assert.AreEqual(VectorInt3.LEFT, DownX);
        Assert.AreEqual(VectorInt3.DOWN, RightX);
        Assert.AreEqual(VectorInt3.UP, LeftX);
        Assert.AreEqual(VectorInt3.ZERO, ForwardX);
        Assert.AreEqual(VectorInt3.ZERO, BackwardX);
    }

    [Test]
    public void CrossProductRelativeDirections_Up()
    {
        // Create forward Vector
        var up = VectorInt3.UP;

        // Calculate cross products to get the relative direction
        var UpX = VectorInt3.CrossProduct(up, VectorInt3.UP);
        var DownX = VectorInt3.CrossProduct(up, VectorInt3.DOWN);
        var RightX = VectorInt3.CrossProduct(up, VectorInt3.RIGHT);
        var LeftX = VectorInt3.CrossProduct(up, VectorInt3.LEFT);
        var ForwardX = VectorInt3.CrossProduct(up, VectorInt3.FORWARD);
        var BackwardX = VectorInt3.CrossProduct(up, VectorInt3.BACKWARD);

        // Verify relative directions are correct
        Assert.AreEqual(VectorInt3.ZERO, UpX);
        Assert.AreEqual(VectorInt3.ZERO, DownX);
        Assert.AreEqual(VectorInt3.BACKWARD, RightX);
        Assert.AreEqual(VectorInt3.FORWARD, LeftX);
        Assert.AreEqual(VectorInt3.RIGHT, ForwardX);
        Assert.AreEqual(VectorInt3.LEFT, BackwardX);
    }
}
