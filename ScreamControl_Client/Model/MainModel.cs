﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreamControl_Client.Model
{
    class MainModel
    {
        #region Fields
        private string _ConnectionInfo;
        //  private
        #endregion

        #region Properties

        /// <summary>
        /// Get or set Connection Info
        /// </summary>
        public string ConnectionInfo { get; set; }
        public List<CultureInfo> Languages { get; set; }
        public CultureInfo CurrentLanguage { get; set; }
        public bool IsStealthMode { get; set; }
        public bool IsSoundAlarmEnabled { get; set; }
        public int AlarmVolume { get; set; }
        public int SystemVolume { get; set; }
        public int DelayBeforeAlarm { get; set; }
        public bool IsMessageAlarmEnabled { get; set; }
        public int AlertWindowDelay { get; set; }
        public int MicBoost { get; set; }
        public float InputVolume { get; set; }
        public int Threshold { get; set; }
        #endregion
    }
}
