use tempdb;
if db_id('Shuhari_Framework_TestDb') is not null
begin
  alter database Shuhari_Framework_TestDb set single_user with rollback immediate;
  drop database Shuhari_Framework_TestDb;
end
go
create database Shuhari_Framework_TestDb;
go
alter database Shuhari_Framework_TestDb set recovery simple with no_wait;
go
use Shuhari_Framework_TestDb;
go

use tempdb;
go
