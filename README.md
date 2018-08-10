# Instruções para execução da PoC

## - Ferramentas

- Docker: https://www.docker.com
- Azure CLI: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest
- Kubernetes: https://kubernetes.io
- Kompose: http://kompose.io

## - Documentações

- https://www.docker.com/get-started
- https://docs.microsoft.com/en-us/azure/aks/
- https://kubernetes.io/docs/home/?path=users&persona=app-developer&level=foundational
- http://kompose.io/user-guide/

## - dockerfile e docker-compose

O comando abaixo executa as instruções do arquivo **dockerfile**.

```
docker build -t <<nome de usuário no docker>>/todo .
```

A aplicação depende do **MongoDB** sendo assim, criamos um **docker-compose** para criar os serviços (WebAPI e MongoDB) e conectá-los através de uma network.

Caso queira testar a aplicação localmente, execute o comando abaixo

```
docker-compose up -d
```

Esse comando vai subir todos os serviços e suas dependências.

Para testar a aplicação acesse [http://localhost:8080/swager](http://localhost:8080/swager)

## Publicando a imagem no Docker Hub

Até aqui, configuramos a aplicação para rodar localmente. Agora vamos disponibilzar nossa api no Docker Hub.

Antes de tudo é necessário efetuar o login no **docker**

```
docker login
```

Entre com o seu usuário e senha.

Agora vamos gerar a imagem e publicá-la.

```
docker push <<nome de usuário no docker>>/todo
```

Essa imagem será disponibilizada de maneira pública no Docker Hub.

## - AKS - Montando o Ambiente

O AKS é um serviço disponível dentro do Azure. É possível criá-lo de duas formas: linha de comando ou via portal.

Antes disso, precisamos efetuar login no Azure:

```
az login
```

Não vou entrar em detalhes de como crir um cluster por que está muito bem documentado [aqui via linha de comando](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough) e [aqui via portal](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-portal).

Nesse ponto precisamos ter anotado duas informações: **resource group** e **nome do cluster**.

Depois desses procedimento, temos a infraestrutura do kubernetes criada. Mas ainda não temos a nossa aplicação publicada.

Vamos começar instalando o cliente de aks:

```
az aks install-cli
``` 

Agora vamos obter as credenciais para acesso ao cluster:

```
az aks get-credentials --resource-group <<nome do resource group>> --name <<nome do cluster>>
```

Agora vamos nos conectar ao cluster e abrir a central de administração do Kubernetes localmente fazendo conexão com o Azure:

```
az aks browse --resource-group <<nome do resource group>> --name <<nome do cluster>>
```

Um navegador será aberto. Pode ser que a página carregada dê erro, pois o serviço ainda está tentando a conexão, porém, aguarde alguns segundos e dê refresh na página.

## - AKS - Fazendo deploy da Aplicação

O Kubernete espera arquivos de configuração da serviços e deploys. Podemos usar o próprio arquivo docker-compose da nossa aplicação para extrair essas informações e gerar esses arquivos.

Para isso vamos voltar ao nosso projeto e usar a ferramenta **kompose**:

```
kompose convert
```

No caso do nosso projeto, quatro arquivos serão gerados:

- mongodb-deployment
- mongodb-service
- todo-deployment
- todo-service

Eu recomendo mover esses arquivos para uma pastas. No nosso caso eu criei uma pasta chamada Kubernetes.

**IMPORTANTE**

Para expormos nosso serviço, precisamos configurar o **External Endpoint**.
Por padrão, o kompose não configura isso para nós, sendo assim precisamos abrir o arquivo **todo-service** e adicionar, dentro da opção _spec_ a configuração ```type: LoadBalancer```.

O resultado deve ser esse:

```
...
spec:
  type: LoadBalancer
  ports:
  - name: "8080"
    port: 8080
    targetPort: 80
  selector:
    io.kompose.service: todo
...
```

Agora já temos os arquivos de configuração de deploy e serviço criados. Vamos executá-los no Kubernetes.

Via linha de comando, passando a pasta contendo os arquivos gerados pelo kompose:

```
kubectl create -f ./kubernetes
```

Outra maneira é pelo portal do Kubernetes, clicando em **+ CREATE** no canto superior direito, selecionando a opção **CREATE FROM FILE** e fazendo o upload dos arquivos da pasta ./kubernetes.

Depois do deploy efetuado, podemos obter o ip exterdo (External endpoints) da aplicação executando o comando:

```
```

ou pelo portal na seção **Discovery and load balancing**, **Services** na coluna **External endpoints**.

Feito isso, nossa aplicação estará pronta para uso.

O kubernetes tem inúmera opções de gerenciamento, configuração de escalonamento etc. e toda documentação está em [https://kubernetes.io/docs/user-journeys/users/application-developer/foundational/#section-1](https://kubernetes.io/docs/user-journeys/users/application-developer/foundational/#section-1).


Para saber mais sobre como escalar a sua aplicação acesse [https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-scale](https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-scale).