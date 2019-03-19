# Console serverless chat
This is a simple example of how can peer-to-peer communication can be organized. As an illustrative example was chosen a hackneyed topic of chats. 
Having a set of PC's, virtual machines or  whatever you can configure this application by specifying it's peer IPs in chat_config.xml file (which needs to be placed near exe). 

After starting the application, anyone can send a message and each peer (other connected participant) will resend received message to its own peers and so on. Finally, the sended message from one point will be delivered to any other one. You can choose any network topology (configuration of peers set).

For example, for this configuration:
![Alt text](github%20imgs/Peer%20topology%201.png?raw=true "Topology 1")

you need to specify such peers configuration (shown for 'host'):
```xml
<?xml version="1.0" encoding="utf-8" ?>
<peers>
  <peer>172.25.142.241</peer>
  <peer>172.25.134.87</peer>
  <peer>172.25.136.66</peer>
</peers>
```

As you can see, peers configuaration considers the directions of peer-to-peer route, which increase the number of options of how peers can be orginized. 
