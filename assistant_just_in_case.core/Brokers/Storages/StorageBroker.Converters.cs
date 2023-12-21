//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.Converters;
using Microsoft.EntityFrameworkCore;

namespace assistant_just_in_case.core.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Converter> Converters { get; set; }

        public async ValueTask<Converter> InsertConverterAsync(Converter converter) =>
            await InsertAsync(converter);

        public IQueryable<Converter> SelectAllConverters() =>
        SelectAll<Converter>();

        public async ValueTask<Converter> SelectConverterByIdAsync(Guid converterId) =>
        await SelectAsync<Converter>(converterId);

        public async ValueTask<Converter> UpdateConverterAsync(Converter converter) =>
        await UpdateAsync(converter);

        public async ValueTask<Converter> DeleteConverterAsync(Converter converter) =>
            await DeleteAsync(converter);
    }
}
