using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hackathon.Domain;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Hackathon.Domain;

namespace Hackathon.Tests
{
    [TestClass]
    public class ParseJson
    {
        string pathToData;
        string jsonAsString;
        JObject npa;
        JObject neighborhood1data;
        List<Neighborhood> neighborhoods;

        [TestInitialize]
        public void Initialize()
        {
            string path = "data/npa.json";
            pathToData = System.IO.Path.GetFullPath(path);
            jsonAsString = System.IO.File.ReadAllText(pathToData);
            npa = JObject.Parse(jsonAsString);
            neighborhood1data = (JObject)npa["features"][0];
            InitializeNeighborhoods();
        }

        private void InitializeNeighborhoods()
        {
            neighborhoods = new List<Neighborhood>();
            foreach (var neighborhood in npa["features"])
            {
                neighborhoods.Add(JsonConvert.DeserializeObject<Neighborhood>(neighborhood.ToString()));
            }
        }
        [TestMethod]
        public void CanGetPathToFile()
        {
            Trace.WriteLine(pathToData);
        }
        [TestMethod]
        public void CanReadFile()
        {
            Trace.WriteLine(jsonAsString);
        }
        [TestMethod]
        public void CanGrabFirstNeighborhood()
        {
            Trace.WriteLine(neighborhood1data);
        }
        [TestMethod]
        public void CanDesearializeNeighborhood()
        {
            Neighborhood myNeighborhood = JsonConvert.DeserializeObject<Neighborhood>(neighborhood1data.ToString());
            Assert.IsTrue(myNeighborhood.id == 79);
            Trace.WriteLine(myNeighborhood.geometry.coordinates[0][0][0]);

        }

        [TestMethod]
        public void CanDesearializeAllNeighborhoods()
        {
            Assert.AreEqual(464,neighborhoods.Count);
        }

        [TestMethod]
        public void CanConvertNeighborhoodsToBorderMarkersAndInserIntoDatabase()
        {
            Repository repository = new Repository();
            int orderNumber;
            int idOfNeighborhoodborderMarker = 0;
            int totalRowCount = 0;
            List<NeighborhoodBorderMarker> neighborhoodBorderMarkers = new List<NeighborhoodBorderMarker>();    
            foreach (Neighborhood neighborhood in neighborhoods)
            {
                orderNumber = 0;
                foreach (var setOfCoordinates in neighborhood.geometry.coordinates[0])
                {
                    neighborhoodBorderMarkers.Add(new NeighborhoodBorderMarker()
                    {
                        Id = idOfNeighborhoodborderMarker,
                        NeighborhoodId = neighborhood.id,
                        OrderId = orderNumber,
                        Latitude = setOfCoordinates[0],
                        Longitude = setOfCoordinates[1]
                    });
                    orderNumber++;
                    totalRowCount++;
                }
                idOfNeighborhoodborderMarker++;
            }
            repository.Save(neighborhoodBorderMarkers);
        }

        public class Neighborhood
        {
            public int id { get; set; }
            public geometry geometry { get; set; }
        }
        public class geometry
        {
            public float [][][]coordinates {get;set;}
        }
    }
}
