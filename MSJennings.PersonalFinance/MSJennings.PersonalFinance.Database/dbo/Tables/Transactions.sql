CREATE TABLE [dbo].[Transactions] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Date]       DATE           NOT NULL,
    [CategoryId] INT            NOT NULL,
    [Memo]       NVARCHAR (500) NULL,
    [Amount]     DECIMAL (9, 2) NOT NULL,
    [IsCredit]   BIT            NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

