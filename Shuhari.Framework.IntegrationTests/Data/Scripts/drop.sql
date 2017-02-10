use tempdb;
if db_id('$(db)') is not null
begin
  alter database $(db) set single_user with rollback immediate;
  drop database $(db);
end
go
