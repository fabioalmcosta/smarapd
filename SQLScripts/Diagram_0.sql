/*
   quarta-feira, 8 de abril de 202015:29:23
   User: 
   Server: (localdb)\MSSQLLocalDB
   Database: aspnet-SmarAPDChallenge-A9EFF63F-B0EB-4C02-97DF-6D0E6C831167
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Rooms
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nchar(150) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Rooms ADD CONSTRAINT
	PK_Rooms PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Rooms SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AspNetUsers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Schedules
	(
	Id int NOT NULL IDENTITY (1, 1),
	Title nchar(100) NOT NULL,
	TimeStart datetime NOT NULL,
	TimeEnd datetime NOT NULL,
	RoomId int NOT NULL,
	UserId nvarchar(450) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Schedules ADD CONSTRAINT
	PK_Schedules PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Schedules ADD CONSTRAINT
	FK_Schedules_Rooms FOREIGN KEY
	(
	RoomId
	) REFERENCES dbo.Rooms
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Schedules ADD CONSTRAINT
	FK_Schedules_AspNetUsers FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.AspNetUsers
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Schedules SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
