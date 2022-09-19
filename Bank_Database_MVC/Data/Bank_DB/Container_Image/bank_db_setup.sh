#!/usr/bin/bash

# Execute the "bank_db.sql" script that creates the database
for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -U "SA" -P "P@ssw0rd" -l 30 -i "/tmp/bank_db.sql"
    if [ ${?} -eq 0 ]
    then
        /usr/bin/echo "SUCCESS: create database bank_db"
        break
    else
        /usr/bin/echo "TRY: create database bank_db"
        /usr/bin/sleep 1
    fi
done
