FROM quay.io/keycloak/keycloak:18.0 as builder

ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true
ENV KC_DB=postgres
# Install custom providers
# here
RUN /opt/keycloak/bin/kc.sh build

FROM quay.io/keycloak/keycloak:18.0
COPY --from=builder /opt/keycloak/ /opt/keycloak/

COPY start.sh /opt/keycloak/start.sh

WORKDIR /opt/keycloak
# for demonstration purposes only, please make sure to use proper certificates in production instead
RUN keytool -genkeypair -storepass password -storetype PKCS12 -keyalg RSA -keysize 2048 -dname "CN=server" -alias server -ext "SAN:c=DNS:localhost,IP:127.0.0.1" -keystore conf/server.keystore
# change these values to point to a running postgres instance

ENV KC_HOSTNAME=localhost

ENTRYPOINT ["/opt/keycloak/start.sh"]