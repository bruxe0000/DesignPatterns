
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomeDesignPatterns.BehavioralDP
{
    /// <summary>
    /// Pros:
    /// - Open/Closed Principle. 
    /// You can introduce new subscriber classes 
    /// without having to change the publisher’s code (and vice versa if there’s a publisher interface).
    /// - You can establish relations between objects at runtime.
    /// 
    /// Cons:
    /// - Subscribers are notified in random order.
    /// </summary>
    /// 


    // The base publisher class includes subscription management
    // code and notification methods.
    public class EventManager
    {
        private Dictionary<string, IEventListenter> listeners = new Dictionary<string, IEventListenter>();

        public void subscribe(string eventType, IEventListenter listener)
        {
            listeners[eventType] = listener;
        }

        public void unsubcribe(string eventType)
        {
            listeners.Remove(eventType);
        }

        public void notify(string eventType, string data)
        {
            foreach (var listener in listeners)
            {
                if (listener.Key == eventType)
                {
                    listener.Value.update(data);
                }
            }
        }
    }

    public class FileObject
    {
        public string Name { get; set; }
        public string path { get; set; }

        public FileObject(string filePath)
        {
            Name = Path.GetFileName(filePath);
            path = filePath;
        }

        public async Task Write(string message)
        {
            using StreamWriter sw = new (path, append: true);
            await sw.WriteLineAsync(message);
        }
    }


    // The concrete publisher contains real business logic that's
    // interesting for some subscribers. We could derive this class
    // from the base publisher, but that isn't always possible in
    // real life because the concrete publisher might already be a
    // subclass. In this case, you can patch the subscription logic
    // in with composition, as we did here.
    public class Editor
    {
        public EventManager events;
        private FileObject file;

        public Editor()
        {
            events = new EventManager();
        }

        // Methods of business logic can notify subscribers about
        // changes.
        public void openFile(string path)
        {
            this.file = new FileObject(path);
            events.notify("open", file.Name);
        }

        public async void saveFile(string path, string message)
        {
            await file.Write(message);
            events.notify("save", file.Name);
        }
    }


    // Here's the subscriber interface. If your programming language
    // supports functional types, you can replace the whole
    // subscriber hierarchy with a set of functions.
    public interface IEventListenter
    {
        void update(string fileName);
    }

    // Concrete subscribers react to updates issued by the publisher
    // they are attached to.
    public class LoggingListener : IEventListenter
    {
        private FileObject log;
        private string message;

        public LoggingListener(string log_filename, string message)
        {
            this.log = new FileObject(log_filename);
            this.message = message;
        }

        public void update(string fileName)
        {
            log.Write($"{message} {fileName}");
            Console.WriteLine($"{message} {fileName}");
        }
    }

    public class EmailAlertsListener : IEventListenter
    {
        private string email;
        private string message;

        public EmailAlertsListener(string email, string message)
        {
            this.email = email;
            this.message = message;
        }

        public void update(string fileName)
        {
            Console.WriteLine($"{message} {fileName}");
        }
    }

    public class Application
    {
        private Editor editor;
        private LoggingListener logger;
        private EmailAlertsListener emailAlerts;

        public Application()
        {
            config();
        }

        public void config()
        {
            editor = new Editor();

            logger = new LoggingListener(
                log_filename: @"D:\Programming\Bruce_code\CSharp\DesignPatterns\SomeDesignPatterns\SomeDesignPatterns\BehavioralDP\logging.txt",
                message: "Someone has opened the file:"
                );
            editor.events.subscribe("open", logger);

            emailAlerts = new EmailAlertsListener(
                email: "admin@example.com",
                message: "Someone has changed the file:"
                );
            editor.events.subscribe("save", emailAlerts);
        }

        public void Run()
        {
            string filePath = @"D:\Programming\Bruce_code\CSharp\DesignPatterns\SomeDesignPatterns\SomeDesignPatterns\BehavioralDP\test_file.txt";
            editor.openFile(filePath);
            editor.saveFile(filePath, "Testing ABC...");
        }
    }
}
