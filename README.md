# Introduction 

The objective of that application is play with rabbitmq and the queues. The application has an endpoint that send a mesage to a queue and a hosted services that consumes from this queue.

# Getting Started

To configure the environment and test the application, it's necessary to have installed:

- [Docker desktop](https://www.docker.com/products/docker-desktop)

## Setup RabbitMQ with Docker Image.

First we download the latest version of the RabbitMq image for docker, for this we execute: 

```` 
docker pull rabbitmq
```` 

Then run the image:

```` 
docker run -it --hostname awesome-rabbit --name awesome-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```` 
With this command we have the management plugin installed and enabled by default, which is available on the standard management port of *15672*, with the default username and password of *guest/guest*:

Now you can see rabbit in http://localhost:15672/ 
