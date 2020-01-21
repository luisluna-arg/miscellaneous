using System;
using System.Web.Mvc;

namespace ExampleApp.Controllers
{
    class CustomSerializer : System.Web.Script.Serialization.JavaScriptSerializer
    {
        public CustomSerializer()
        {
            /* JSON max character count is 2097152, it is a value initialized inside JavaScriptSerializer.
             * When the default JSON action is used, that max count is not modified.
             * This class is inherits from JavaScriptSerializar, and changes the default character count to then Int32 data type max value.
             */
            this.MaxJsonLength = int.MaxValue;
        }
    }

    /// <summary>
    /// Basic controller with a special serializer to increase the max size of JSON data
    /// </summary>
    public class BaseController : Controller
    {
        private CustomSerializer JSSerializer { get; set; }

        public BaseController() : base()
        {
            /* A new CustomSerializer is created for the BaseController instance */
            this.JSSerializer = new CustomSerializer();
        }

        /// <summary>
        /// Returns serialized, using the CustomSerializer, wrapped in a ContentResult instance
        /// </summary>
        /// <param name="content">Data to be serialized</param>
        /// <returns></returns>
        public ContentResult CustomJson(object content)
        {
            return new ContentResult
            {
                Content = this.JSSerializer.Serialize(content),
                ContentType = "application/json"
            };
        }
    }
}
