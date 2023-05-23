using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelectManager : MonoBehaviour
{
    public CarData CarData;

    public TMP_Text nameText;

    int selectedOption = 0;

    public Transform spawnTransform;

    public GameObject carObject;
    public GameObject carDummy;


    void Start()
    {
        UpdateCar(selectedOption);
    }

    public void NextCar()
    {
        selectedOption++;

        if (selectedOption >= CarData.CarCount) 
        {
            selectedOption = 0;
        }
        Destroy(carDummy);
        UpdateCar(selectedOption);
    }

    public void PreviousCar() 
    {
        selectedOption--;

        if (selectedOption < 0) 
        {
            selectedOption = CarData.CarCount - 1;
        }
        Destroy(carDummy);
        UpdateCar(selectedOption);

    }



    public void UpdateCar(int selectedOption)
    {

        Car car = CarData.GetCar(selectedOption);
        nameText.text = car.carName;

        carDummy = Instantiate(car.carDummyModel, spawnTransform.position, spawnTransform.localRotation);
    }

    public void SetCar() 
    {
        Car car = CarData.GetCar(selectedOption);

        GlobalVariables.selectedCar = car.car;

    }

}
