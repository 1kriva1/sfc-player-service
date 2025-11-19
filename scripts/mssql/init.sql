--Create login----------------------------------------------------

PRINT 'Running create_sfc_login.sql...';

:r /usr/src/app/create_sfc_login.sql

PRINT 'Finished running create_sfc_login.sql';

--Create Hangfire database----------------------------------------

PRINT 'Running create_hangfire_database.sql...';

:r /usr/src/app/create_hangfire_database.sql

PRINT 'Finished running create_hangfire_database.sql';

------------------------------------------------------------------
