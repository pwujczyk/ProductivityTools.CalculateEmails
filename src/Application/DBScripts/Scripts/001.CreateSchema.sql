IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'outlook')
                            BEGIN
                            EXEC('CREATE SCHEMA outlook')
                            END