### RabbitMQ ile Topic Exchange yapısı kullanılarak oluşturulmuş Publisher-Consumer konsol uygulamasıdır.
### Dockerfile dosyaları içerisindeki ENV URI alanına RabbitMQ Cloud adresi yazılmalıdır.

### ./UdemyRabbitMQ.publisher
#### docker build -t topic-exc-pub-img .
#### docker run --name topic-exc-pub-con topic-exc-pub-img

### ./UdemyRabbitMQ.subscriber
#### docker build -t topic-exc-subs-img .
#### docker run --name topic-exc-subs-con topic-exc-subs-img
