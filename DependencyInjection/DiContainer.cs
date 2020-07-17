using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection.Descriptors;
using DependencyInjection.Enums;

namespace DependencyInjection
{
    public class DiContainer : IDisposable
    {
        private readonly IList<ServiceDescriptor> _descriptors;

        public DiContainer(IList<ServiceDescriptor> descriptors)
        {
            _descriptors = descriptors;
        }

        public object GetService(Type serviceType)
        {
            ServiceDescriptor descriptor = _descriptors.SingleOrDefault(d => d.ServiceType == serviceType);

            if (descriptor == null)
            {
                throw new Exception($"Service of type {serviceType.FullName} is not registered");
            }

            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            object[] ctorArgs = descriptor.ConstructorInfo.GetParameters()
                .Select(p => p.ParameterType)
                .Select(pt => GetService(pt))
                .ToArray();

            object implementation = Activator.CreateInstance(descriptor.ImplementationType, ctorArgs);

            if (descriptor.LifeTime == LifeTime.Singleton)
            {
                descriptor.Implementation = implementation;
            }

            return implementation;
        }

        public TService GetService<TService>()
        {
            return (TService) GetService(typeof(TService));
        }

        public void Dispose()
        {
            IEnumerable<IDisposable> disposables = _descriptors
                .Where(d => d.Implementation != null)
                .Where(d => d.Implementation is IDisposable)
                .Select(d => (IDisposable) d.Implementation);

            foreach (IDisposable disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }
}