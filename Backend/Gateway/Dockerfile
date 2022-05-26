FROM envoyproxy/envoy-dev:latest

COPY ./envoy.yaml /etc/envoy.yaml
COPY ./configs /var/lib/envoy
COPY ./https /var/lib/envoy/https

RUN chmod go+x /var/lib/envoy && chmod go+r /etc/envoy.yaml /var/lib/envoy/*

CMD ["/usr/local/bin/envoy", "-c /etc/envoy.yaml"]