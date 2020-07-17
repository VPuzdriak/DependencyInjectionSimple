using System;

namespace DependencyInjection.Sample.Services.GuidGenerator
{
    public interface IGuidGenerator
    {
        public Guid Guid { get; }
    }
}