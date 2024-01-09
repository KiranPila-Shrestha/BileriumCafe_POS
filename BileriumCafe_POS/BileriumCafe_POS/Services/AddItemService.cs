using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BileriumCafe_POS.Utils;
using BileriumCafe_POS.Models;
using System.Text.Json;

namespace BileriumCafe_POS.Services
{
    public class AddItemService
    {
        private readonly List<AddIn> _addInsList = new()
        {
            new() {Id =  Guid.NewGuid(),  ProductName = "Sugar", Price = 15.0 },
            new() {Id =  Guid.NewGuid(),  ProductName = "Honey", Price = 30.0 },
            new() {Id =  Guid.NewGuid(),  ProductName = "Syrups", Price = 40.0 },
            new() {Id =  Guid.NewGuid(),  ProductName = "Vanila Cream", Price = 50.0 },
            new() {Id =  Guid.NewGuid(),  ProductName = "Choco chips", Price = 50.0 },
            new() {Id =  Guid.NewGuid(),  ProductName = "Choclate Cream", Price = 50.0 }

        };

        //List AddIn from JSON file
        public static List<AddIn> GetAddItem()
        {
            string addInsFilePath = AppUtils.GetProductFile();

            if (!File.Exists(addInsFilePath))
            {
                return new List<AddIn>();
            }

            var json = File.ReadAllText(addInsFilePath);

            return JsonSerializer.Deserialize<List<AddIn>>(json);

        }

        // Save Add Ins in Json File
        public void SaveAddInsInFile(List<AddIn> addInLists)
        {
            //Path of the folder where all files are stored
            string appDir = AppUtils.GetDesktopDirectoryPath();
            string filePathForAddIns = AppUtils.GetProductFile();

            if (!Directory.Exists(appDir))
            {
                Directory.CreateDirectory(appDir);
            }

            var json = JsonSerializer.Serialize(addInLists);

            File.WriteAllText(filePathForAddIns, json);
        }


        // Seeds JSON file with AddIn if it is empty
        public void SeedAddIns()
        {
            List<AddIn> AddInsList = GetAddItem();

            SaveAddInsInFile(_addInsList);


            if (AddInsList.Count == 0)
            {
                SaveAddInsInFile(_addInsList);
            }
        }


        //Get AddIns by Id


        public AddIn GetAddInsByID(String AddInsID)
        {
            List<AddIn> addInsList = GetAddItem();
            AddIn addIns = addInsList.FirstOrDefault(addIns => addIns.Id.ToString() == AddInsID);
            return addIns;
        }



        //Updating ID
        public void UpdateAddInsDetails(AddIn addIns)
        {
            List<AddIn> addInsList = GetAddItem();

            AddIn AddInsForUpdate = addInsList.FirstOrDefault(_addInsList => _addInsList.Id.ToString() == addIns.Id.ToString());

            if (AddInsForUpdate == null)
            {
                throw new Exception("No AddIns Found");
            }


            AddInsForUpdate.ProductName = addIns.ProductName;
            AddInsForUpdate.Price = addIns.Price;
            SaveAddInsInFile(addInsList);
        }
    }
}


