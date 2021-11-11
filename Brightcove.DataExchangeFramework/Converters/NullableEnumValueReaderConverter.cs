using Brightcove.DataExchangeFramework.ValueReaders;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.DataAccess.Readers;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;

namespace Brightcove.DataExchangeFramework.Converters
{
    [SupportedIds(new string[] { "{28602B8B-760C-472A-895E-732F727A42A5}" })]
    public class NullableEnumValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string FieldNameEnumType = "EnumType";

        public NullableEnumValueReaderConverter(IItemModelRepository repository)
          : base(repository)
        {
        }

        protected override ConvertResult<IValueReader> ConvertSupportedItem(
          ItemModel source)
        {
            Type typeFromTypeName = this.GetTypeFromTypeName(source, "EnumType");
            return typeFromTypeName == (Type)null ? this.NegativeResult(source, "No type was resolved for the item.") : this.PositiveResult((IValueReader)new NullableEnumValueReader(typeFromTypeName));
        }
    }
}