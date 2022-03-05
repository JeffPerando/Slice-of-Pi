CREATE TABLE [Home]
(
    [ID]                INT PRIMARY   KEY IDENTITY(1,1),
    [StreetAddress]     NVARCHAR(100) NOT NULL,
    [ZipCode]           NVARCHAR(10)  NOT NULL,
    [State]             NVARCHAR(2)   NOT NULL,
    [County]            NVARCHAR(100) NOT NULL,
    [Price]             FLOAT         NOT NULL,
    [UserID]            INT,
    [AgencyID]          INT
);

CREATE TABLE [User]
(
    [ID]                INT PRIMARY    KEY IDENTITY(1,1),
    [Name]              NVARCHAR (100) NOT NULL,
    [EmailAddress]      NVARCHAR (100) NOT NULL,
    [Address]           NVARCHAR (100) NOT NULL,
);
    
CREATE TABLE [AgencyInformation]
(
    [ID]                INT PRIMARY   KEY IDENTITY(1,1),
    [ORI]               NVARCHAR(25)  NOT NULL,
    [AgencyName]        NVARCHAR(100) NOT NULL,
    [AgencyCounty]      NVARCHAR(100) NOT NULL
);

CREATE TABLE [Crime]
(
    [ID]                INT PRIMARY   KEY IDENTITY(1,1),
    [Year]              INT           NOT NULL,
    [State]             NVARCHAR(100)   NOT NULL,
    [OffenseType]       NVARCHAR(100) NOT NULL,
    [TotalOffenses]     INT           NOT NULL, 
    [ActualConvictions] INT           NOT NULL,
    [AgencyID]          INT,
    [Population]        NVARCHAR(100) NOT NULL,
    [CrimePerCapita]    FLOAT         NOT NULL

);

ALTER TABLE [Home]  ADD CONSTRAINT [Home_Fk_User]              FOREIGN KEY  ([UserID])   REFERENCES   [User]              ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [Home]  ADD CONSTRAINT [Home_Fk_AgencyInformation] FOREIGN KEY  ([AgencyID]) REFERENCES   [AgencyInformation] ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [Crime] ADD CONSTRAINT [Home_Fk_Crime]             FOREIGN KEY  ([AgencyID]) REFERENCES   [AgencyInformation] ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;