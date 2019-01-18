
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2018 11:11:36
-- Generated from EDMX file: C:\Users\Eric\Desktop\exia\A5\Entreprenariat\Projet\MeniconHelper\MeniconHelper\MeniconHelperBDD.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MeniconHelper];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_document_incident_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[document] DROP CONSTRAINT [FK_document_incident_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_incident_area3_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[incident] DROP CONSTRAINT [FK_incident_area3_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_incident_engine2_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[incident] DROP CONSTRAINT [FK_incident_engine2_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_incident_person_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[incident] DROP CONSTRAINT [FK_incident_person_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_incident_statut1_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[incident] DROP CONSTRAINT [FK_incident_statut1_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_incident_type_incident0_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[incident] DROP CONSTRAINT [FK_incident_type_incident0_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_person_role_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[person] DROP CONSTRAINT [FK_person_role_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_supervise_person0_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[supervise] DROP CONSTRAINT [FK_supervise_person0_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_supervise_type_incident_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[supervise] DROP CONSTRAINT [FK_supervise_type_incident_FK];
GO
IF OBJECT_ID(N'[dbo].[FK_task_incident_FK]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[task] DROP CONSTRAINT [FK_task_incident_FK];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[area]', 'U') IS NOT NULL
    DROP TABLE [dbo].[area];
GO
IF OBJECT_ID(N'[dbo].[document]', 'U') IS NOT NULL
    DROP TABLE [dbo].[document];
GO
IF OBJECT_ID(N'[dbo].[engine]', 'U') IS NOT NULL
    DROP TABLE [dbo].[engine];
GO
IF OBJECT_ID(N'[dbo].[incident]', 'U') IS NOT NULL
    DROP TABLE [dbo].[incident];
GO
IF OBJECT_ID(N'[dbo].[person]', 'U') IS NOT NULL
    DROP TABLE [dbo].[person];
GO
IF OBJECT_ID(N'[dbo].[role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[role];
GO
IF OBJECT_ID(N'[dbo].[statut]', 'U') IS NOT NULL
    DROP TABLE [dbo].[statut];
GO
IF OBJECT_ID(N'[dbo].[supervise]', 'U') IS NOT NULL
    DROP TABLE [dbo].[supervise];
GO
IF OBJECT_ID(N'[dbo].[task]', 'U') IS NOT NULL
    DROP TABLE [dbo].[task];
GO
IF OBJECT_ID(N'[dbo].[type_incident]', 'U') IS NOT NULL
    DROP TABLE [dbo].[type_incident];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'area'
CREATE TABLE [dbo].[area] (
    [id_area] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [date_create] datetime  NOT NULL,
    [enable] bit  NOT NULL
);
GO

-- Creating table 'document'
CREATE TABLE [dbo].[document] (
    [id_document] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [date_create] datetime  NOT NULL,
    [id_anomaly] int  NOT NULL
);
GO

-- Creating table 'engine'
CREATE TABLE [dbo].[engine] (
    [id_engine] int IDENTITY(1,1) NOT NULL,
    [name] varchar(50)  NOT NULL,
    [serial_number] varchar(50)  NOT NULL,
    [date_create] datetime  NOT NULL,
    [enable] bit  NOT NULL
);
GO

-- Creating table 'incident'
CREATE TABLE [dbo].[incident] (
    [id_anomaly] int IDENTITY(1,1) NOT NULL,
    [incident_code] varchar(50)  NOT NULL,
    [description] varchar(max)  NOT NULL,
    [lot_number] varchar(50)  NOT NULL,
    [date_create] datetime  NOT NULL,
    [date_processing] datetime  NOT NULL,
    [date_update] datetime  NOT NULL,
    [date_close] datetime  NOT NULL,
    [id_person] int  NOT NULL,
    [id_type_anomaly] int  NOT NULL,
    [id_statut] int  NOT NULL,
    [id_engine] int  NULL,
    [id_area] int  NOT NULL
);
GO

-- Creating table 'person'
CREATE TABLE [dbo].[person] (
    [id_person] int IDENTITY(1,1) NOT NULL,
    [last_name] varchar(50)  NOT NULL,
    [first_name] varchar(50)  NOT NULL,
    [username] varchar(50)  NOT NULL,
    [email] varchar(50)  NOT NULL,
    [phone] varchar(50)  NOT NULL,
    [language] varchar(50)  NOT NULL,
    [password_scan] varchar(50)  NOT NULL,
    [password_default] varchar(130)  NOT NULL,
    [enable] bit  NOT NULL,
    [id_role] int  NOT NULL
);
GO

-- Creating table 'role'
CREATE TABLE [dbo].[role] (
    [id_role] int IDENTITY(1,1) NOT NULL,
    [label] varchar(50)  NOT NULL,
    [enable] bit  NOT NULL
);
GO

-- Creating table 'statut'
CREATE TABLE [dbo].[statut] (
    [id_statut] int IDENTITY(1,1) NOT NULL,
    [label] varchar(50)  NOT NULL,
    [enable] bit  NOT NULL
);
GO

-- Creating table 'task'
CREATE TABLE [dbo].[task] (
    [id_task] int IDENTITY(1,1) NOT NULL,
    [comment] varchar(max)  NOT NULL,
    [date_create] datetime  NOT NULL,
    [date_close] datetime  NOT NULL,
    [id_anomaly] int  NOT NULL
);
GO

-- Creating table 'type_incident'
CREATE TABLE [dbo].[type_incident] (
    [id_type_anomaly] int IDENTITY(1,1) NOT NULL,
    [label] varchar(50)  NOT NULL,
    [enable] bit  NOT NULL
);
GO

-- Creating table 'supervise'
CREATE TABLE [dbo].[supervise] (
    [person_id_person] int  NOT NULL,
    [type_incident_id_type_anomaly] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_area] in table 'area'
ALTER TABLE [dbo].[area]
ADD CONSTRAINT [PK_area]
    PRIMARY KEY CLUSTERED ([id_area] ASC);
GO

-- Creating primary key on [id_document] in table 'document'
ALTER TABLE [dbo].[document]
ADD CONSTRAINT [PK_document]
    PRIMARY KEY CLUSTERED ([id_document] ASC);
GO

-- Creating primary key on [id_engine] in table 'engine'
ALTER TABLE [dbo].[engine]
ADD CONSTRAINT [PK_engine]
    PRIMARY KEY CLUSTERED ([id_engine] ASC);
GO

-- Creating primary key on [id_anomaly] in table 'incident'
ALTER TABLE [dbo].[incident]
ADD CONSTRAINT [PK_incident]
    PRIMARY KEY CLUSTERED ([id_anomaly] ASC);
GO

-- Creating primary key on [id_person] in table 'person'
ALTER TABLE [dbo].[person]
ADD CONSTRAINT [PK_person]
    PRIMARY KEY CLUSTERED ([id_person] ASC);
GO

-- Creating primary key on [id_role] in table 'role'
ALTER TABLE [dbo].[role]
ADD CONSTRAINT [PK_role]
    PRIMARY KEY CLUSTERED ([id_role] ASC);
GO

-- Creating primary key on [id_statut] in table 'statut'
ALTER TABLE [dbo].[statut]
ADD CONSTRAINT [PK_statut]
    PRIMARY KEY CLUSTERED ([id_statut] ASC);
GO

-- Creating primary key on [id_task] in table 'task'
ALTER TABLE [dbo].[task]
ADD CONSTRAINT [PK_task]
    PRIMARY KEY CLUSTERED ([id_task] ASC);
GO

-- Creating primary key on [id_type_anomaly] in table 'type_incident'
ALTER TABLE [dbo].[type_incident]
ADD CONSTRAINT [PK_type_incident]
    PRIMARY KEY CLUSTERED ([id_type_anomaly] ASC);
GO

-- Creating primary key on [person_id_person], [type_incident_id_type_anomaly] in table 'supervise'
ALTER TABLE [dbo].[supervise]
ADD CONSTRAINT [PK_supervise]
    PRIMARY KEY CLUSTERED ([person_id_person], [type_incident_id_type_anomaly] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_area] in table 'incident'
ALTER TABLE [dbo].[incident]
ADD CONSTRAINT [FK_incident_area3_FK]
    FOREIGN KEY ([id_area])
    REFERENCES [dbo].[area]
        ([id_area])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_incident_area3_FK'
CREATE INDEX [IX_FK_incident_area3_FK]
ON [dbo].[incident]
    ([id_area]);
GO

-- Creating foreign key on [id_anomaly] in table 'document'
ALTER TABLE [dbo].[document]
ADD CONSTRAINT [FK_document_incident_FK]
    FOREIGN KEY ([id_anomaly])
    REFERENCES [dbo].[incident]
        ([id_anomaly])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_document_incident_FK'
CREATE INDEX [IX_FK_document_incident_FK]
ON [dbo].[document]
    ([id_anomaly]);
GO

-- Creating foreign key on [id_engine] in table 'incident'
ALTER TABLE [dbo].[incident]
ADD CONSTRAINT [FK_incident_engine2_FK]
    FOREIGN KEY ([id_engine])
    REFERENCES [dbo].[engine]
        ([id_engine])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_incident_engine2_FK'
CREATE INDEX [IX_FK_incident_engine2_FK]
ON [dbo].[incident]
    ([id_engine]);
GO

-- Creating foreign key on [id_person] in table 'incident'
ALTER TABLE [dbo].[incident]
ADD CONSTRAINT [FK_incident_person_FK]
    FOREIGN KEY ([id_person])
    REFERENCES [dbo].[person]
        ([id_person])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_incident_person_FK'
CREATE INDEX [IX_FK_incident_person_FK]
ON [dbo].[incident]
    ([id_person]);
GO

-- Creating foreign key on [id_statut] in table 'incident'
ALTER TABLE [dbo].[incident]
ADD CONSTRAINT [FK_incident_statut1_FK]
    FOREIGN KEY ([id_statut])
    REFERENCES [dbo].[statut]
        ([id_statut])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_incident_statut1_FK'
CREATE INDEX [IX_FK_incident_statut1_FK]
ON [dbo].[incident]
    ([id_statut]);
GO

-- Creating foreign key on [id_type_anomaly] in table 'incident'
ALTER TABLE [dbo].[incident]
ADD CONSTRAINT [FK_incident_type_incident0_FK]
    FOREIGN KEY ([id_type_anomaly])
    REFERENCES [dbo].[type_incident]
        ([id_type_anomaly])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_incident_type_incident0_FK'
CREATE INDEX [IX_FK_incident_type_incident0_FK]
ON [dbo].[incident]
    ([id_type_anomaly]);
GO

-- Creating foreign key on [id_anomaly] in table 'task'
ALTER TABLE [dbo].[task]
ADD CONSTRAINT [FK_task_incident_FK]
    FOREIGN KEY ([id_anomaly])
    REFERENCES [dbo].[incident]
        ([id_anomaly])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_task_incident_FK'
CREATE INDEX [IX_FK_task_incident_FK]
ON [dbo].[task]
    ([id_anomaly]);
GO

-- Creating foreign key on [id_role] in table 'person'
ALTER TABLE [dbo].[person]
ADD CONSTRAINT [FK_person_role_FK]
    FOREIGN KEY ([id_role])
    REFERENCES [dbo].[role]
        ([id_role])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_person_role_FK'
CREATE INDEX [IX_FK_person_role_FK]
ON [dbo].[person]
    ([id_role]);
GO

-- Creating foreign key on [person_id_person] in table 'supervise'
ALTER TABLE [dbo].[supervise]
ADD CONSTRAINT [FK_supervise_person]
    FOREIGN KEY ([person_id_person])
    REFERENCES [dbo].[person]
        ([id_person])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [type_incident_id_type_anomaly] in table 'supervise'
ALTER TABLE [dbo].[supervise]
ADD CONSTRAINT [FK_supervise_type_incident]
    FOREIGN KEY ([type_incident_id_type_anomaly])
    REFERENCES [dbo].[type_incident]
        ([id_type_anomaly])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_supervise_type_incident'
CREATE INDEX [IX_FK_supervise_type_incident]
ON [dbo].[supervise]
    ([type_incident_id_type_anomaly]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------