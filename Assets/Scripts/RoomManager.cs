using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField roomNumberInput;
    [SerializeField] private TMP_InputField tenantNameInput;
    [SerializeField] private Toggle hasPetToggle;
    [SerializeField] private Button addButton;
    [SerializeField] private Button processButton;
    [SerializeField] private Transform roomsContainer;
    [SerializeField] private GameObject roomPrefab;

    private List<RoomInfo> rooms = new List<RoomInfo>();

    private void Start()
    {
        addButton.onClick.AddListener(AddRoom);
        processButton.onClick.AddListener(ProcessData);
    }

    private void AddRoom()
    {
        int roomNumber;
        if (!int.TryParse(roomNumberInput.text, out roomNumber))
        {
            Debug.LogError("Неверное значение номера комнаты");
            return;
        }

        string tenantName = tenantNameInput.text;
        bool hasPet = hasPetToggle.isOn;

        RoomInfo newRoom = new RoomInfo { roomNumber = roomNumber, tenantName = tenantName, hasPet = hasPet };
        rooms.Add(newRoom);

        CreateRoomUI(newRoom);
    }

    private void ProcessData()
    {
        CountTenantsWithPets();
        CountTenantsNamedSmith();
        CountSingleDigitRooms();
        ReverseTenantOrder();
        RemoveRoomsDivisibleByThree();
        SwapLastPetOwnerWithSmallestRoom();
    }

    private void CountTenantsWithPets()
    {
        int count = rooms.Count(r => r.hasPet);
        Debug.Log($"Количество жильцов с животными: {count}");
    }

    private void CountTenantsNamedSmith()
    {
        int count = rooms.Count(r => r.tenantName == "Смит");
        Debug.Log($"Количество жильцов с именем \"Смит\": {count}");
    }

    private void CountSingleDigitRooms()
    {
        int count = rooms.Count(r => r.roomNumber.ToString().Length == 1);
        Debug.Log($"Количество комнат с однозначными номерами: {count}");
    }

    private void ReverseTenantOrder()
    {
        rooms.Reverse();
        UpdateRoomPanels();
        Debug.Log("Жильцы поменялись местами.");
    }

    private void RemoveRoomsDivisibleByThree()
    {
        rooms.RemoveAll(r => r.roomNumber % 3 == 0);
        UpdateRoomPanels();
        Debug.Log("Удалены комнаты, номера которых кратны трем.");
    }

    private void SwapLastPetOwnerWithSmallestRoom()
    {
        var lastPetOwner = rooms.LastOrDefault(r => r.hasPet);
        if (lastPetOwner != null)
        {
            var smallestRoom = rooms.OrderBy(r => r.roomNumber).First();
            var indexLastPetOwner = rooms.IndexOf(lastPetOwner);
            var indexSmallestRoom = rooms.IndexOf(smallestRoom);

            rooms[indexLastPetOwner] = smallestRoom;
            rooms[indexSmallestRoom] = lastPetOwner;

            UpdateRoomPanels();
            Debug.Log("Последний жилец с животным поменялся местами с жильцом в комнате с наименьшим номером.");
        }
        else
        {
            Debug.Log("Нет жильцов с животными.");
        }
    }

    private void UpdateRoomPanels()
    {
        foreach (Transform child in roomsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var room in rooms)
        {
            CreateRoomUI(room);
        }
    }

    private void CreateRoomUI(RoomInfo room)
    {
        GameObject newRoomPanel = Instantiate(roomPrefab, roomsContainer);
        TMP_Text roomNumberText = newRoomPanel.GetComponentInChildren<TMP_Text>();
        TMP_Text tenantNameText = newRoomPanel.GetComponentInChildren<TMP_Text>();
        Image petImage = newRoomPanel.GetComponentInChildren<Image>();

        roomNumberText.text = $"Комната №{room.roomNumber}";
        tenantNameText.text = room.tenantName;
        petImage.enabled = room.hasPet;
    }
}