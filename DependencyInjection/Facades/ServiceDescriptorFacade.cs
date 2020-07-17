using System;
using System.Reflection;
using DependencyInjection.Descriptors;

namespace DependencyInjection.Facades
{
    public class ServiceDescriptorFacade
    {
        private readonly ServiceDescriptor _descriptor;

        public ServiceDescriptorFacade(ServiceDescriptor descriptor)
        {
            _descriptor = descriptor;
        }

        public ServiceDescriptorFacade UsingConstructor(params Type[] ctorArgTypes)
        {
            ConstructorInfo constructorInfo = _descriptor.ImplementationType.GetConstructor(ctorArgTypes);

            if (constructorInfo == null)
            {
                throw new Exception($"Constructor for type {_descriptor.ImplementationType.FullName} does not exist");
            }

            _descriptor.ConstructorInfo = constructorInfo;

            return this;
        }
    }
}