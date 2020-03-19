using System;
namespace user_handler.Model
{
    public class dto_model
    {
        public string message { get; set; }
        public bool success { get; set; }
    }
    public class RequestData<T>
    {
        public Data <T> data { get; set; }
    }
    public class Data<T>
    {
        public T Attributes { get; set; }
    }
}
