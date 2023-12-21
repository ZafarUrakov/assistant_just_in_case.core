//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Brokers.Storages;
using assistant_just_in_case.core.Models.Converters;

namespace assistant_just_in_case.core.Services.Foundations.Converters
{
    public class ConverterService : IConverterService
    {
        private readonly IStorageBroker storageBroker;

        public ConverterService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
        public async ValueTask<Converter> AddConverterAsync(Converter user) =>
            await this.storageBroker.InsertConverterAsync(user);

        public IQueryable<Converter> RetriveAllConverters() =>
            this.storageBroker.SelectAllConverters();

        public ValueTask<Converter> RetrieveConverterByIdAsync(Guid converterId) =>
            this.storageBroker.SelectConverterByIdAsync(converterId);

        public ValueTask<Converter> ModifyConverterAsync(Converter converter) =>
            this.storageBroker.UpdateConverterAsync(converter);

        public async ValueTask<Converter> RemoveConverterAsync(Converter converter) =>
            await this.storageBroker.DeleteConverterAsync(converter);
    }
}
