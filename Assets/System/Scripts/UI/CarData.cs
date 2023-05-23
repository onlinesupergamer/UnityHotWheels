using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarData : ScriptableObject
{
    public Car[] cars;

    public int CarCount 
    {
        get 
        {
            return cars.Length;
        }
    }

    public Car GetCar(int index) 
    {
        return cars[index];
    }

}
