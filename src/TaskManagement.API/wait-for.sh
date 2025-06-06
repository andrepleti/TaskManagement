#!/bin/sh
# Espera o MySQL ficar pronto antes de iniciar a aplicação
echo "Aguardando MySQL..."
until nc -z -v -w30 db 3306; do
  echo "MySQL ainda não está disponível - aguardando..."
  sleep 5
done
echo "MySQL está pronto. Iniciando aplicação..."
exec "$@"
