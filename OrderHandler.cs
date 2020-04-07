using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace record_orderId
{
    class OrderHandler
    {
       
        //Path to order json file
        public string pathToJson { get; set; }
        private List<Order> orders { get; set; }

        //Constructor
        public OrderHandler()
        {
            this.orders = new List<Order>();
        }
        //Records a new order id into json file if it doesn't already exist
        public void record(int orderId)
        {
            //Check json file exists and isnt empty
            if (fileExistsAndNotEmpty(pathToJson))
            {
                using (StreamReader file = File.OpenText(pathToJson))
                {
                    //Deserialize json file and set orders
                    JsonSerializer serializer = new JsonSerializer();
                    List<Order> existingOrders = (List<Order>)serializer.Deserialize(file, typeof(List<Order>));
                    this.orders = existingOrders;
                }
            }

            //Check if order attempting to be added already exists, if it doesn't add it
            var existingOrder = orders.Find(x => x.orderId == orderId);
            if (existingOrder == null)
            {
                this.orders.Add(new Order()
                {
                    orderId = orderId
                });
            }
            else
            {
                return;
            }

            //Serialize new orders list and write to json path
            using (StreamWriter file = File.CreateText(pathToJson))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, this.orders);
            }
        }

        //Returns the ith last element from the log
        public Order getLast(int i)
        {
            //Check file exists and not empty
            if (fileExistsAndNotEmpty(pathToJson))
            {
                using (StreamReader file = File.OpenText(pathToJson))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    List<Order> existingOrders = (List<Order>)serializer.Deserialize(file, typeof(List<Order>));
                    //Get index of last item and check i wont push out of bounds
                    var lastItem = existingOrders.Count - 1;
                    if (i > lastItem) { i = lastItem; }
                    return existingOrders[lastItem - i];
                }
            }
            else
            {
                return null;
            }
        }
        //Checks file exists and isnt empty
        private bool fileExistsAndNotEmpty(string path)
        {
            if (File.Exists(pathToJson) && new FileInfo(pathToJson).Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
