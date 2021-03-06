﻿/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class Subscriber : MonoBehaviour
    {
        public string Topic;

        public float TimeStep;
        private int timeStep{ get { return (int)(TimeStep * 1000); } } // the rate(in ms in between messages) at which to throttle the topics

        public MessageReceiver MessageReceiver;

        protected RosSocket rosSocket;
        protected int publicationId;

        private void Start()
        {
            rosSocket = GetComponent<RosConnector>().RosSocket;
            rosSocket.Subscribe(Topic, MessageTypes.RosMessageType(MessageReceiver.MessageType), Receive, timeStep);
        }
               
        private void Receive(Message message)
        {            
            MessageReceiver.RaiseMessageReception(new MessageEventArgs(message));
        }
    }
}