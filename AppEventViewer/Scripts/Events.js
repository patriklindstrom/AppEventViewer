
    /* Initialisation */
    $(document).ready(function() {
     
        function colDefFunds() {
            return [
                { "aTargets": ["EvRTime"], "bVisible": true },
                { "aTargets": ["EvRCatergory"], "bVisible": true },
                { "aTargets": ["EvRServer"], "bVisible": true },
                { "aTargets": ["EvREventCode"], "bVisible": false },
                { "aTargets": ["EvREventType"], "bVisible": false },
                { "aTargets": ["EvRInsMessage"], "bVisible": true },
                { "aTargets": ["EvRLogfile"], "bVisible": false },
                { "aTargets": ["EvRMsg"], "bVisible": true },
                { "aTargets": ["EvRRecordNr"], "bVisible": true },
                { "aTargets": ["EvRSource"], "bVisible": true },
                { "aTargets": ["EvRType"], "bVisible": false }
            ];
        }

        function notHideColFunds() {
            return [0];
        }
        ResultDataTablesInit('#EventsTbl', colDefFunds, notHideColFunds);
    })
