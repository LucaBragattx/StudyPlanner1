CREATE TABLE [dbo].[Modules] (
    [ModCode]   VARCHAR (10) NOT NULL,
    [ModName]   VARCHAR (50) NOT NULL,
    [Credits]   INT          NOT NULL,
    [Hours]     REAL         NOT NULL,
    [NumWeeks]  INT          NOT NULL,
    [StartDate] DATE         NOT NULL,
    [SelfStudy] REAL         NOT NULL,
    [Remainder] REAL         NOT NULL,
    [Username]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ModCode] ASC)
);

