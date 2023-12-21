//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.Converters;

namespace assistant_just_in_case.core.Services.Foundations.Converters
{
    public interface IConverterService
    {
        ValueTask<Converter> AddConverterAsync(Converter user);
        IQueryable<Converter> RetriveAllConverters();
        ValueTask<Converter> RetrieveConverterByIdAsync(Guid converterId);
        ValueTask<Converter> ModifyConverterAsync(Converter converter);
        ValueTask<Converter> RemoveConverterAsync(Converter converter);
    }
}
