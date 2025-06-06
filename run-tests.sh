#!/bin/bash
set -e

echo "Rodando testes e gerando relatório de cobertura..."

docker-compose -f docker-compose.test.yml up --abort-on-container-exit

echo "Testes concluídos. Relatório gerado em ./coverage/report/index.html"
