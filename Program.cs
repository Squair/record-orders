using System;

namespace record_orderId
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new OrderHandler();
            order.pathToJson = @".\OrderLog.json";

            //Driver data
            order.record(1);
            order.record(2);
            order.record(3);
            order.record(4);

            //Get the most recently added order from log
            Console.WriteLine(order.getLast(0).ToString());

            //Get 2nd back from latest addition
            Console.WriteLine(order.getLast(2).ToString());
        }
    }
}
