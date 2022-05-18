#!/bin/sh
/usr/local/bin/envoy -c "/etc/envoy-config/http-service.yaml" --service-cluster front-proxy
