using System;
using System.Linq;
using System.Reflection;
using DependencyInjection.Enums;

namespace DependencyInjection.Descriptors
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public LifeTime LifeTime { get; }
        public ConstructorInfo ConstructorInfo { get; internal set; }
        public object Implementation { get; internal set; }

        public ServiceDescriptor(object implementation)
            :this(implementation.GetType(), implementation.GetType(), LifeTime.Singleton)
        {
            Implementation = implementation;
        }

        public ServiceDescriptor(Type serviceType, LifeTime lifeTime)
            : this(serviceType, serviceType, lifeTime)
        {
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, LifeTime lifeTime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            LifeTime = lifeTime;
            ConstructorInfo = implementationType.GetConstructors().Length == 1
                ? implementationType.GetConstructors().First()
                : null;
        }
    }
}