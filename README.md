# Console serverless chat
This is a simple example of how can peer-to-peer communication can be organized. As an illustrative example was chosen a hackneyed topic of chats. 
Having a set of PC's, virtual machines or  whatever you can configure this application by specifying it's peer IPs in chat_config.xml file (which needs to be placed near exe). 

After starting the application, anyone can send a message and each peer (other connected participant) will resend received message to its own peers and so on. Finally, the sended message from one point will be delivered to any other one. You can choose any network topology (configuration of peers set).
