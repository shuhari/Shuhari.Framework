use tempdb;
if db_id('$(db)') is not null
begin
  alter database $(db) set single_user with rollback immediate;
  drop database $(db);
end
go
create database $(db);
go
alter database $(db) set recovery simple with no_wait;
go
use $(db);
go

create table TNotNullEntity
(
    FID            int not null identity,
    FIntProp       int not null,
    FShortProp     smallint not null,
    FLongProp      bigint not null,
    FFloatProp     real not null,
    FDoubleProp    float not null,
    FDecimalProp   decimal(18,4) not null,
    FBoolProp      bit not null,
    FStringProp    nvarchar(100) not null,
    FDateTimeProp  datetime2 not null,
    FBinaryProp    varbinary(max) not null,
    FGuidProp      uniqueidentifier not null,
    FEnumProp      int not null,
    constraint PK_TNotNullEntity primary key (FID),
);

create table TNullableEntity
(
    FID            int not null identity,
    FIntProp       int null,
    FShortProp     smallint null,
    FLongProp      bigint null,
    FFloatProp     real null,
    FDoubleProp    float null,
    FDecimalProp   decimal(18,4) null,
    FBoolProp      bit null,
    FDateTimeProp  datetime2 null,
    FGuidProp      uniqueidentifier null,
    FEnumProp      int null,
    constraint PK_TNullableEntity primary key (FID),
);

set identity_insert TNotNullEntity on;
insert into TNotNullEntity (FID, FIntProp, FShortProp, FLongProp, FFloatProp, FDoubleProp, FDecimalProp, FBoolProp, FStringProp, FDateTimeProp, FBinaryProp, FGuidProp, FEnumProp)
    values (1, 1, 1, 1, 1, 1, 1, 1, 'abc', getdate(), 0x, newid(), 1);
set identity_insert TNotNullEntity off;

set identity_insert TNullableEntity on;
insert into TNullableEntity (FID, FIntProp, FShortProp, FLongProp, FFloatProp, FDoubleProp, FDecimalProp, FBoolProp, FDateTimeProp, FGuidProp, FEnumProp)
    values (1, 1, 1, 1, 1, 1, 1, 1, getdate(), newid(), 1);
set identity_insert TNullableEntity off;

use tempdb;
go
