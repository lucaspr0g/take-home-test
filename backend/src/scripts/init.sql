CREATE DATABASE Fundo
GO

USE Fundo
GO

CREATE TABLE [Loan] (
    [Id] int NOT NULL IDENTITY,
    [Amount] decimal(12,2) NOT NULL,
    [CurrentBalance] decimal(12,2) NOT NULL,
    [ApplicantName] nvarchar(100) NOT NULL,
    [Status] nvarchar(6) NOT NULL,
    CONSTRAINT [PK_Loan] PRIMARY KEY ([Id])
);
GO

INSERT INTO [Loan] ([Amount], [CurrentBalance], [ApplicantName], [Status]) VALUES (10000.00, 9500.00, N'Milo Willis', N'active');
INSERT INTO [Loan] ([Amount], [CurrentBalance], [ApplicantName], [Status]) VALUES (5000.00, 2500.00, N'Rowan Preston', N'active');
INSERT INTO [Loan] ([Amount], [CurrentBalance], [ApplicantName], [Status]) VALUES (20000.00, 20000.00, N'John Doe', N'active');
INSERT INTO [Loan] ([Amount], [CurrentBalance], [ApplicantName], [Status]) VALUES (15000.00, 10000.00, N'Jane Doe', N'active');
INSERT INTO [Loan] ([Amount], [CurrentBalance], [ApplicantName], [Status]) VALUES (8000.00, 0.00, N'Milton Wilcher', N'paid');
