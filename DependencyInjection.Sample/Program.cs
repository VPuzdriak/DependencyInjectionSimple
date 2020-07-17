using System;
using DependencyInjection.Sample.Services.GuidGenerator;
using DependencyInjection.Sample.Services.SomeService;

namespace DependencyInjection.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Sample1();
            Sample2();
            Sample3();
            Sample4();
            Sample5();
            Sample6();
            Sample7();
        }

        private static void Sample1()
        {
            Console.WriteLine("--------------Sample 1------------------");
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(new RandomGuidGenerator());

            DiContainer container = serviceCollection.BuildContainer();

            var guidGenerator1 = container.GetService<RandomGuidGenerator>();
            var guidGenerator2 = container.GetService<RandomGuidGenerator>();

            Console.WriteLine(guidGenerator1.Guid);
            Console.WriteLine(guidGenerator2.Guid);
            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));
            
            Console.WriteLine("----------End of Sample 1---------------");
        }

        private static void Sample2()
        {
            Console.WriteLine("--------------Sample 2------------------");
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<RandomGuidGenerator>();

            DiContainer container = serviceCollection.BuildContainer();

            var guidGenerator1 = container.GetService<RandomGuidGenerator>();
            var guidGenerator2 = container.GetService<RandomGuidGenerator>();

            Console.WriteLine(guidGenerator1.Guid);
            Console.WriteLine(guidGenerator2.Guid);
            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));
            
            Console.WriteLine("----------End of Sample 2---------------");
        }

        private static void Sample3()
        {
            Console.WriteLine("--------------Sample 3------------------");
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IGuidGenerator, RandomGuidGenerator>();

            DiContainer container = serviceCollection.BuildContainer();

            var guidGenerator1 = container.GetService<IGuidGenerator>();
            var guidGenerator2 = container.GetService<IGuidGenerator>();

            Console.WriteLine(guidGenerator1.Guid);
            Console.WriteLine(guidGenerator2.Guid);
            Console.WriteLine(guidGenerator1.Equals(guidGenerator2));
            
            Console.WriteLine("----------End of Sample 3---------------");
        }

        private static void Sample4()
        {
            Console.WriteLine("--------------Sample 4------------------");
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IGuidGenerator, RandomGuidGenerator>();
            serviceCollection.AddSingleton<ISomeService, SomeService>();

            DiContainer container = serviceCollection.BuildContainer();

            var someService1 = container.GetService<ISomeService>();
            var someService2 = container.GetService<ISomeService>();

            someService1.PrintGuid();
            someService2.PrintGuid();

            Console.WriteLine(someService1.Equals(someService2));
            
            Console.WriteLine("----------End of Sample 4---------------");
        }

        private static void Sample5()
        {
            Console.WriteLine("--------------Sample 5------------------");
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IGuidGenerator, RandomGuidGenerator>();
            serviceCollection.AddTransient<ISomeService, SomeService>();

            DiContainer container = serviceCollection.BuildContainer();

            var someService1 = container.GetService<ISomeService>();
            var someService2 = container.GetService<ISomeService>();

            someService1.PrintGuid();
            someService2.PrintGuid();

            Console.WriteLine(someService1.Equals(someService2));
            
            Console.WriteLine("----------End of Sample 5---------------");
        }

        private static void Sample6()
        {
            Console.WriteLine("--------------Sample 6------------------");
            
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IGuidGenerator, RandomGuidGenerator>();
            serviceCollection.AddTransient<ISomeService, SomeService>().UsingConstructor(typeof(IGuidGenerator));

            DiContainer container = serviceCollection.BuildContainer();

            var someService1 = container.GetService<ISomeService>();
            var someService2 = container.GetService<ISomeService>();

            someService1.PrintGuid();
            someService2.PrintGuid();

            Console.WriteLine(someService1.Equals(someService2));
            
            Console.WriteLine("----------End of Sample 6---------------");
        }

        private static void Sample7()
        {
            Console.WriteLine("--------------Sample 7------------------");
            
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
            
            Console.WriteLine("----------End of Sample 7---------------");
        }
    }
}