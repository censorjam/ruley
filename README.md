# ruley

{
    "name": "test rule",
    "debug": true,

    "input": {
        "$type": "Ruley.Redis.RedisBusInput, Ruley.Redis",
        "channel": "HeartBeat",
        "connectionString": "localhost"
    },

    "filters": [
        {
            "$type": "@groupby",
            "key": "Name",
            "filters": [
                {
                    "$type": "@throttle",
                    "interval": 5000,
                    "countField": "count",
                    "allowEmpty": true
                },
                {
                    "$type": "Ruley.Redis.ReplayFieldFilter, Ruley.Redis",
                    "field": "Name"
                },
                {
                    "$type": "@conditional",
                    "field": "count",
                    "level": 0,
                    "match": ">",
                    "destination": "IsAlive",
                    "false": {
                        "$type": "@passthrough"
                    }
                },
                {
                    "$type": "@prev",
                    "destination": "prev"
                },
                {
                    "$type": "@map",
                    "field": "IsAlive",
                    "mapping": [
                        [ true, "Online" ],
                        [ false, "Offline" ]
                    ],
                    "destination": "status"
                },
                {
                    "$type": "@branch",
                    "field": "IsAlive",
                    "level": "{prev.IsAlive}",
                    "match": "!=",
                    "true": {
                        "$type": "@slackf",
                        "webHookUrl": "@slack_url",
                        "username": "error-bot",
                        "emoji": ":smiling_imp:",
                        "message": "{Name} is now {status}"
                    },
                    "false": {
                        "$type": "@passthrough"
                    }
                }
            ]
        }
    ],

    "outputs": [
        {
            "$type": "@console",
            "enabled": true
        }
    ]
}
