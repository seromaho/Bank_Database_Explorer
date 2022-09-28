-- Create a new database called 'Bank_DB'
-- Connect to the 'master' database to run this snippet
USE [master]
-- Create the new database if it does not exist already
IF NOT EXISTS   (
                    SELECT name
                    FROM sys.databases
                    WHERE name = N'Bank_DB'
                )
                CREATE DATABASE [Bank_DB]
GO

-- Connect to the 'Bank_DB' database to run this snippet
USE [Bank_DB]
-- Create a new table called 'Bank_TBL' in schema 'dbo'
-- Drop the table if it already exists
IF  OBJECT_ID('dbo.Bank_TBL', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bank_TBL]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Bank_TBL]
(
	-- [Key] [int] IDENTITY(1,1) NOT NULL,
	[BLZ] [nvarchar](max) NULL,
	[Merkmal] [int] NULL,
	[Bezeichnung] [nvarchar](max) NULL,
	[PLZ] [int] NULL,
	[Ort] [nvarchar](max) NULL,
	[Kurzbezeichnung] [nvarchar](max) NULL,
	[PAN] [int] NULL,
	[BIC] [nvarchar](max) NULL,
	[Pruefzifferberechnungsmethode] [nvarchar](max) NULL,
	[Datensatznummer] [int] NULL,
	[Aenderungskennzeichen] [nvarchar](max) NULL,
	[Bankleitzahlloeschung] [int] NULL,
	[NachfolgeBLZ] [nvarchar](max) NULL
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Connect to the 'Bank_DB' database to run this snippet
USE [Bank_DB]
-- Import table data into table 'Bank_TBL' from the specified file
-- Skip the header (first row)
-- Use a pipe (|) as the field terminator
-- Use the default row terminator: the newline character (\n)
BULK INSERT [Bank_TBL]
FROM '/tmp/Bank_TBL-w-o-PK.csv'
WITH
(
	-- CODEPAGE = 'ACP',
	FIELDTERMINATOR = '|',
	FIRSTROW = 2
)
GO

-- Connect to the 'Bank_DB' database to run this snippet
USE [Bank_DB]
-- Add a PRIMARY KEY identity column
ALTER TABLE [dbo].[Bank_TBL] ADD [Key] INT IDENTITY(1,1) NOT NULL
GO
ALTER TABLE [dbo].[Bank_TBL] ADD CONSTRAINT [PK_Bank_TBL] PRIMARY KEY CLUSTERED
(
	[Key] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
