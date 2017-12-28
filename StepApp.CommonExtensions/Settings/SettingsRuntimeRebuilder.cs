using System;
using System.Configuration;
using System.IO;
using System.Xml;
using StepApp.CommonExtensions.Serialization;

namespace StepApp.CommonExtensions.Settings
{
    public static class SettingsRuntimeRebuilder
    {
        public static bool RebuildSettingsFromFile(this System.Configuration.ApplicationSettingsBase settingsClass,
            string appFullPath, string fileName = "MessagesCustom.settings")
        {

            try
            {
                if (!File.Exists(appFullPath + Path.DirectorySeparatorChar + fileName)) return false;


                // 1. Open the settings xml file present in the same location

                XmlDocument docSetting = new XmlDocument();
                docSetting.Load(appFullPath + Path.DirectorySeparatorChar + fileName);
                XmlNodeList labelSettings = docSetting.GetElementsByTagName("Settings")[0].ChildNodes;

                //2. look for all Lables2 settings in Label settings & update
                foreach (XmlNode item in labelSettings)
                {
                    var nameItem = item.Attributes["Name"];

                    //if collection of strings
                    if (item.InnerText.Contains("<ArrayOfString"))
                    {
                        var obj = item.InnerText.XmlDeserializeFromString<string[]>();
                        var newObj = new System.Collections.Specialized.StringCollection();
                        newObj.AddRange(obj);
                        settingsClass[nameItem.Value] = newObj;
                    }
                    else
                    {
                        settingsClass[nameItem.Value] = item.InnerText;
                    }


                    //  settingsClass.PropertyValues[nameItem.Value].PropertyValue = item.InnerText;
                }

                settingsClass.Save();
                    // save. this will save it to user.config not app.config but the setting will come in effect in application
                return true;
                // settingsClass.Reload();
            }
            catch (Exception e)
            {
                //todo: add logging
                return false;
                throw;
            }

        }

        /// <summary>
        /// Custom file must exist
        /// Only editing supported now, this means that property must exist in xml
        /// Collections not supported yet, edit from file
        /// </summary>
        /// <param name="settingsClass"></param>
        /// <param name="appFullPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SaveSettingsToCustomFile(this System.Configuration.ApplicationSettingsBase settingsClass,
            string appFullPath, string fileName = "MessagesCustom.settings")
        {
            try
            {
                var fullPathWithName = appFullPath + Path.DirectorySeparatorChar + fileName;
                if (!File.Exists(fullPathWithName)) return false;

                XmlDocument docSetting = new XmlDocument();
                docSetting.Load(fullPathWithName);
                XmlNodeList labelSettings = docSetting.GetElementsByTagName("Settings")[0].ChildNodes;


                foreach (SettingsProperty settingsClassProperty in settingsClass.Properties)
                {
                    foreach (XmlNode item in labelSettings)
                    {
                        if (item.InnerText.Contains("<ArrayOfString")) continue;

                        if (item.Attributes["Name"].Value == settingsClassProperty.Name &&
                            item.InnerText != settingsClass[settingsClassProperty.Name] as string)
                        {
                            item.InnerText = (string) settingsClass[settingsClassProperty.Name];
                        }
                    }
                }

                docSetting.Save(fullPathWithName);
                return true;
            }
            catch (Exception e)
            {
                //todo: add logging
                return false;
                throw;
            }
        }
    }
}
