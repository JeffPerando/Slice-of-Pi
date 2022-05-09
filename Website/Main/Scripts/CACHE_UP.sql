
--Create a generic table called APICache, which is the basis for the next tables...
--the hashtag indicates a temporary local table
CREATE TABLE [#APICache]
(
    [Endpoint]          NVARCHAR(256) PRIMARY KEY NOT NULL,
    [Expiry]            DATETIME NOT NULL,
    [Data]              NVARCHAR(MAX)
);

--The falsy expression ensures nothing is copied from the original table. Just in case it gets stored to.
--Otherwise, this just copies the table structure
SELECT * INTO [FBICache] FROM [#APICache] WHERE 1 <> 1;
SELECT * INTO [ATTOMCache] FROM [#APICache] WHERE 1 <> 1;

--drop the original table since we don't need it anymore
DROP TABLE [#APICache];
