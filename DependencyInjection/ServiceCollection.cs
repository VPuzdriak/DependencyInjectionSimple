using System;
using System.Collections.Generic;
using DependencyInjection.Descriptors;
using DependencyInjection.Enums;
using DependencyInjection.Facades;

namespace DependencyInjection
{
    public class ServiceCollection
    {
        private readonly IList<ServiceDescriptor> _descriptors = new List<ServiceDescriptor>();

        public ServiceDescriptorFacade AddSingleton<TService>(TService service)
        {
            var desciptor = new ServiceDescriptor(service);

            _descriptors.Add(desciptor);

            return new ServiceDescriptorFacade(desciptor);
        }

        public ServiceDescriptorFacade AddSingleton<TService>()
        {
            var desciptor = new ServiceDescriptor(typeof(TService));
            
            _descriptors.Add(desciptor);

            return new ServiceDescriptorFacade(desciptor);
        }

        public ServiceDescriptorFacade AddSingleton<TService, TImplementation>()
        {
            var descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), LifeTime.Singleton);
            
            _descriptors.Add(descriptor);
            
            return new ServiceDescriptorFacade(descriptor);
        }

        public ServiceDescriptorFacade AddTransient<TService, TImplementation>()
        {
            var descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), LifeTime.Transient);
            
            _descriptors.Add(descriptor);
            
            return new ServiceDescriptorFacade(descriptor);
        }

        public DiContainer BuildContainer()
        {
            return new DiContainer(_descriptors);
        }
    }
}