using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using Newtonsoft.Json;
using System.IO;

namespace Api.DBManagement
{
    public class DBAdapter : IDisposable, IDBAdapter
    {
        private List<Door> Doors { get; set; }
        private string Path = "../Backend/MockDB/mockDb.json";

        public DBAdapter()
        {
            Open();
        }

        public void Open()
        {
            CreateDBIfNotExists();
            var rawJson = File.ReadAllText(Path);
            Doors = JsonConvert.DeserializeObject<List<Door>>(rawJson);
        }

        public void Create(Door door)
        {
            if (Doors.Any())
            {
                var lastDoor = Doors.OrderByDescending(d => d.Id).First();
                var lastDoorId = lastDoor.Id;
                door.Id = ++lastDoorId;
            }
            else
            {
                door.Id = 0;
            }

            Doors.Add(door);
            StoreData();
        }

        public IEnumerable<Door> Read()
        {
            return Doors;
        }

        public Door Read(int id)
        {
            return Doors.SingleOrDefault(d => d.Id == id);
        }

        public void Update(Door door)
        {
            var selected = Doors.FindIndex(d => d.Id == door.Id);
            Doors[selected] = door;
            StoreData();
        }

        public void Delete(int id)
        {
            Doors.Remove(Doors.SingleOrDefault(d => d.Id == id));
            StoreData();
        }

        private void StoreData()
        {
            CreateDBIfNotExists();
            var json = JsonConvert.SerializeObject(Doors, Formatting.Indented);
            File.WriteAllText(Path, json);
        }

        private void CreateDBIfNotExists()
        {
            if (!File.Exists(Path))
            {
                File.Create(Path);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
