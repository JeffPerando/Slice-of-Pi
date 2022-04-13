ALTER TABLE [Home]  DROP CONSTRAINT [Home_Fk_User];
ALTER TABLE [Home]  DROP CONSTRAINT [Home_Fk_AgencyInformation];
ALTER TABLE [Crime] DROP CONSTRAINT [Home_Fk_Crime];
ALTER TABLE [StateCrimeSearchResult] DROP CONSTRAINT [SCSR_Fk_User];

DROP TABLE [Home];
DROP TABLE [User];
DROP TABLE [AgencyInformation];
DROP TABLE [Crime];
DROP TABLE [StateCrimeSearchResult];