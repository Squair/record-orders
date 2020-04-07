namespace record_orderId
{
    //Can be deserialised and serialised to/from json
    public class Order
    {
        public int orderId { get; set; }

        public override string ToString()
        {
            return ($"Order: {orderId.ToString()}");
        }
    }
}