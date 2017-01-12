using System;
using System.IO;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.Data
{
    [Table("TNotNullEntity")]
    [PrimaryKey("FID", FieldFlags.Identity)]
    public class NotNullEntity : BaseEntity<int>
    {
        [Field("FIntProp")]
        public int IntProp { get; set; }

        [Field("FShortProp")]
        public short ShortProp { get; set; }

        [Field("FLongProp")]
        public long LongProp { get; set; }

        [Field("FFloatProp")]
        public float FloatProp { get; set; }

        [Field("FDoubleProp")]
        public double DoubleProp { get; set; }

        [Field("FDecimalProp")]
        public decimal DecimalProp { get; set; }

        [Field("FBoolProp")]
        public bool BoolProp { get; set; }

        [Field("FStringProp")]
        public string StringProp { get; set; }

        [Field("FDateTimeProp")]
        public DateTime DateTimeProp { get; set; }

        [Field("FBinaryProp")]
        public byte[] BinaryProp { get; set; }

        [Field("FGuidProp")]
        public Guid GuidProp { get; set; }

        [Field("FEnumProp")]
        public FileMode EnumProp { get; set; }

        public string NotMappedProp { get; set; }
    }

    [Table("TNullableEntity")]
    [PrimaryKey("FID", FieldFlags.Identity)]
    public class NullableEntity : BaseEntity<int>
    {
        [Field("FIntProp")]
        public int? IntProp { get; set; }

        [Field("FShortProp")]
        public short? ShortProp { get; set; }

        [Field("FLongProp")]
        public long? LongProp { get; set; }

        [Field("FFloatProp")]
        public float? FloatProp { get; set; }

        [Field("FDoubleProp")]
        public double? DoubleProp { get; set; }

        [Field("FDecimalProp")]
        public decimal? DecimalProp { get; set; }

        [Field("FBoolProp")]
        public bool? BoolProp { get; set; }

        [Field("FDateTimeProp")]
        public DateTime? DateTimeProp { get; set; }

        [Field("FGuidProp")]
        public Guid? GuidProp { get; set; }

        [Field("FEnumProp")]
        public FileMode? EnumProp { get; set; }
    }

    [PrimaryKey("FID", FieldFlags.Identity)]
    public abstract class CreateEntity : BaseEntity<int>
    {
        [Field("FCreateBy", FieldFlags.Insert)]
        public int CreateBy { get; set; }

        [Field("FCreateAt", FieldFlags.Insert)]
        public DateTime CreateAt { get; set; }
    }

    public abstract class UpdateEntity : CreateEntity
    {
        [Field("FUpdateBy")]
        public int UpdateBy { get; set; }

        [Field("FUpdateAt")]
        public DateTime UpdateAt { get; set; }
    }

    [Table("TDerivedEntity")]
    public class DerivedEntity : UpdateEntity
    {
    }
}
