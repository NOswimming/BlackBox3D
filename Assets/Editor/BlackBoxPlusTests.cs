using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using BlackBoxPlus;
using Ray = BlackBoxPlus.Ray;

public class BlackBoxPlusTests
{
    [Test]
    public void FireAllRays_NoAtoms()
    {
        // craete new Blackbox 1x1x1
        var blackBox = new BlackBox(new VectorInt3(1, 1, 1));
        // send rays from all angles

        // verify all rays pass straight through

    }

    [Test]
    public void GetImmediateGridForRay_NoAtoms()
    {
        int[,] expectedImmediateGrid = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        // Create new Blackbox 3x3x3
        var blackBox = new BlackBox(new VectorInt3(3, 3, 3));

        // Send rays from centre edge [1, 1, 0] to GetImmediateGridForRay
        var immediateGrid = blackBox.GetImmediateGridForRay(new Ray(new VectorInt3(1, 1, -1), VectorInt3.FORWARD));

        // Verify immediate grid is correct
        Assert.IsTrue(ArraysEqual(expectedImmediateGrid, immediateGrid));

    }

    [Test]
    public void GetImmediateGridForRay_WithAtoms()
    {
        int[,] expectedImmediateGrid = { { 0, 1, 0 }, { 1, 0, 0 }, { 0, 0, 0 } };
        // Create new Blackbox 3x3x3
        var blackBox = new BlackBox(new VectorInt3(3, 3, 3));
        // Place atoms in the blackbox
        blackBox.AddAtom(new VectorInt3(1, 0, 0));
        blackBox.AddAtom(new VectorInt3(0, 1, 0));
        blackBox.AddAtom(new VectorInt3(0, 0, 1));

        // Send rays from centre edge [1, 1, 0] to GetImmediateGridForRay
        var immediateGrid = blackBox.GetImmediateGridForRay(new Ray(new VectorInt3(1, 1, -1), VectorInt3.FORWARD));
            
        // Verify immediate grid is correct
        Assert.IsTrue(ArraysEqual(expectedImmediateGrid, immediateGrid));

    }

    bool ArraysEqual<T>(T[,] array1, T[,] array2)
    {
        return array1.Rank == array2.Rank
            && Enumerable.Range(0, array1.Rank).All(dimension => array1.GetLength(dimension) == array2.GetLength(dimension)) 
            && array1.Cast<T>().SequenceEqual(array2.Cast<T>());
    }
}
