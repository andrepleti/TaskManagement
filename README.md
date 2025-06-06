Todos os comandos abaixo, devem ser rodados na pasta raiz do projeto:

Para rodar o container:

docker-compose up --build

Para gerar os arquivos do relatório:

./run-tests.sh

Para abrir no chrome (no caso do terminal ubuntu):

google-chrome ./coverage/report/index.html

Caso contrário, os arquivos estão no caminho do comando acima, é só abrir o index.html pelo navegador.


Utilizei DDD e SOLID na arquitetura, considero o mínimo viável para organização do código e boas práticas.

Utilizei xUnit e não outras opções, por ser o recomendado pela equipe Microsoft, ser padrão, nativo e mais moderno, portanto, mais alinhado com boas práticas.

Utilizei MySql por gosto pessoal, considero ele leve e já o possuía instalado.

Utilizei EntityFramework porque facilita o versionamento e transações com o banco de dados.

Para melhorar o projeto, eu adicionaria Fluentvalidation, remoderaria o insert e o update de Projects para Inserir tasks junto com o objeto "pai", eliminando 2 endpoints separados (não é algo crucial, mas nesse contexto, enxergo como benéfico), separaria as migrations em um projeto separado na solução, porque se eu quiser alterar o tipo de banco de dados, fica mais fácil gerenciar múltiplas opções, adicionaria swagger, endpoints para recuperar os históricos das tasks e índices ao banco de dados, em especial no histórico, para garantir melhor performance desde o início.