Create Database [UsersDb]
GO
USE [UsersDb]
GO
/****** Object:  UserDefinedFunction [dbo].[f_GetRandomDate]    Script Date: 01-08-2022 11:19:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create View dbo.v_Rand
As
	Select Rand() [rand]
Go


Create Function [dbo].[f_GetRandomDate]
(
	@DateStart	Date
	,@DateEnd	Date
)
Returns Date
As Begin

	Declare @Result Date

	Select	@Result = DateAdd(Day, [Rand] * DateDiff(Day, @DateStart, @DateEnd), @DateStart)
	From	dbo.v_Rand

	Return 	@Result

	
End
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 01-08-2022 11:19:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Mobile Number] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Mobile Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 01-08-2022 11:19:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Mobile Number] [nvarchar](10) NOT NULL,
	[Amount] [decimal](19, 4) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD FOREIGN KEY([Mobile Number])
REFERENCES [dbo].[Customers] ([Mobile Number])
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [CK_MyTable_PhoneNumber] CHECK  (([Mobile Number] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'))
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [CK_MyTable_PhoneNumber]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_alltransactions]    Script Date: 01-08-2022 11:19:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_alltransactions]( @numberofmonths int) as
begin
if @numberofmonths <= 0
select C.*, tr.total from 
(Select t.[Mobile Number] , SUM(t.amount) as total from  Transactions t
group by t.[Mobile Number]) tr
join Customers c
on c.[Mobile Number] = tr.[Mobile Number]
Else
select C.*, tr.total from 
(Select t.[Mobile Number] , SUM(t.amount) as total from  Transactions t
where t.TransactionDate >= DATEADD(Month,@numberofmonths*-1, GETDATE())
group by t.[Mobile Number]) tr
join Customers c
on c.[Mobile Number] = tr.[Mobile Number]
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_cutomer_transaction]    Script Date: 01-08-2022 11:19:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_get_cutomer_transaction](@mobilenumber nvarchar(10), @numberofmonths int) as
begin
if @numberofmonths <= 0
select C.*, tr.total from 
(Select t.[Mobile Number] , SUM(t.amount) as total from  Transactions t
where  t.[Mobile Number] = @mobilenumber
group by t.[Mobile Number]) tr
join Customers c
on c.[Mobile Number] = tr.[Mobile Number]
Else
select C.*, tr.total from 
(Select t.[Mobile Number] , SUM(t.amount) as total from  Transactions t
where  t.[Mobile Number] = @mobilenumber and t.TransactionDate >= DATEADD(Month,@numberofmonths*-1, GETDATE())
group by t.[Mobile Number]) tr
join Customers c
on c.[Mobile Number] = tr.[Mobile Number]
END
GO
Declare @randommobilenumber decimal(10)
Declare @lowerlimitMobileNumber decimal(10)
Declare @upperlimitMobileNumber decimal(10)
set @lowerlimitMobileNumber= 8345400000
set @upperlimitMobileNumber = 8345499999
Declare @id int
set @id =1
while (@id<100)
Begin
	select @randommobilenumber = ROUND(((@upperlimitMobileNumber-@lowerlimitMobileNumber+1) * RAND() + @lowerlimitMobileNumber),0)
	insert into Customers values('Customer-'+ CAST(@id as nvarchar(20)), @randommobilenumber)
	set @id = @id+1
End

Declare @counter int 
set @counter =1


 Declare @DateStart	Date = '2021-01-01',
		 @DateEnd Date = getDate()

Declare @amount decimal(19,4)
Declare @mo decimal(10)
Declare @lowerlimitamount decimal(10)
Declare @upperlimitamount decimal(10)
set @lowerlimitamount= 100
set @upperlimitamount = 1000
set @id =1
while (@id<10000)
Begin
	 select Top 1 @mo = [Mobile Number] FROM Customers ORDER BY NEWID()
	select @amount = ROUND(((@upperlimitamount-@lowerlimitamount+1) * RAND() + @lowerlimitamount),4)
	insert into Transactions values( dbo.f_GetRandomDate(@DateStart, @DateEnd), @mo,  @amount)
	set @id = @id+1
End
