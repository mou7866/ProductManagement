FROM liquibase/liquibase:4.4

CMD ["sh", "-c", "docker-entrypoint.sh --url=${URL} --contexts=${CONTEXTS} --username=${USERNAME} --password=${PASSWORD} --classpath=/liquibase --changeLogFile=changelog/changelog.xml --logLevel=FINE update"]