#!/usr/bin/env bash
#echo "Starting import...";
#/opt/keycloak/bin/kc.sh import --file /tmp/logistics-realm.json --override false;

echo "Starting server in normal mode...";
  /opt/keycloak/bin/kc.sh --spi-login-protocol-openid-connect-legacy-logout-redirect-uri=true start;