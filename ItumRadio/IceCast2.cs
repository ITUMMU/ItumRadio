using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace itumRadio
{
    public class Source
    {
        public int listener_peak { get; set; }
        public int listeners { get; set; }
        public string listenurl { get; set; }
        public int on_demand { get; set; }
        public object dummy { get; set; }
        public string audio_info { get; set; }
        public int? bitrate { get; set; }
        public string genre { get; set; }
        public string server_description { get; set; }
        public string server_name { get; set; }
        public string server_type { get; set; }
        public string server_url { get; set; }
        public string stream_start { get; set; }
        public string stream_start_iso8601 { get; set; }
    }

    public class Icestats
    {
        public string admin { get; set; }
        public string host { get; set; }
        public string location { get; set; }
        public string server_id { get; set; }
        public string server_start { get; set; }
        public string server_start_iso8601 { get; set; }
        public List<Source> source { get; set; }
    }

    public class IceData
    {
        public Icestats icestats { get; set; }
    }

    public class IceCast2
    {
        public IceData iceData;
        public List<StreamInfo> streamInfoList = new List<StreamInfo>();
        public UriBuilder serverInfo;

        public struct StreamInfo
        {
            public string mountpoint { get; set; }
            public UriBuilder uriListen { get; set; }
            public UriBuilder uriLogo { get; set; }
            public string streamName { get; set; }
            public string streamCity { get; set; }
            public string streamDescription { get; set; }
        }

        private static string DecodeFromUtf8(string utf8String)
        {
            try
            {
                byte[] utf8Bytes = new byte[utf8String.Length];
                for (int i = 0; i < utf8String.Length; ++i)
                {
                    utf8Bytes[i] = (byte)utf8String[i];
                }

                return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in DecodeFromUtf8:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }

                return null;
            }
        }

        private String getResponseData(System.Net.WebHeaderCollection responseHeaders, String Key, Int32 Id)
        {
            try
            {
                String[] data = getResponseData(responseHeaders, Key);

                if (data != null)
                {
                    return (DecodeFromUtf8(data[Id]));

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in getResponseData:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
                return null;
            }
        }

        private String[] getResponseData(System.Net.WebHeaderCollection responseHeaders, String Key)
        {
            try
            {
                if (responseHeaders.GetValues(Key) != null)
                {
                    return (responseHeaders.GetValues(Key));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in getResponseData:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }

                return null;
            }
        }

        public IceCast2(String serverURL)
        {
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    serverInfo = new UriBuilder(serverURL);
                    webClient.Encoding = Encoding.UTF8;
                    serverInfo.Path = @"/status-json.xsl";
                    iceData = JsonConvert.DeserializeObject<IceData>(webClient.DownloadString(serverInfo.Uri));
                    System.Net.WebHeaderCollection responseHeaders = webClient.ResponseHeaders;
                    foreach (Source iceCastSources in iceData.icestats.source)
                    {
                        StreamInfo streamInfo = new StreamInfo();
                        streamInfo.uriListen = new UriBuilder(iceCastSources.listenurl);
                        streamInfo.mountpoint = iceCastSources.listenurl.Split('/').Last().ToLower();

                        if (getResponseData(responseHeaders, streamInfo.mountpoint + @"-description", 0) != null)
                        {
                            streamInfo.streamDescription = getResponseData(responseHeaders, streamInfo.mountpoint + @"-description", 0);
                        }
                        else if (iceCastSources.server_description != String.Empty)
                        {
                            streamInfo.streamDescription = iceCastSources.server_description;
                        }
                        else
                        {
                            streamInfo.streamDescription = "Aucune Description";
                        }

                        if (getResponseData(responseHeaders, streamInfo.mountpoint + @"-name", 0) != null)
                        {
                            streamInfo.streamName = getResponseData(responseHeaders, streamInfo.mountpoint + @"-name", 0);
                        }
                        else if (iceCastSources.server_name != String.Empty)
                        {
                            streamInfo.streamName = iceCastSources.server_name;
                        }
                        else
                        {
                            streamInfo.streamName = streamInfo.mountpoint.ToUpper();
                        }

                        if (getResponseData(responseHeaders, streamInfo.mountpoint + @"-city", 0) != null)
                        {
                            streamInfo.streamCity = getResponseData(responseHeaders, streamInfo.mountpoint + @"-city", 0);
                        }
                        else
                        {
                            streamInfo.streamCity = String.Empty;
                        }

                        if (getResponseData(responseHeaders, streamInfo.mountpoint + @"-logo", 0) != null)
                        {
                            String s = getResponseData(responseHeaders, streamInfo.mountpoint + @"-logo", 0);
                            if (s.IndexOf(@"://") > -1)
                            {
                                streamInfo.uriLogo = new UriBuilder(s);
                            }
                            else
                            {
                                streamInfo.uriLogo = new UriBuilder(serverInfo.Uri);
                                streamInfo.uriLogo.Path = s;
                            }

                        }
                        else
                        {
                            streamInfo.uriLogo = null;
                        }
                        streamInfoList.Add(streamInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                iceData = null;
                streamInfoList = null;
                serverInfo = null;

                using (eventLogger ev = new eventLogger("itumRadio"))
                {
                    ev.writeLog(String.Format("Exception in IceCast2:\n\n{0}", ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                }
            }
        }
    }
}
