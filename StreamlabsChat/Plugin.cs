using ChatCore;
using ChatCore.Interfaces;
using ChatCore.Services;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using Microsoft.Extensions.DependencyInjection;
using StreamlabsChat.Interfaces;
using StreamlabsChat.Services;
using StreamlabsChat.WebSockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace StreamlabsChat
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        private IServiceProvider serviceProvider;
        private StreamlabsService streamlabsService;
        private ChatServiceMultiplexer chatServiceMultiplexer;
        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("StreamlabsChat initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            //new GameObject("StreamlabsChatController").AddComponent<StreamlabsChatController>();
            this.streamlabsService = new StreamlabsService(new StreamlabsServer());
            this.streamlabsService.OnTextMessageReceived += this.StreamlabsService_OnTextMessageReceived;
            var instance = ChatCoreInstance.Create();
            this.chatServiceMultiplexer = instance.RunAllServices();
        }

        private void StreamlabsService_OnTextMessageReceived(ChatCore.Interfaces.IChatService arg1, ChatCore.Interfaces.IChatMessage arg2)
        {
            var method = this.chatServiceMultiplexer.GetType().GetMethod("Service_OnTextMessageReceived", (BindingFlags.NonPublic | BindingFlags.Instance));
            method.Invoke(this.chatServiceMultiplexer, new object[2] { arg1, arg2 });

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
