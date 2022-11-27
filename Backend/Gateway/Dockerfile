FROM envoyproxy/envoy-debug-dev:latest

COPY ./backend/Gateway/envoy.yaml /etc/envoy.yaml
COPY ./backend/Gateway/configs /var/lib/envoy

ADD ./backend/Gateway/https/server.crt /etc/server.crt
ADD ./backend/Gateway/https/server.key /etc/server.key

RUN chmod go+x /var/lib/envoy && chmod go+r /etc/envoy.yaml /var/lib/envoy/*

CMD ["/usr/local/bin/envoy", "-c /etc/envoy.yaml"]