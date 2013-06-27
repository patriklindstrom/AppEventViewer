
    /* Initialisation */
    $(document).ready(function() {
     
        function colDefFunds() {
            return [
                { "aTargets": ["EvRTime"], "bVisible": true },
                { "aTargets": ["EvRCatergory"], "bVisible": false },
                { "aTargets": ["EvRServer"], "bVisible": true },
                { "aTargets": ["EvREventCode"], "bVisible": false },
                { "aTargets": ["EvREventType"], "bVisible": false },
                { "aTargets": ["EvRInsMessage"], "bVisible": false },
                { "aTargets": ["EvRLogfile"], "bVisible": false },
                { "aTargets": ["EvRMsg"], "bVisible": true },
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
