# Set the Base Image for subsequent instructions
FROM mcr.microsoft.com/mssql/server:2019-latest
# FROM must be the first instruction in a Dockerfile

# Set the user name to use as the default user and group for the remainder of the current stage
# The specified user is used for RUN instructions and at runtime, runs the relevant ENTRYPOINT and CMD commands
USER mssql

# Copy files or folders from <source path> in the docker build context to <destination path> in the image's filesystem
COPY --chown=mssql * /tmp/
# In this case, all files in the docker build context are copied to the image's filesystem

# The RUN instruction will execute any commands in a new layer on top of the current image and commit the results
# The resulting committed image will be used for the next step in the Dockerfile
RUN ["/usr/bin/chmod", "740", "/tmp/container_setup.sh", "/tmp/bank_db_setup.sh", "/tmp/bank_db.sql"]
RUN ["/usr/bin/chmod", "640", "/tmp/Bank_TBL-w-o-PK.csv"]
# In this case, the modes of the listed files are changed
RUN ["/usr/bin/sed", "-i", "-e", "s/\r$//", "/tmp/container_setup.sh", "/tmp/bank_db_setup.sh", "/tmp/bank_db.sql", "/tmp/Bank_TBL-w-o-PK.csv"]
# In this case, carriage return characters are removed from the listed files

# Configures the container to be run as an executable
# ENTRYPOINT ["/tmp/container_setup.sh"]

# The main purpose of a CMD is to provide defaults for the executing container
# CMD does not execute anything at build time, but specifies the intended command for the image
CMD ["/tmp/container_setup.sh"]
# If an executable is not specified, then ENTRYPOINT must be specified as well
# There can only be one CMD instruction in a Dockerfile
