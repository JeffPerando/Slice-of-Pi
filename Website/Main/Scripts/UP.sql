CREATE TABLE [Home]
(
    [ID]                INT             PRIMARY KEY IDENTITY(1,1),
    [StreetAddress]     NVARCHAR(100)   NOT NULL,
    [ZipCode]           NVARCHAR(10)    NOT NULL,
    [State]             NVARCHAR(2)     NOT NULL,
    [City]              NVARCHAR(100)   NOT NULL,
    [County]            NVARCHAR(100),
    [Price]             FLOAT           NOT NULL,
    [UserID]            NVARCHAR (450)  NOT NULL
);

CREATE TABLE [User]
(
    [ID]                NVARCHAR (450)  PRIMARY KEY,
    [Name]              NVARCHAR (100)  NOT NULL,
    [EmailAddress]      NVARCHAR (100)  NOT NULL
);

-- I personally feel like we don't need these in the database
/*
CREATE TABLE [AgencyInformation]
(
    [ID]                INT PRIMARY     KEY IDENTITY(1,1),
    [ORI]               NVARCHAR(25)    NOT NULL,
    [AgencyName]        NVARCHAR(100)   NOT NULL,
    [AgencyCounty]      NVARCHAR(100)   NOT NULL
);

CREATE TABLE [Crime]
(
    [ID]                INT PRIMARY     KEY IDENTITY(1,1),
    [Year]              INT             NOT NULL,
    [State]             NVARCHAR(100)   NOT NULL,
    [OffenseType]       NVARCHAR(100)   NOT NULL,
    [TotalOffenses]     INT             NOT NULL, 
    [ActualConvictions] INT             NOT NULL,
    [AgencyID]          INT,
    [Population]        INT             NOT NULL

);
*/

CREATE TABLE [StateCrimeSearchResult]
(
    [ID]                INT             PRIMARY KEY IDENTITY(1,1),
    [UserID]            NVARCHAR(450)   NOT NULL,
    [DateSearched]      DATETIME        NOT NULL,
    [State]             NVARCHAR(100)   NOT NULL,
    [Year]              INT,
    --Crime data below this point
    [Population]        INT,
    [ViolentCrimes]     INT,
    [Homicide]          INT,
    [RapeLegacy]        INT,
    [RapeRevised]       INT,
    [Robbery]           INT,
    [Assault]           INT,
    [PropertyCrimes]    INT,
    [Burglary]          INT,
    [Larceny]           INT,
    [MotorVehicleTheft] INT,
    [Arson]             INT
);

ALTER TABLE [Home]  ADD CONSTRAINT [Home_Fk_User]              FOREIGN KEY  ([UserID])   REFERENCES   [User]              ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Home]  ADD CONSTRAINT [Home_Fk_AgencyInformation] FOREIGN KEY  ([AgencyID]) REFERENCES   [AgencyInformation] ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
--ALTER TABLE [Crime] ADD CONSTRAINT [Home_Fk_Crime]             FOREIGN KEY  ([AgencyID]) REFERENCES   [AgencyInformation] ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [StateCrimeSearchResult] ADD CONSTRAINT [SCSR_Fk_User] FOREIGN KEY ([UserID]) REFERENCES  [User]              ([ID])  ON DELETE NO ACTION ON UPDATE NO ACTION;
