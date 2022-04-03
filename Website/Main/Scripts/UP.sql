CREATE TABLE [Home]
(
    [ID]                INT PRIMARY   KEY IDENTITY(1,1),
    [StreetAddress]     NVARCHAR(100) NOT NULL,
    [ZipCode]           NVARCHAR(10)  NOT NULL,
    [State]             NVARCHAR(2)   NOT NULL,
    [County]            NVARCHAR(100) NOT NULL,
    [Price]             FLOAT         NOT NULL,
    [UserID]            NVARCHAR (450) NOT NULL,
    [AgencyID]          INT
);

CREATE TABLE [User]
(
    [ID]                NVARCHAR (450) PRIMARY KEY,
    [Name]              NVARCHAR (100) NOT NULL,
    [EmailAddress]      NVARCHAR (100) NOT NULL,
    [Address]           NVARCHAR (100) NOT NULL
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
    [State]             NVARCHAR(100) NOT NULL,
    [OffenseType]       NVARCHAR(100) NOT NULL,
    [TotalOffenses]     INT           NOT NULL, 
    [ActualConvictions] INT           NOT NULL,
    [AgencyID]          INT,
    [Population]        NVARCHAR(100) NOT NULL,
    [CrimePerCapita]    FLOAT         NOT NULL

);

CREATE TABLE [StateCrimeSearchResult]
(
    [ID]                INT             PRIMARY KEY IDENTITY(1,1),
    [UserID]            NVARCHAR(450)   NOT NULL,
    [DateSearched]      DATETIME        NOT NULL,
    [State]             NVARCHAR(100)   NOT NULL,
    [Year]              INT             NOT NULL,
    --Crime data below this point
    [Population]        INT             NOT NULL,
    [ViolentCrimes]     INT             NOT NULL,
    [Homicide]          INT             NOT NULL,
    [RapeLegacy]        INT             NOT NULL,
    [RapeRevised]       INT             NOT NULL,
    [Robbery]           INT             NOT NULL,
    [Assault]           INT             NOT NULL,
    [PropertyCrimes]    INT             NOT NULL,
    [Burglary]          INT             NOT NULL,
    [Larceny]           INT             NOT NULL,
    [MotorVehicleTheft] INT             NOT NULL,
    [Arson]             INT             NOT NULL
);

ALTER TABLE [Home]  ADD CONSTRAINT [Home_Fk_User]              FOREIGN KEY  ([UserID])   REFERENCES   [User]              ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [Home]  ADD CONSTRAINT [Home_Fk_AgencyInformation] FOREIGN KEY  ([AgencyID]) REFERENCES   [AgencyInformation] ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [Crime] ADD CONSTRAINT [Home_Fk_Crime]             FOREIGN KEY  ([AgencyID]) REFERENCES   [AgencyInformation] ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [StateCrimeSearchResult] ADD CONSTRAINT [SCSR_Fk_User] FOREIGN KEY ([UserID]) REFERENCES  [User]              ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
