using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Utils
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = "App1.Resources.AppResources";
        public string Text { get; set; }

        public TranslateExtension()
        {
            if (string.IsNullOrEmpty(App.CultureCode))
            {
                DependencyService.Get<ILocalize>().SetLocale();
                App.CultureCode = DependencyService.Get<ILocalize>().GetCurrentCultureInfo().Name;
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

            string r = resourceManager.GetString(Text, CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(r))
            {
                throw new ArgumentException("Resource file Parameter cannot be null", Text);
            }
            return r;
        }

        public string GetValue(string key)
        {
            if (key == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

            string r = resourceManager.GetString(key, CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(r))
            {
                throw new ArgumentException("Resource file Parameter cannot be null", key);
            }
            return r;
        }
    }
}
