/*
Created		10/1/2021
Modified		10/1/2021
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/
use master
go
create database db_ShopGiay
go
use db_ShopGiay
go
Create table [Categories]
(
	[category_id] Integer Identity NOT NULL,
	[Category_name] Nvarchar(200) NOT NULL,
	[created_at] Nvarchar(50) NULL,
	[updated_at] Nvarchar(50) NULL,
Primary Key ([category_id])
) 
go

Create table [Users]
(
	[user_id] Integer Identity NOT NULL,
	[email] Nvarchar(100) NOT NULL,
	[phone_number] Nvarchar(20) NOT NULL,
	[password] Nvarchar(100) NOT NULL,
	[full_name] Nvarchar(50) NOT NULL,
	[address] Nvarchar(100) NOT NULL,
	[role] Integer NOT NULL,
	[created_at] Datetime NOT NULL,
	[updated_at] Datetime NOT NULL,
	[is_active] Bit NOT NULL,
Primary Key ([user_id])
) 
go

Create table [Products]
(
	[product_id] Integer Identity NOT NULL,
	[category_id] Integer NOT NULL,
	[product_name] Nvarchar(100) NOT NULL,
	[product_height] Nvarchar(100) NOT NULL,
	[product_type] Nvarchar(100) NOT NULL,
	[product_decription] Ntext NOT NULL,
	[product_price] Decimal(18,0) NOT NULL,
	[product_image] Nvarchar(100) NULL,
	[create_at] Datetime NULL,
	[update_at] Datetime NULL,
Primary Key ([product_id])
) 
go

Create table [Orders]
(
	[orders_id] Integer Identity NOT NULL,
	[user_id] Integer NOT NULL,
	[status] Integer NOT NULL,
	[created_at] Datetime NULL,
	[updated_at] Datetime NULL,
Primary Key ([orders_id])
) 
go

Create table [Order_detail]
(
	[orders_id] Integer NOT NULL,
	[product_id] Integer NOT NULL,
	[price] Decimal(18,0) NOT NULL,
	[quantity] Integer NOT NULL,
	[created_at] Datetime NOT NULL,
	[updated_at] Datetime NULL,
Primary Key ([orders_id],[product_id])
) 
go





Alter table [Products] add  foreign key([category_id]) references [Categories] ([category_id])  on update no action on delete no action 
go
Alter table [Orders] add  foreign key([user_id]) references [Users] ([user_id])  on update no action on delete no action 
go
Alter table [Order_detail] add  foreign key([product_id]) references [Products] ([product_id])  on update no action on delete no action 
go
Alter table [Order_detail] add  foreign key([orders_id]) references [Orders] ([orders_id])  on update no action on delete no action 
go


Set quoted_identifier on
go









Set quoted_identifier off
go





