//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.Converters;

namespace assistant_just_in_case.core.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Converter> InsertConverterAsync(Converter converter);
        IQueryable<Converter> SelectAllConverters();
        ValueTask<Converter> SelectConverterByIdAsync(Guid converterId);
        ValueTask<Converter> UpdateConverterAsync(Converter converter);
        ValueTask<Converter> DeleteConverterAsync(Converter converter);
    }
}
