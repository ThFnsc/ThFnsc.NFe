# ThFnsc.NFe

## Rodando localmente (maneira mais rápida e fácil)

1. [Instale o Docker](https://docs.docker.com/get-docker/) e [docker-compose](https://docs.docker.com/compose/install/)
2. Baixe o arquivo [assets/docker-compose.yml](https://raw.githubusercontent.com/ThFnsc/ThFnsc.NFe/main/assets/docker-compose.yml) em uma nova pasta vazia em algum lugar seguro do seu computador. O arquivo `docker-compose.yml` cria automaticamente um container com um MySQL pré-configurado e mapeia os arquivos para o diretório `mysql_data`.
3. Abra um terminal nessa pasta
4. Buildar e executar em modo _detached_ (segundo plano)

```
docker-compose up -d
```

_É suposto demorar um pouquinho mesmo_

5. Acesse a interface no seu navegador [http://localhost:24621/](http://localhost:24621/)



## Atualizando para a última versão

```
docker-compose pull && docker-compose up -d
```