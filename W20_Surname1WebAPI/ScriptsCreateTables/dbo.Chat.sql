CREATE TABLE [dbo].[Chat] (
    [Id]         NVARCHAR(128)          NOT NULL,
    [LastMesage] VARCHAR (50) NULL,
    [IsLogued] TINYINT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Chat_ToAspNetUsers] FOREIGN KEY ([Id]) REFERENCES [AspNetUsers]([Id])
);

