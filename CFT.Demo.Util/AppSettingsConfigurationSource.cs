using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CFT.Demo.Util
{
    //public class AppSettingsConfigurationSource : FileConfigurationSource
    //{
    //    public AppSettingsConfigurationSource(string path)
    //    {
    //        Path = path;
    //        ReloadOnChange = true;
    //        Optional = true;
    //        FileProvider = null;
    //    }
    //    //public override IConfigurationProvider Build(IConfigurationBuilder builder)
    //    //{
    //    //    FileProvider = FileProvider ?? builder.GetFileProvider();
    //    //   // return new AppSettingsConfigurationProvider;
    //    //}
    //}

    //public class AppSettingsConfigurationProvider : FileConfigurationProvider
    //{
    //    public AppSettingsConfigurationProvider(AppSettingsConfigurationSource source):base(source)
    //    {

    //    }
    //    public override void Load(Stream stream)
    //    {
    //        try
    //        {
    //        }
    //        catch (Exception)
    //        {

    //            throw new Exception("读取配置信息失败,可能是文件内容不正确");
    //        }
    //    }
    //    //private IDictionary<string,string> ReadAppSettings(Stream stream)
    //    //{
    //    //    var data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    //    //}
    //}
}
