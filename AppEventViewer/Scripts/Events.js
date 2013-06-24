﻿
    /* Initialisation */
    $(document).ready(function() {
        alert("Whats up in Events.js document ready country?")
        function colDefFunds() {
            return [
                { "aTargets": ["EvRCatergory"], "bVisible": true },
                { "aTargets": ["EvRServer"], "bVisible": true },
                { "aTargets": ["EvREventCode"], "bVisible": false },
                { "aTargets": ["EvREventType"], "bVisible": false },
                { "aTargets": ["EvRInsMessage"], "bVisible": true },
                { "aTargets": ["EvRLogfile"], "bVisible": false },
                { "aTargets": ["EvRMsg"], "bVisible": true },
                { "aTargets": ["EvRRecordNr"], "bVisible": true },
                { "aTargets": ["EvRSource"], "bVisible": true },
                { "aTargets": ["EvRTime"], "bVisible": true },
                { "aTargets": ["EvRType"], "bVisible": false }
            ];
        }

        function notHideColFunds() {
            return [6, 9];
        }
        ResultDataTablesInit('#EventsTbl', colDefFunds, notHideColFunds);
    })