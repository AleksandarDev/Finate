using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.Messages;
using Serilog.Core;
using Serilog.Events;

namespace Finate.UWP.Logging.Sinks.Raygun
{
    // Copyright 2014 Serilog Contributors
    // 
    // Licensed under the Apache License, Version 2.0 (the "License");
    // you may not use this file except in compliance with the License.
    // You may obtain a copy of the License at
    // 
    //     http://www.apache.org/licenses/LICENSE-2.0
    // 
    // Unless required by applicable law or agreed to in writing, software
    // distributed under the License is distributed on an "AS IS" BASIS,
    // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    // See the License for the specific language governing permissions and
    // limitations under the License.
    //
    // 
    // DISCLAIMER:
    // File was changed by Aleksandar Toplek on 2016.07.09
    //



    /// <summary>
    /// Writes log events to the Raygun.com service.
    /// </summary>
    public class RaygunSink : ILogEventSink
    {
        readonly IFormatProvider _formatProvider;
        readonly string _userNameProperty;
        readonly string _applicationVersionProperty;
        readonly IEnumerable<string> _tags;
        readonly IEnumerable<string> _ignoredFormFieldNames;
        readonly RaygunClient _client;
        private string computerName;

        /// <summary>
        /// Construct a sink that saves errors to the Raygun.io service. Properties are being send as userdata and the level is included as tag. The message is included inside the userdata.
        /// </summary>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="applicationKey">The application key as found on the Raygun website.</param>
        /// <param name="wrapperExceptions">If you have common outer exceptions that wrap a valuable inner exception which you'd prefer to group by, you can specify these by providing a list.</param>
        /// <param name="userNameProperty">Specifies the property name to read the username from. By default it is UserName. Set to null if you do not want to use this feature.</param>
        /// <param name="applicationVersionProperty">Specifies the property to use to retrieve the application version from. You can use an enricher to add the application version to all the log events. When you specify null, Raygun will use the assembly version.</param>
        /// <param name="tags">Specifies the tags to include with every log message. The log level will always be included as a tag.</param>
        /// <param name="ignoredFormFieldNames">Specifies the form field names which to ignore when including request form data.</param>
        public RaygunSink(IFormatProvider formatProvider,
            string applicationKey,
            IEnumerable<Type> wrapperExceptions = null,
            string userNameProperty = "UserName",
            string applicationVersionProperty = "ApplicationVersion",
            IEnumerable<string> tags = null,
            IEnumerable<string> ignoredFormFieldNames = null)
        {
            if (string.IsNullOrEmpty(applicationKey))
                throw new ArgumentNullException(nameof(applicationKey));

            this._formatProvider = formatProvider;
            this._userNameProperty = userNameProperty;
            this._applicationVersionProperty = applicationVersionProperty;
            this._tags = tags ?? new string[0];
            this._ignoredFormFieldNames = ignoredFormFieldNames ?? Enumerable.Empty<string>();

            this._client = new RaygunClient(applicationKey);
            if (wrapperExceptions != null)
                this._client.AddWrapperExceptions(wrapperExceptions.ToArray());


            var hostNames = NetworkInformation.GetHostNames();
            var localName = hostNames.FirstOrDefault(name => name.DisplayName.Contains(".local"));
            this.computerName = localName.DisplayName.Replace(".local", "");
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            //Include the log level as a tag.
            var tags = this._tags.Concat(new[] {logEvent.Level.ToString()}).ToList();

            var properties = logEvent.Properties
                .Select(pv => new {Name = pv.Key, Value = RaygunPropertyFormatter.Simplify(pv.Value)})
                .ToDictionary(a => a.Name, b => b.Value);

            // Add the message 
            properties.Add("RenderedLogMessage", logEvent.RenderMessage(this._formatProvider));
            properties.Add("LogMessageTemplate", logEvent.MessageTemplate.Text);

            // Create new message builder
            var raygunMessageBuilder = RaygunMessageBuilder.New
                .SetEnvironmentDetails()
                .SetTimeStamp(logEvent.Timestamp.UtcDateTime)
                .SetMachineName(new EasClientDeviceInformation().FriendlyName)
                .SetClientDetails()
                .SetTags(tags)
                .SetUserCustomData(properties);

            // Add exception when available
            if (logEvent.Exception != null)
                raygunMessageBuilder.SetExceptionDetails(logEvent.Exception);

            // Add user when requested
            if (!string.IsNullOrWhiteSpace(this._userNameProperty) &&
                logEvent.Properties.ContainsKey(this._userNameProperty) &&
                logEvent.Properties[this._userNameProperty] != null)
                raygunMessageBuilder.SetUser(new RaygunIdentifierMessage(logEvent.Properties[this._userNameProperty].ToString()));

            // Add version when requested
            if (!string.IsNullOrWhiteSpace(this._applicationVersionProperty) &&
                logEvent.Properties.ContainsKey(this._applicationVersionProperty) &&
                logEvent.Properties[this._applicationVersionProperty] != null)
                raygunMessageBuilder.SetVersion(logEvent.Properties[this._applicationVersionProperty].ToString());

            // Build the message
            var raygunMessage = raygunMessageBuilder.Build();

            // Submit
            var action = this._client.SendAsync(raygunMessage);
        }
    }
}