using PlantTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlantTemp.View
{

    public class DataTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public DataTemplate BaseDataTemplate { get; set; }
        public DataTemplate UserDataTemplate { get; set; }
        public DataTemplate BotDataTemplate { get; set; }
        public DataTemplate ImageDataTemplate { get; set; }
        public DataTemplate ChatDataTemplate { get; set; }
        public DataTemplate ServiceDataTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //item을 MessageModel인지 확인   
            if (item is MessageModel message)
            {
                //MessageType에 따라서 DataTemplate를 각각 반환
                switch (message.MessageType)
                {
                    case "User":
                        return UserDataTemplate;
                    case "Bot":
                        return BotDataTemplate;
                    case "Image":
                        return ImageDataTemplate;
                    case "Chat":
                        return ChatDataTemplate;
                    case "Service":
                        return ServiceDataTemplate;
                    default:
                        return BaseDataTemplate;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
