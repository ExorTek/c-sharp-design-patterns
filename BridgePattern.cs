using System;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.MessageSenderBase = new EmailSender();//customerManager.MessageSenderBase = new SmsSender();
            customerManager.AddCustomer();
            Console.ReadKey();
        }
    }

    abstract class MessageSenderBase
    {
        public void Save()
        {
            Console.WriteLine("Message Saved!");
        }

        public abstract void Send(MessageBody messageBody);
    }

    class MessageBody
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }


    class EmailSender : MessageSenderBase
    {
        public override void Send(MessageBody messageBody)
        {
            Console.WriteLine($"{messageBody.Title} / {messageBody.Text} was sent Email");
        }
    }

    class SmsSender : MessageSenderBase
    {
        public override void Send(MessageBody messageBody)
        {
            Console.WriteLine($"{messageBody.Title} / {messageBody.Text} was sent Sms");
        }
    }

    class CustomerManager
    {
        public MessageSenderBase MessageSenderBase { get; set; }

        public void AddCustomer()
        {
            MessageSenderBase.Send(new MessageBody(){Title = "Successful",Text = "Customer Added"});
            Console.WriteLine("Customer Added!");
        }
    }
}