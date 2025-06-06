Todos os comandos abaixo, devem ser rodados na pasta raiz do projeto:

Para rodar o container:

docker-compose up --build

Para gerar os arquivos do relatório:

./run-tests.sh

Para abrir no chrome (no caso do terminal ubuntu):

google-chrome ./coverage/report/index.html

Caso contrário, os arquivos estão no caminho do comando acima, é só abrir o index.html pelo navegador.