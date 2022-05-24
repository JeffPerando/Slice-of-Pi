ALTER TABLE [Home]  DROP CONSTRAINT [Home_Fk_User];
ALTER TABLE [StateCrimeSearchResult] DROP CONSTRAINT [SCSR_Fk_User];

DROP TABLE [Home];
DROP TABLE [User];
DROP TABLE [StateCrimeSearchResult];
