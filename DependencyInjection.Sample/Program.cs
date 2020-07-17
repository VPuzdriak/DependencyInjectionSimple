using System;
using DependencyInjection.Sample.Services.GuidGenerator;
using DependencyInjection.Sample.Services.SomeService;

namespace DependencyInjection.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Sample1();
            // Sample2();
            // Sample3();
            // Sample4();
            // Sample5();
            // Sample6();
            Sample7();
        }

        private static void Sample1()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(new RandomGuidGenerator());

            DiContainer container = serviceCollection.BuildContainer();

            var guidGenerator1 = container.GetService<RandomGuidGenerator>();
            var guidGenerator2 = container.GetService<RandomGuidGenerator>();

            Console.WriteLine(guidGenerator1.Guid);
            Console.WriteLine(guidGenerator2.Guid);
            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));
        }

        private static void Sample2()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<RandomGuidGenerator>();

            DiContainer container = serviceCollection.BuildContainer();

            var guidGenerator1 = container.GetService<RandomGuidGenerator>();
            var guidGenerator2 = container.GetService<RandomGuidGenerator>();

            Console.WriteLine(guidGenerator1.Guid);
            Console.WriteLine(guidGenerator2.Guid);
            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));
        }

        private static void Sample3()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IGuidGenerator, RandomGuidGenerator>();

            DiContainer container = serviceCollection.BuildContainer();

            var guidGenerator1 = container.GetService<IGuidGenerator>();
            var guidGenerator2 = container.GetService<IGuidGenerator>();

            Console.WriteLine(guidGenerator1.Guid);
            Console.WriteLine(guidGenerator2.Guid);
            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));
        }

        private static void Sample4()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IGuidGenerator, RandomGuidGenerator>();
            serviceCollection.AddSingleton<ISomeService, SomeService>();

            DiContainer container = serviceCollection.BuildContainer();

            var someService1 = container.GetService<ISomeService>();
            var someService2 = container.GetService<ISomeService>();

            someService1.PrintGuid();
            someService2.PrintGuid();

            Console.WriteLine(someService1.Equals(someService2));
        }

        private static void Sample5()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IGuidGenerator, RandomGuidGenerator>();
            serviceCollection.AddTransient<ISomeService, SomeService>();

            DiContainer container = serviceCollection.BuildContainer();

            var someService1 = container.GetService<ISomeService>();
            var someService2 = container.GetService<ISomeService>();

            someService1.PrintGuid();
            someService2.PrintGuid();

            Console.WriteLine(someService1.Equals(someService2));
        }

        private static void Sample6()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IGuidGenerator, RandomGuidGenerator>();
            serviceCollection.AddTransient<ISomeService, SomeService>().UsingConstructor(typeof(IGuidGenerator));

            DiContainer container = serviceCollection.BuildContainer();

            var someService1 = container.GetService<ISomeService>();
            var someService2 = container.GetService<ISomeService>();

            someService1.PrintGuid();
            someService2.PrintGuid();

            Console.WriteLine(someService1.Equals(someService2));
        }

        private static void Sample7()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IGuidGenerator, RandomGuidGenerator>();
            serviceCollection.AddSingleton<ISomeService, SomeService>().UsingConstructor(typeof(IGuidGenerator));

            DiContainer container = serviceCollection.BuildContainer();

            var someService1 = container.GetService<ISomeService>();
            var someService2 = container.GetService<ISomeService>();

            Console.WriteLine(someService1.Disposed);
            Console.WriteLine(someService2.Disposed);
            
            container.Dispose();
            
            Console.WriteLine(someService1.Disposed);
            Console.WriteLine(someService2.Disposed);
        }
    }
}