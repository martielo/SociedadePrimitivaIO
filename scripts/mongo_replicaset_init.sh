#!/bin/bash

set -e
set -u

_config=\
'
{
	"_id": "rs0",
	"members": [
		{ "_id": 0, "host": "mongo1" },
		{ "_id": 1, "host": "mongo2" },
		{ "_id": 2, "host": "mongo3", arbiterOnly: true },
	]
}
'

sleep 10;

mongosh --quiet \
	--host mongo1 \
	<<-EOF
		rs.initiate($_config);
	EOF

exec "$@"

sleep infinity