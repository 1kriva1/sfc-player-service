BEGIN TRY
    IF NOT EXISTS (
        SELECT name 
        FROM sys.databases 
        WHERE name = N'Hangfire'
    )
    BEGIN
        CREATE DATABASE Hangfire;
        PRINT 'Database "Hangfire" created successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Database "Hangfire" already exists.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error occurred while creating the database.';
    PRINT ERROR_MESSAGE();
    PRINT ERROR_NUMBER();
    PRINT ERROR_SEVERITY();
    PRINT ERROR_STATE();
    PRINT ERROR_LINE();
END CATCH