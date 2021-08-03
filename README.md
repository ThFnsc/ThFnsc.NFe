# ThFnsc.NFe

## Rodando localmente (maneira mais rápida e fácil)

1. [Instale o Docker](https://docs.docker.com/get-docker/) e [docker-compose](https://docs.docker.com/compose/install/)
2. Clone o projeto

```
git clone https://github.com/ThFnsc/ThFnsc.NFe
```
3. Entre na pasta do projeto

```
cd ThFnsc.NFe
```
4. Buildar e executar (é suposto demorar um pouquinho mesmo)

```
docker-compose up
```

Ou, adicione o argumento `-d` para rodar em modo _detached_ (executar em segundo plano)

```
docker-compose up -d
```

5. Acesse a interface no seu navegador [http://localhost:24621/](http://localhost:24621/)

O arquivo `docker-compose.yml` cria automaticamente um container com um MySQL pré-configurado e mapeia os arquivos para o diretório `mysql_data`. Então mantenha essa pasta em um lugar seguro para manter seus dados. Pode inclusive ser transferida entre computadores apenas copiando essa pasta.

## Atualizando para a última versão

1. Dê um pull na branch `main`

```
git pull origin main
```

2. Se estava de fato desatualizado, force um novo build

```
docker-compose up --build
```

Ou, adicione o argumento `-d` para rodar em modo _detached_ (executar em segundo plano)

```
docker-compose up --build -d
```
