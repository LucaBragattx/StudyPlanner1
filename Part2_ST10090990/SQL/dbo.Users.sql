CREATE TABLE [dbo].[Users] (
    [Username]  VARCHAR (50)  NOT NULL,
    [StudentNo] VARCHAR (10)  NOT NULL,
    [Password]  VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Username] ASC)
);

