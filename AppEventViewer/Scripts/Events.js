
    /* Initialisation */
    $(document).ready(function() {
        function localizeDateStr (date_to_convert_str) 
        { var dateToConvert = new Date(date_to_convert_str);
        var date =  new Date();
        var hours = dateToConvert.getHours + (date.getTimezoneOffset() / 60);
        dateToConvert.setHours(hours);
            // return dateToConvert.toString(); 
            return dateToConvert;
        }
        function colDefFunds() {
            return [
                  { "aTargets": ["EvRTime"], "bVisible": true, "mRender": function(data, type, row) {
                      return data;
                  } },
                { "aTargets": ["EvRCatergory"], "bVisible": false },
                { "aTargets": ["EvRServer"], "bVisible": true },
                { "aTargets": ["EvREventCode"], "bVisible": false },
                { "aTargets": ["EvREventType"], "bVisible": false },
                { "aTargets": ["EvRInsMessage"], "bVisible": true },
                { "aTargets": ["EvRLogfile"], "bVisible": false },
                { "aTargets": ["EvRMsg"], "bVisible": false },
                { "aTargets": ["EvRRecordNr"], "bVisible": false },
                { "aTargets": ["EvRSource"], "bVisible": false },
                { "aTargets": ["EvRType"], "bVisible": false }
            ];
        }

        function notHideColFunds() {
            return [0];
        }
        ResultDataTablesInit('#EventsTbl', colDefFunds, notHideColFunds);
    })
