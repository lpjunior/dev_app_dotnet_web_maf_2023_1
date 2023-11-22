USE BibliotecaDB;
select * from AspNetUsers where Email = 'luis.paulo@senac.com';

delete from Usuarios;
USE BibliotecaDB;
GO
EXEC sp_tables @table_owner = 'dbo';


select * from AspNetRoles

select * from AspNetUserRoles

insert into AspNetUserRoles(RoleId, UserId) values ('3d6ea266-5b82-459b-88cb-19e948b6550f',  '0d0d897e-7651-4265-8195-32d82220e46f');