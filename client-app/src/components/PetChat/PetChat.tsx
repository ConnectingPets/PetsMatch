import React from "react";
import { observer } from "mobx-react";
import "./PetChat.scss";
import { ChatMessages } from "../ChatMessages/ChatMessages";
import { ChatHeader } from "../ChatHeader/ChatHeader";
import { useParams } from "react-router-dom";
import chatStore from "../../stores/chatStore";
import { Field, Form } from "react-final-form";
import { sendMessageValidator } from "../../validators/messageValidator";
import agent from "../../api/axiosAgent";
import { FormApi } from "final-form";
import { useEffect, useRef, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { IMessage } from "../../interfaces/Interfaces";

interface PetChatProps { }

interface ISendMessage {
  Content: string;
  AnimalId: string;
  MatchId: string;
}

export const PetChat: React.FC<PetChatProps> = observer(() => {
  const { id: animalId } = useParams();
  const [messages, setMessages] = useState<IMessage[]>([]);

  const connection = useRef<signalR.HubConnection | null>(null);

  useEffect(() => {
    agent.apiMessages.getChatHistory(chatStore.matchId!)
      .then((res) => {
        setMessages(res.data.reverse());
    });
  }, [animalId]);

  useEffect(() => {
    connection.current = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5216/chat")
      .build();

    connection.current.start()
      .then(() => {
        console.log('Connected');
      })
      .catch((err) =>{
        console.error(err);
      });

    connection.current.on(
      "ReceiveMessage",
      (animalId: string, content: string, date: string) => {
        const newMessage: IMessage = {
          content,
          animalId,
          sentOn: date,
        };

        setMessages((prevMessages) => [newMessage, ...prevMessages]);
      }
    );
  }, []);

  const onSend = async (values: ISendMessage, form: FormApi<ISendMessage>) => {
    const body: ISendMessage = {
      Content: values.Content,
      MatchId: chatStore.matchId!,
      AnimalId: animalId!,
    };

    try {
      if (connection.current?.state === signalR.HubConnectionState.Connected) {
        const result = await agent.apiMessages.sendMessage(body);

        if (result.isSuccess) {
          await connection.current?.invoke(
            "SendMessage",
            chatStore.matchId,
            animalId,
            body.Content,
            new Date(result.data)
          );
        }
      }

      form.reset();
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className="chat-container">
      <ChatHeader />
      <ChatMessages messages={messages} />
      <Form
        onSubmit={(values, form) => onSend(values, form)}
        validate={sendMessageValidator}
        render={({ handleSubmit }) => (
          <form onSubmit={handleSubmit} className="message-input">
            <Field name="Content">
              {({ input }) => (
                <input
                  {...input}
                  type="text"
                  placeholder="Type your message..."
                />
              )}
            </Field>
            <button type="submit">Send</button>
          </form>
        )}
      />
    </div>
  );
});
